using Exe.Starot.Api.Configuration;
using Exe.Starot.Api.Filters;
using Exe.Starot.Api.Installer;
using Exe.Starot.Application;
using Exe.Starot.Application.FileUpload;
using Exe.Starot.Application.Order;
using Exe.Starot.Infrastructure;
using Serilog;

namespace Exe.Starot.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallerServicesInAssembly(Configuration);
            

            // Register System.Text.Encoding
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

         
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Development error page
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // Handle exceptions in production
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseSerilogRequestLogging();

            app.UseAuthentication();
            app.UseAuthorization();

            // Endpoint configuration
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/notificationHub");
            });

            app.UseSwashbuckle(Configuration);
        }
    }
}
