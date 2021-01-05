using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Incident_Reporting.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Incident_Reporting.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        public List<IncidentTypesVM> GetIncident_Type(string text)
        {
            try { 
            using (var incidentReportDC = GetContext())
            {
                var incidentreport = incidentReportDC.IncidentTypes.Select(incidenttype => new IncidentTypesVM
                {
                    Id = incidenttype.Id,
                    IncidentTypeName = incidenttype.IncidentTypeName,
                    IncidentTypeDescription = incidenttype.IncidentTypeDescription

                }); ;

                return incidentreport.ToList();
            }
        }
            catch(Exception ex)
            {
                return null;
            }
        }
        public JsonResult GetCountry()
        {
            try
            {
                using (var countryDC = GetContext())
                {
                    var country = countryDC.Countries.ToList();
                  

                    return Json(country.ToList());
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JsonResult GetStates()
        {
            try
            {
                using (var stateDC = GetContext())
                {
                    var state = stateDC.StateProvinces.ToList();


                    return Json(state.ToList());
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
