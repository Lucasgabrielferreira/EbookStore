using Microsoft.EntityFrameworkCore;

namespace EbookStore.Configuration
{
    public static class MvcConfig
    {
        public static WebApplicationBuilder AddMvcConfiguration(this WebApplicationBuilder builder)
        {
            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            builder.Services.AddControllersWithViews();

            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<ContextoEbookStore>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            return builder;
        }

        public static WebApplication UseMvcConfiguration(this WebApplication app)
        {

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
               // app.UseExceptionHandler("/Home/Error");
               app.UseDeveloperExceptionPage();
               
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            return app;
        }

    }
}
