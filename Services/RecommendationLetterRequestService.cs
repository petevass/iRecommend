using iRecommendWeb.Data;
using iRecommendWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;

namespace iRecommendWeb.Services
{
    public class RecommendationLetterRequestService
    {
        private readonly ApplicationDbContext _context;

        public RecommendationLetterRequestService(ApplicationDbContext context) {
            _context = context;

        }



        public async Task<RecLetterRequest> getRecLetterByStudentId(string student) {

            RecLetterRequest req = await (from r in _context.RecLetterRequest
                                          where r.studentId == student
                                          select r
                                        ).FirstAsync();


            return req;
        }

        public async Task<RecLetterRequest> getRecLetterById(Guid? id) {

            RecLetterRequest req = await (from r in _context.RecLetterRequest
                                          where r.letterId == id
                                          select r
                                         ).FirstAsync();
            return req;
        }

        public bool existsById(Guid id) {

            return _context.RecLetterRequest.Any(req => req.letterId == id);
        
        }

    }
}
