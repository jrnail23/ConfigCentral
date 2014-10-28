using System;
using Microsoft.Owin.Hosting;

namespace ConfigCentral.WebApi
{
    public class ConfigCentralApplication
    {
        private IDisposable _webApplication;

        public void Start()
        {
            _webApplication = WebApp.Start<WebPipeline>("http://localhost:5001");
        }

        public void Stop()
        {
            _webApplication.Dispose();
        }
    }
}