using AspNetCoreIdentity.Data;
using AspNetCoreIdentity.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreIdentity.Config
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddAuthorizationConfig(this IServiceCollection services)
        {
            //claims
            services.AddAuthorization(options =>
            {
                options.AddPolicy("PodeExcluir", police => police.RequireClaim("PodeExcluir"));
                options.AddPolicy("PodeLer", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeLer")));
                options.AddPolicy("PodeEscrever", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeEscrever")));

            });
            return services;
        }

        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<AspNetCoreIdentityContext>(options =>
                     options.UseSqlServer(
                         Configuration.GetConnectionString("AspNetCoreIdentityContextConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AspNetCoreIdentityContext>();
            return services;
        }
    }
}
