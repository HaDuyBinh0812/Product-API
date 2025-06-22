using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Product.Core.Entities;
using Product.Core.Interface;
using Product.Infrastrucre.Data;
using Product.Infrastrucre.Data.Config;
using Product.Infrastrucre.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastrucre
{
    public static class InfraStructureRequistration
    {
        public static IServiceCollection InfraStructureConfigration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitWork, UnitWork>();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
            //Configration token services 
            services.AddScoped<ITokenServices, TokenServices>();
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddMemoryCache();
            //services.AddAuthentication(opt =>
            //{
            //    opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //});
            services.AddAuthentication(opt=>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey =true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                    ValidIssuer = configuration["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });
            return services;
        }
        public static async Task infastructureConfigMiddleware(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                await IdentitySeed.SeedUserAsync(userManager, dbContext);
            }
        }
    }
}   
