﻿namespace Exe.Starot.Api.Installer
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection services,IConfiguration configuration);


    }
}
