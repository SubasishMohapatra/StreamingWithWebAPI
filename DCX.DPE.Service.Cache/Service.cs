using DCX.DPE.Service.Cache.Properties;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DCX.DPE.Service.Cache
{
    public partial class Service : ServiceBase
    {
        private IDisposable _startup;

        public Service()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The OnStart method.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            StartOptions options = new StartOptions();
            //string apiName = Settings.Default.BaseApiUrl;
            //options.Urls.Add(apiName);
            //options.Urls.Add("http://*:9889/DPE");
            options.Urls.Add("http://+:7654/DPE");
            _startup = WebApp.Start<OwinStartup>(options);
        }

        /// <summary>
        /// The OnStop method.
        /// </summary>
        protected override void OnStop()
        {
            if (_startup != null)
            {
                _startup.Dispose();
                _startup = null;
            }
        }

        /// <summary>
        /// The StartService method.
        /// </summary>
        /// <param name="args"></param>
        public void StartService(string[] args)
        {
            OnStart(args);
        }

        /// <summary>
        /// The StopService method.
        /// </summary>
        public void StopService()
        {
            OnStop();
        }
    }
}
