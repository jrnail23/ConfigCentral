using System;
using Microsoft.Owin.Hosting;

namespace ConfigCentral.WebApi.TopShelfHost
{
    public class ConfigCentralApplication
    {
        private IDisposable _webApplication;

        public void Start(OwinPipeline owinPipeline)
        {
            _webApplication = WebApp.Start("http://localhost:5001", owinPipeline.Configuration);
        }

        public void Stop()
        {
            _webApplication.Dispose();
        }
    }
}