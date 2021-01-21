using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Incident_Reporting.Controllers
{
    //[AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration, IHostingEnvironment environment) : base(configuration, environment)
        {
            _configuration = configuration;
        }


        public async Task<IActionResult> SignedOut()
        {
            foreach (var claim in User.Claims)
            {
                if (claim.Type == "authenticationType")
                {
                    if (claim.Value == "OPENID_CONNECT")
                    {
                        Response.Cookies.Append("authenticationType", "OPENID_CONNECT");
                    }
                    else
                    {
                        Response.Cookies.Append("authenticationType", "PASSWORD");
                    }
                }
            }

            await HttpContext.SignOutAsync();
            SignOut("cookie", "oidc");
            var host = _configuration["GIS_Upload_Page:Authority"];
            var cookieName = _configuration["GIS_Upload_Page:CookieName"];

            var domain = Request.Scheme + "://" + Request.Host.Value;
            Response.Cookies.Delete(cookieName, new CookieOptions { Domain = domain.ToString() });

            var clientId = _configuration["GIS_Upload_Page:ClientId"];
            var url = host + "/oauth2/logout?client_id=" + clientId;

            return Redirect(url);
        }

        public IActionResult SignedOut2()
        {
            var authType = Request.Cookies["authenticationType"];
            if (authType == "OPENID_CONNECT")
            {
                var tenantId = _configuration["GIS_Upload_Page:TenantId"];
                var url = "https://login.microsoftonline.com/" + tenantId + "/oauth2/v2.0/logout";
                return Redirect(url);
            }
            else
            {
                var url = "~/Home/Index";
                return Redirect(url);
            }
        }
        //public async Task<IActionResult> SignedOut_Azure()
        //{
        //    await HttpContext.SignOutAsync();
        //    SignOut("cookie", "oidc");
        //    var host = _configuration["GIS_Upload_Page:Authority"];
        //    var cookieName = _configuration["GIS_Upload_Page:CookieName"];
        //    var domain = Request.Scheme + "://" + Request.Host.Value;
        //    Response.Cookies.Delete(cookieName, new CookieOptions { Domain = domain.ToString() });
        //    var clientId = _configuration["GIS_Upload_Page:ClientId"];
        //    //var url1 = host + "/oauth2/logout?client_id=" + clientId;
        //    //Redirect(url1);
        //    // RedirectToAction("SignedOut", "Account");
        //    // var url = "https://login.microsoftonline.com/4ac47f73-7479-484a-903a-7c08b6270689/oauth2/v2.0/logout";
        //    var url = host + "/oauth2/logout?client_id=" + clientId + "&redirect_uri=https://login.microsoftonline.com/4ac47f73-7479-484a-903a-7c08b6270689/oauth2/v2.0/logout";
        //    return Redirect(url);
        //}
    }
}
