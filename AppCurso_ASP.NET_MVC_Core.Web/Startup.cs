using AppCurso_ASP.NET_MVC_Core.Business.Interfaces;
using AppCurso_ASP.NET_MVC_Core.Data.Context;
using AppCurso_ASP.NET_MVC_Core.Data.Repository;
using AppCurso_ASP.NET_MVC_Core.Web.Data;
using AppCurso_ASP.NET_MVC_Core.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AppCurso_ASP.NET_MVC_Core.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));

			services.AddDbContext<AppCursoDbContext>(opt => 
				opt.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));

			services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddAutoMapper(typeof(Startup));

			services.AddControllersWithViews(opt => opt.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
			// com isso sua app fica protegida contra ataques de csrf, não tem a necessidade tbm de colocar o atributo ValidateAntiForgeryToken
			// em todas as controllers.

			services.AddRazorPages();

			services.DependencyInjection();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/erro/500");
				app.UseStatusCodePagesWithRedirects("/erro/{0}");
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			var defaultCulture = new CultureInfo("pt-BR");
			var localizationOptions = new RequestLocalizationOptions
			{ 
				DefaultRequestCulture = new RequestCulture(defaultCulture),
				SupportedCultures = new List<CultureInfo> { defaultCulture },
				SupportedUICultures = new List<CultureInfo> { defaultCulture }
			};
			app.UseRequestLocalization(localizationOptions);

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});
		}
	}
}
