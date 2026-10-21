using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iRecommendWeb.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
    public DbSet<iRecommendWeb.Models.RecLetterRequestPool> RecLetterRequestPool { get; set; } = default!;
    public DbSet<iRecommendWeb.Models.RecLetterRequest> RecLetterRequest { get; set; } = default!;
    }
}
