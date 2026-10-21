
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iRecommendWeb.Models;
using iRecommendWeb.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using iRecommendWeb.Services;
using Microsoft.Identity.Client.RP;
using Microsoft.AspNetCore.Components;

namespace iRecommendWeb.Controllers
{

    [Authorize(Roles = Roles.Teacher)]
    public class RecLetterRequestPoolsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RecLetterPoolService poolService;
        

        public RecLetterRequestPoolsController(ApplicationDbContext context, RecLetterPoolService service)
        {
            _context = context;
            this.poolService = service;
        }

        // GET: RECLETTERREQUESTPOOLS
        public async Task<IActionResult> Index()
        {
            List<RecLetterRequestPool> pool = await (from p in _context.RecLetterRequestPool
                                                     where p.teacherId == User.FindFirstValue(ClaimTypes.NameIdentifier)
                                                     select p
                                                     ).ToListAsync();

            return View(pool);
        }

        // GET: RECLETTERREQUESTPOOLS/Details/5
        public async Task<IActionResult> Details(System.Guid? poolid)
        {
            if (poolid == null)
            {
                return NotFound();
            }

            

            RecLetterRequestPool recletterrequestpool = await poolService.getRecLetterPoolById(poolid);

            List<RecLetterRequest> letters = await (from letter in _context.RecLetterRequest
                                                        where recletterrequestpool.requestIds.Contains(letter.letterId)
                                                        select letter).ToListAsync();

            List<ReqLetterRequestDisplay> displaysReq = new List<ReqLetterRequestDisplay>();
            for (int x = 0; x < letters.Count; x++) {
                RecLetterRequest req = letters[x];
                string name = await (from u in _context.Users
                                     where u.Id == req.studentId
                                     select u.Email
                                     ).MinAsync();
                displaysReq.Add(new ReqLetterRequestDisplay(req.letterId, name, req.studentId, req.teacherId, req.poolId, req.method, req.details, req.deadline, req.college, req.currentProgress));
                    
            }

           RecLetterPoolDisplay display = new RecLetterPoolDisplay(recletterrequestpool.poolName,displaysReq, recletterrequestpool.closingDate);

            if (recletterrequestpool.teacherId != User.FindFirstValue(ClaimTypes.NameIdentifier)) {

                return Redirect(nameof(Index));
            
            }

            if (recletterrequestpool == null)
            {
                return NotFound();
            }

            display.poolId = recletterrequestpool.poolId;

            return View(display);
        }

        // GET: RECLETTERREQUESTPOOLS/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RECLETTERREQUESTPOOLS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("poolId,poolName,teacherId,requestIds, closingDate")] RecLetterRequestPool recletterrequestpool)
        {

            ModelState.Remove(nameof(RecLetterRequestPool.teacherId));
            ModelState.Remove(nameof(RecLetterRequestPool.requestIds));

            recletterrequestpool.teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            recletterrequestpool.requestIds = new List<Guid>();
           // recletterrequestpool.requestIds = new List<string>();
            Console.WriteLine($"Mode State: ${ModelState.IsValid}");

            if (ModelState.IsValid)
            {
                
                _context.Add(recletterrequestpool);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recletterrequestpool);
        }

       

        // GET: RECLETTERREQUESTPOOLS/Delete/5
        public async Task<IActionResult> Delete(System.Guid? poolid)
        {
            if (poolid == null)
            {
                return NotFound();
            }

            var recletterrequestpool = await _context.RecLetterRequestPool
                .FirstOrDefaultAsync(m => m.poolId == poolid);
            if (recletterrequestpool == null)
            {
                return NotFound();
            }

            if (recletterrequestpool.teacherId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {

                Redirect(nameof(Index));

            }

            return View(recletterrequestpool);
        }

        // POST: RECLETTERREQUESTPOOLS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(System.Guid? poolid)
        {
            var recletterrequestpool = await _context.RecLetterRequestPool.FindAsync(poolid);

            

            if (recletterrequestpool.teacherId != User.FindFirstValue(ClaimTypes.NameIdentifier)) {

                Redirect(nameof(Index));
            
            }

            if (recletterrequestpool != null)
            {
                _context.RecLetterRequestPool.Remove(recletterrequestpool);

                foreach(System.Guid? letterid in recletterrequestpool.requestIds)
                {
                    RecLetterRequest letter = await _context.RecLetterRequest.FindAsync(letterid);
                    if (letter != null)
                    {
                        _context.Remove(letter);

                    }
                }
            }



            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecLetterRequestPoolExists(System.Guid? poolid)
        {
            return _context.RecLetterRequestPool.Any(e => e.poolId == poolid);
        }
    }
}
