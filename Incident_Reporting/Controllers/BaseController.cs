using Incident_Reporting.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Incident_Reporting.Controllers
{
    public abstract class BaseController : Controller
    {
        public BaseController()
        {
            CultureInfo.DefaultThreadCurrentCulture =
                CultureInfo.DefaultThreadCurrentUICulture =
                new CultureInfo("en-US");
        }
        public virtual TCPL_Keystone_XL_Safety_ReportsContext GetContext()
        {
            return new TCPL_Keystone_XL_Safety_ReportsContext();
        }
    }
}
