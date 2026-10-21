using iRecommendWeb.Data;
using iRecommendWeb.Data.Migrations;
using iRecommendWeb.Models;
using Microsoft.EntityFrameworkCore;
using RecLetterRequestPool = iRecommendWeb.Models.RecLetterRequestPool;

namespace iRecommendWeb.Services
{
    public class RecLetterPoolService
    {

        private readonly ApplicationDbContext context;

        public RecLetterPoolService(ApplicationDbContext context) {

            this.context = context;
        
        }




        public async Task<RecLetterRequestPool> GetRecLetterByTeacherId(string teacher)
        {

            RecLetterRequestPool req = await (from r in context.RecLetterRequestPool
                                          where r.teacherId == teacher
                                          select r
                                        ).FirstAsync();


            return req;
        }

        public async Task<RecLetterRequestPool> getRecLetterPoolById(Guid? id)
        {

            RecLetterRequestPool req = await (from r in context.RecLetterRequestPool
                                          where r.poolId == id
                                          select r
                                         ).FirstAsync();
            return req;
        }

        public bool existsById(Guid? id)
        {

            return context.RecLetterRequestPool.Any(req => req.poolId == id);

        }


    }
}
