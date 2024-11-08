﻿using Exe.Starot.Application.Common.Behaviours;
using Exe.Starot.Application.Common.Validation;
using Exe.Starot.Application.FileUpload;
using Exe.Starot.Application.PayOs;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


using System.Reflection;


namespace Exe.Starot.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), lifetime: ServiceLifetime.Transient);
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
                cfg.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
                cfg.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
            });
           
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidatorProvider, ValidatorProvider>();
            services.AddTransient<FileUploadService>();
            services.AddTransient<PayOsServices>();

            return services;
        }
    }
}
