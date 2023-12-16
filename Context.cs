using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace soft
{
    public class Context:IdentityDbContext
    {
        public Context(DbContextOptions opt):base(opt)
        {
            
        }
    }
}
