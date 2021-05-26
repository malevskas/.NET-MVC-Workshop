using System;
using Final.Areas.Identity.Data;
using Final.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Final.Areas.Identity.IdentityHostingStartup))]
namespace Final.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<FinalContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("FinalContext")));

                /*services.AddDefaultIdentity<FinalUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<FinalUserContext>();*/
            });
        }
    }
}