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
using Incident_Reporting.Data.Entities;
using Incident_Reporting.Data;
using Microsoft.EntityFrameworkCore;
using Incident_Reporting.Classes.Exceptions;

namespace Incident_Reporting.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataService _incidentDataService;
        protected readonly ITCPL_Keystone_XL_Safety_ReportsContext _dbContext;
        public HomeController(ILogger<HomeController> logger, IDataService incidentDataService, ITCPL_Keystone_XL_Safety_ReportsContext dbContext)
        {
            _logger = logger;
            _incidentDataService = incidentDataService;
            _dbContext = dbContext;
        }


     
        public IActionResult Index()
        {
            try
            {
                var user = _incidentDataService.FindUser();

                ViewBag.Name = user.FirstName +' ' + user.LastName;
                ViewBag.FirstName = user.FirstName;
                ViewBag.LastName = user.LastName;
                ViewBag.Email = user.Email;
              
                return View();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        public JsonResult GetIncident_Type(string text)
        {
            try {
               
                using (TCPL_Keystone_XL_Safety_ReportsContext incidentDC = new TCPL_Keystone_XL_Safety_ReportsContext())
                {
                    var incidentTypes = incidentDC.IncidentTypes.ToList();
                    return Json(incidentTypes.Select(c => new { id = c.Id, incidentTypeName = c.IncidentTypeName }).ToList());
                }
               
        }
            catch(Exception ex)
            {
                return null;
            }
        }
        public JsonResult GetClient(string text)
        {
            try
            {
                using (TCPL_Keystone_XL_Safety_ReportsContext clientDC = new TCPL_Keystone_XL_Safety_ReportsContext()) 
                {
                    var client = clientDC.Clients.ToList();
                    return Json(client.Select(c => new { id = c.Id, clientCompanyName = c.ClientCompanyName }).ToList());
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public JsonResult GetCountry()
        {
            try
            {
                using (TCPL_Keystone_XL_Safety_ReportsContext countryDC = new TCPL_Keystone_XL_Safety_ReportsContext())
                {
                    var country = countryDC.Countries.ToList();

                    return Json(country.Select(c => new { id = c.Id, countryName = c.CountryName }).ToList());
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JsonResult GetStates(int? country)
        {
            try
            {
                using (TCPL_Keystone_XL_Safety_ReportsContext stateDC = new TCPL_Keystone_XL_Safety_ReportsContext())
              
                {
                    var state = stateDC.StateProvinces.AsQueryable();
                    if (country != null)
                    {
                        state = state.Where(p => p.CountryId == country);
                    }

                    return Json(state.Select(p => new { id = p.Id, stateProvinceName = p.StateProvinceName }).ToList());
                    
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JsonResult GetProjects(int? client)
        {
            try
            {
                using (TCPL_Keystone_XL_Safety_ReportsContext projectDC = new TCPL_Keystone_XL_Safety_ReportsContext())
             
                {
                    var project = projectDC.Projects.AsQueryable();
                    if (client != null)
                    {
                        project = project.Where(p => p.ClientId == client);
                    }

                    return Json(project.Select(p => new { id = p.Id, projectName = p.ProjectName }).ToList());
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
        public IActionResult Submit(IncidentDataVM incidentData)
        {
            try { 
            int userId=0;
            if (string.IsNullOrEmpty(incidentData.Email))
            {
                var user = _incidentDataService.FindUser();
                using (TCPL_Keystone_XL_Safety_ReportsContext userDC = new TCPL_Keystone_XL_Safety_ReportsContext())
                {
                    //var userDB = userDC.Users.AsQueryable();
                    //userId = Convert.ToInt32(userDB.Where(p => p.Email == user.Email).FirstOrDefault());

                    var userDB = userDC.Users.FirstOrDefault(u => u.Email == user.Email);
                    userId = userDB.Id;
                }

            }
            else
            {
                using (TCPL_Keystone_XL_Safety_ReportsContext userDC = new TCPL_Keystone_XL_Safety_ReportsContext())
                //using (var userDC = _dbContext as TCPL_Keystone_XL_Safety_ReportsContext)
                {
                    //var user = userDC.Users.AsQueryable();

                    var userDB = userDC.Users.FirstOrDefault(u => u.Email == incidentData.Email);
                    userId = userDB.Id;
                }
               
            }
            using (TCPL_Keystone_XL_Safety_ReportsContext incidentDC = new TCPL_Keystone_XL_Safety_ReportsContext())
            {
                var dateTimeUtcNow = DateTime.UtcNow;

                var newincidentReport = new IncidentReport()
                {   IncidentTypeId= incidentData.IncidentTypeId,
                    UserId = userId,
                    ProjectId= incidentData.ProjectId,
                    DateTimeIncidentUtc=incidentData.DateTimeIncidentUtc,
                     ReporterCompanyName=incidentData.ReporterCompanyName,
                    LocationClassId = 1,
                    Description= incidentData.Description,
                    ActionTaken=incidentData.Description,
                     DateTimeReportedUtc= DateTime.UtcNow
                };

                incidentDC.IncidentReports.Add(newincidentReport);
                incidentDC.Entry(newincidentReport).State = EntityState.Added;
                incidentDC.SaveChanges();
                }
                return View();
            }

            catch (Exception e)
            {
                //transaction.Rollback();
                throw new ShowMessageException(e.InnerException.Message);
            }
           
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
