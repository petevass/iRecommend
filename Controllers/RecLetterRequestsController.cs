using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iRecommendWeb.Models;
using iRecommendWeb.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using iRecommendWeb.Services;

namespace iRecommendWeb.Controllers
{

    record createRequest(string poolId, string details, DateTime deadline, SubmitMethod method) { }
    
    [Authorize]
    public class RecLetterRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RecommendationLetterRequestService letterService;

        public RecLetterRequestsController(ApplicationDbContext context, RecommendationLetterRequestService service)
        {
            letterService = service;
            _context = context;
        }

        // GET: RECLETTERREQUESTS
        [HttpGet]
        [Authorize(Roles=Roles.Student)]
        public async Task<IActionResult> Index()
        {

            List<RecLetterRequest> reqs = await (from rec in _context.RecLetterRequest
                              where rec.studentId == User.FindFirstValue(ClaimTypes.NameIdentifier)
                              select rec)
                                .ToListAsync();

            foreach (RecLetterRequest item in reqs)
            {
                List<ApplicationUser> user = await (from u in _context.Users
                                              where u.Id == item.teacherId
                                              select u).ToListAsync();
                item.teacherId = user[0].Email;
            }

            return View(reqs);
        }

        // GET: RECLETTERREQUESTS/Details/5
        [HttpGet]
        
        public async Task<IActionResult> Details(System.Guid? letterid)
        {
            if (letterid == null)
            {
                return NotFound();
            }

            var recletterrequest = await _context.RecLetterRequest
                .FirstOrDefaultAsync(m => m.letterId == letterid);
            if (recletterrequest == null)
            {
                return NotFound();
            }

            if (User.FindFirstValue(ClaimTypes.NameIdentifier) != recletterrequest.teacherId && User.FindFirstValue(ClaimTypes.NameIdentifier) != recletterrequest.studentId) {

                return NotFound();
            
            }

            string name = await (from u in _context.Users
                                 where u.Id == recletterrequest.studentId
                                 select u.Email
                                    ).MinAsync();

            ReqLetterRequestDisplay display = new ReqLetterRequestDisplay(recletterrequest.letterId,
                name, recletterrequest.studentId, recletterrequest.teacherId, recletterrequest.poolId,
                recletterrequest.method, recletterrequest.details, recletterrequest.deadline, recletterrequest.college,
                recletterrequest.currentProgress);

            return View(display);
        }

        [HttpGet("/RecLetterRequests/Create/{poolId?}")]
        [Authorize(Roles =Roles.Student)]
        public IActionResult Create(Guid? poolId)
        {
            if (poolId != null)
            {
                ViewData["poolId"] = poolId;
                
            }
            ViewData["disabled"] = poolId != null;
            
            return View();
        }

        // POST: RECLETTERREQUESTS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Student)]
        public async Task<IActionResult> Create([Bind("poolId,studentId,method,details,deadline,college,currentProgress")] RecLetterRequest recletterrequest)
        {
            ModelState.Remove(nameof(RecLetterRequest.teacherId));
            ModelState.Remove(nameof(RecLetterRequest.studentId));



            RecLetterRequestPool? pool = await (from p in _context.RecLetterRequestPool
                                                where p.poolId == recletterrequest.poolId
                                                select p).FirstOrDefaultAsync();

            if (pool == null)
            {
                ModelState.AddModelError("poolId", "That request pool doesn't exist.");
                ViewData["poolId"] = recletterrequest.poolId;
                ViewData["disabled"] = false;
                return View(recletterrequest);
            }

            recletterrequest.letterId = Guid.CreateVersion7();
            recletterrequest.teacherId = pool.teacherId;
            recletterrequest.studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            recletterrequest.currentProgress = ProgressLevel.NotStarted;
            

            if (!ModelState.IsValid)
            {
                return View(recletterrequest);
            }
            pool.requestIds.Add(recletterrequest.letterId);
            _context.Add(recletterrequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: RECLETTERREQUESTS/Edit/5
        public async Task<IActionResult> Edit(System.Guid? letterid)
        {
            if (letterid == null)
            {
                return NotFound();
            }

           

            var recletterrequest = await _context.RecLetterRequest.FindAsync(letterid);
            if (recletterrequest == null)
            {
                return NotFound();
            }

            if (User.FindFirstValue(ClaimTypes.NameIdentifier) != recletterrequest.studentId) {

                return NotFound();
            
            }

                return View(recletterrequest);
        }

        // POST: RECLETTERREQUESTS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(System.Guid? letterid, [Bind("college, letterId,studentId,teacherId,method,details,deadline,currentProgress")] RecLetterRequest recletterrequest)
        {
            ModelState.Remove(nameof(RecLetterRequest.studentId));
            ModelState.Remove(nameof(RecLetterRequest.teacherId));
            ModelState.Remove(nameof(RecLetterRequest.poolId));
      

            RecLetterRequest req = await letterService.getRecLetterById(letterid);
            req.method = recletterrequest.method;
            req.deadline = recletterrequest.deadline;
            req.details = recletterrequest.details;
            req.currentProgress = recletterrequest.currentProgress;
            req.college = recletterrequest.college;

            recletterrequest = req;
            if (recletterrequest.studentId != User.FindFirstValue(ClaimTypes.NameIdentifier)) 
            {
                return Unauthorized();
            }

            if (letterid == null) 
            {
                return NotFound();
            }

            if (letterid != recletterrequest.letterId)
            {
                return NotFound();
            }

            if (User.FindFirstValue(ClaimTypes.NameIdentifier) != recletterrequest.studentId) {

                return NotFound();

            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recletterrequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecLetterRequestExists(recletterrequest.letterId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recletterrequest);
        }

        // GET: RECLETTERREQUESTS/Delete/5
        [Authorize(Roles = Roles.Student)]
        public async Task<IActionResult> Delete(System.Guid? letterid)
        {
            if (letterid == null)
            {
                return NotFound();
            }

            var recletterrequest = await _context.RecLetterRequest
                .FirstOrDefaultAsync(m => m.letterId == letterid);
            if (recletterrequest == null)
            {
                return NotFound();
            }

            return View(recletterrequest);
        }

        // POST: RECLETTERREQUESTS/Delete/5
        [Authorize(Roles = Roles.Student)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(System.Guid? letterid)
        {
            var recletterrequest = await _context.RecLetterRequest.FindAsync(letterid);
            if (recletterrequest != null)
            {
                _context.RecLetterRequest.Remove(recletterrequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecLetterRequestExists(System.Guid? letterid)
        {
            return _context.RecLetterRequest.Any(e => e.letterId == letterid);
        }

        [HttpPost("/RecLetterRequests/update-status/{letterid}")]
        [Authorize(Roles = Roles.Teacher)]
        public async Task<IActionResult> updateStatus(System.Guid? letterid, [FromBody] ProgressLevel level) {

            // levelIndex = int.Parse(lIndex);

            if (letterid == null || !Enum.IsDefined(level))
            {
                return BadRequest();
            }

            RecLetterRequest? recletter = await letterService.getRecLetterById(letterid);
            if (recletter == null)
            {
                return NotFound();
            }

            if (User.FindFirstValue(ClaimTypes.NameIdentifier) != recletter.teacherId) {

                return Unauthorized();
            
            }

            recletter.currentProgress = level;
            _context.Update(recletter);
            await _context.SaveChangesAsync();
            return Ok();
        
        }
    }
}