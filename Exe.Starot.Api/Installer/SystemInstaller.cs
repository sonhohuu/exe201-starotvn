
using Exe.Starot.Api.Configuration;
using Exe.Starot.Application.Order;
using Exe.Starot.Application;
using Exe.Starot.Infrastructure;
using Exe.Starot.Application.FileUpload;
using Exe.Starot.Api.Filters;
using Net.payOS;

namespace Exe.Starot.Api.Installer
{
    public class SystemInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // Add necessary services
            services.AddControllers(opt => opt.Filters.Add(typeof(ExceptionFilter)));

            services.AddSignalR();
            services.AddScoped<OrderService>();
            services.AddApplication(configuration);
            services.ConfigureApplicationSecurity(configuration);
            services.ConfigureProblemDetails();
            services.ConfigureApiVersioning();
            services.AddInfrastructure(configuration);
            services.ConfigureSwagger(configuration);

            // CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins("https://starotvn.com", "https://localhost:7024", "http://localhost:7024")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });


            // Register System.Text.Encoding
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            // Validate FirebaseConfig section
            var firebaseSection = configuration.GetSection("FirebaseConfig");
            if (!firebaseSection.Exists())
            {
                throw new ArgumentNullException("FirebaseConfig section does not exist in configuration.");
            }

            var firebaseConfig = firebaseSection.Get<FirebaseConfig>();
            if (firebaseConfig == null)
            {
                throw new ArgumentNullException(nameof(firebaseConfig), "FirebaseConfig section is missing in configuration.");
            }

            services.AddSingleton<PayOS>(provider =>
            {
                string clientId = configuration["PaymentEnvironment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find PAYOS_CLIENT_ID");
                string apiKey = configuration["PaymentEnvironment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find PAYOS_API_KEY");
                string checksumKey = configuration["PaymentEnvironment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find PAYOS_CHECKSUM_KEY");

                return new PayOS(clientId, apiKey, checksumKey);

            });

            services.AddSingleton(firebaseConfig);
            services.AddSingleton<FileUploadService>();

        }
    }
}
