using Incident_Reporting.Data;
using Incident_Reporting.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace Incident_Reporting.Controllers
{
    public abstract class BaseController : Controller
    {
        //public BaseController()
        //{
        //    CultureInfo.DefaultThreadCurrentCulture =
        //        CultureInfo.DefaultThreadCurrentUICulture =
        //        new CultureInfo("en-US");
        //}
        //public virtual TCPL_Keystone_XL_Safety_ReportsContext GetContext()
        //{
        //    return new TCPL_Keystone_XL_Safety_ReportsContext();
        //}

        private IConfigurationSection _configs;
        private IConfigurationSection _appConfigs;
        private IConfigurationSection _featuresConfigs;

        public BaseController(IConfiguration configuration, IHostingEnvironment environment) : base()
        {
            Configuration = configuration;
            Environment = environment;
            _configs = Configuration.GetSection("Configs");
            _appConfigs = _configs.GetSection("Application");
            _featuresConfigs = _configs.GetSection("Features");

            CultureInfo.DefaultThreadCurrentCulture =
                CultureInfo.DefaultThreadCurrentUICulture =
                new CultureInfo("en-US");
        }

        public virtual TCPL_Keystone_XL_Safety_ReportsContext GetContext()
        {
            return new TCPL_Keystone_XL_Safety_ReportsContext();
        }
        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

    }
}
