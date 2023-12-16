using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using Microsoft.AspNetCore.Identity;



namespace PetAdopt
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PetAdoptContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("PetAdoptContext")));

            services.AddDbContext<LibraryIdentityContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("LibraryIdentityContext")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<LibraryIdentityContext>();

            services.AddRazorPages();
        }
    }


}
