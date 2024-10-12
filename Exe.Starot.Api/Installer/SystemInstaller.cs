﻿
using Exe.Starot.Api.Configuration;
using Exe.Starot.Application.Order;
using Exe.Starot.Application;
using Exe.Starot.Infrastructure;
using Exe.Starot.Application.FileUpload;
using Exe.Starot.Api.Filters;

namespace Exe.Starot.Api.Installer
{
    public class SystemInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(opt => opt.Filters.Add(typeof(ExceptionFilter)));
            services.AddSignalR();
            services.AddSignalR();
            services.AddScoped<OrderService>();
            services.AddApplication(configuration);
            services.ConfigureApplicationSecurity(configuration);
            services.ConfigureProblemDetails();
            services.ConfigureApiVersioning();
            services.AddInfrastructure(configuration);
            services.ConfigureSwagger(configuration);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins("https://localhost:7024")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });

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

            services.AddSingleton(firebaseConfig);
            services.AddSingleton<FileUploadService>();

        }
    }
}