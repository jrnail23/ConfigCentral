using System;
using Microsoft.Owin.Hosting;

namespace ConfigCentral.WebApi.TopShelfHost
{
    public class ConfigCentralApplication
    {
        private IDisposable _webApplication;

        public void Start(Startup startup)
        {
            _webApplication = WebApp.Start("http://localhost:5001", startup.Configuration);
        }

        public void Stop()
        {
            _webApplication.Dispose();
        }
    }
}