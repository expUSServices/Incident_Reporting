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
using System.Net.Http.Headers;
using System.IO;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using System.Text;

namespace Incident_Reporting.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataService<IncidentDataVM> _incidentDataService;
        protected readonly ITCPL_Keystone_XL_Safety_ReportsContext _dbContext;
        private readonly IWebHostEnvironment _environment;

        public HomeController(ILogger<HomeController> logger, IDataService<IncidentDataVM> incidentDataService, ITCPL_Keystone_XL_Safety_ReportsContext dbContext, IWebHostEnvironment environment)
        {
            _logger = logger;
            _incidentDataService = incidentDataService;
            _dbContext = dbContext;
             _environment = environment;
        }

        [HttpGet]
        [Authorize(Policy = "Registered")]
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
                    var client = clientDC.Clients.AsQueryable();
                    foreach (var claim in User.Claims)
                    {
                        if (claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")
                        {
                            if (claim.Value == "tc_guest@tcenergy.com")
                            {
                                client = client.Where(p => p.Id == 1);
                                
                            }
                            else if (claim.Value == "et_guest@energytransfer.com")
                            {
                                client = client.Where(p => p.Id == 2);
                            }
                            else if (claim.Value != "tc_guest@tcenergy.com" || claim.Value != "et_guest@energytransfer.com")
                            {
                                return Json(client.Select(c => new { id = c.Id, clientCompanyName = c.ClientCompanyName }).ToList());
                            }
                        }
                    }
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

        public JsonResult GetStates(int? CountryId)
        {
            try
            {
                using (TCPL_Keystone_XL_Safety_ReportsContext stateDC = new TCPL_Keystone_XL_Safety_ReportsContext())
              
                {
                    var state = stateDC.StateProvinces.AsQueryable();
                    if (CountryId != null)
                    {
                        state = state.Where(p => p.CountryId == CountryId);
                    }

                    return Json(state.Select(p => new { id = p.Id, stateProvinceName = p.StateProvinceName }).ToList());
                    
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JsonResult GetProjects(int? ClientId)
        {
            try
            {
                using (TCPL_Keystone_XL_Safety_ReportsContext projectDC = new TCPL_Keystone_XL_Safety_ReportsContext())
             
                {
                    var project = projectDC.Projects.AsQueryable();
                    if (ClientId != null)
                    {
                        project = project.Where(p => p.ClientId == ClientId);
                    }

                    return Json(project.Select(p => new { id = p.Id, projectName = p.ProjectName }).ToList());
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public JsonResult GetProjects_new(int? ClientId)
        {
            try
            {
                using (TCPL_Keystone_XL_Safety_ReportsContext projectDC = new TCPL_Keystone_XL_Safety_ReportsContext())

                {
                    var project = projectDC.Projects.AsQueryable();
                    if (ClientId != null)
                    {
                        project = project.Where(p => p.ClientId == 1);
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
       
        [HttpPost]
        public IActionResult Submit(IncidentDataVM incidentData, IList<IFormFile> files)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    int loggedinuserId = 0;
                    if (string.IsNullOrEmpty(incidentData.Email))
                    {
                        var user = _incidentDataService.FindUser();
                        using (TCPL_Keystone_XL_Safety_ReportsContext userDC = new TCPL_Keystone_XL_Safety_ReportsContext())
                        {
                           var userDB = userDC.Users.FirstOrDefault(u => u.Email == user.Email);
                            loggedinuserId = userDB.Id;
                        }

                    }
                    else
                    {
                        using (TCPL_Keystone_XL_Safety_ReportsContext userDC = new TCPL_Keystone_XL_Safety_ReportsContext())
                        //using (var userDC = _dbContext as TCPL_Keystone_XL_Safety_ReportsContext)
                        {
                            //var user = userDC.Users.AsQueryable();

                            var userDB = userDC.Users.FirstOrDefault(u => u.Email == incidentData.Email);
                            loggedinuserId = userDB.Id;
                        }

                    }

                    //timezone code -- to be implemented later
                 
                    //TimeZoneInfo Zone = TimeZoneInfo.FindSystemTimeZoneById(incidentData.TimeZone);
                    //DateTime TimeZone_utc = TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(incidentData.DateTimeIncident), Zone);

                    //timezone code

                    var newincidentReport = new IncidentDataVM()
                    {
                        IncidentTypeId = incidentData.IncidentTypeId,
                        UserId = loggedinuserId,
                        ProjectId = incidentData.ProjectId,
                        DateTimeIncident = incidentData.DateTimeIncident,
                        ReporterCompanyName = incidentData.ReporterCompanyName,
                        LocationId = incidentData.LocationId,
                        Description = incidentData.Description,
                        ActionTaken = incidentData.ActionTaken,
                        StateId= incidentData.StateId,
                        DateTimeReportSubmittedUtc = DateTime.UtcNow
                    };
                    using (TCPL_Keystone_XL_Safety_ReportsContext incidentDC = new TCPL_Keystone_XL_Safety_ReportsContext())
                    {

                        using (var transaction = incidentDC.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                        {
                            try
                            {
                                _incidentDataService.Create(newincidentReport);

                                int incidentId = newincidentReport.Id;
                               
                                if (files != null && files.Count() > 0)
                                {
                                    for (var i = 0; i < files.Count(); i++)
                                    {
                                        var file = files[i];
                                        var fileName = Path.GetFileName(file.FileName); // GetFileName is necessary for Edge and prob IE
                                        var fileExt= Path.GetExtension(file.FileName);
                                        var fileSize = file.Length;
                                        var fileData = new StringBuilder();

                                        ////using (var reader = new StreamReader(file.OpenReadStream()))
                                        ////{
                                        ////    using (var fileStream = new MemoryStream())
                                        ////    {
                                        ////        var s = reader.ReadLine();
                                        ////        while (s != null)
                                        ////        {
                                        ////            fileData.Append(s);
                                        ////            fileData.Append(Environment.NewLine);
                                        ////            s = reader.ReadLine();
                                        ////        }
                                        ////    }
                                        ////}
                                        ////using (var stream = GenerateMemoryStreamFromString(fileData.ToString()))
                                        ////{

                                            var newincidentReportFileAttach = new IncidentDataVM()
                                            {
                                                Id= newincidentReport.Id,
                                                FileLocation =  file.FileName,
                                                FileExtension = fileExt
                                            };
                                            _incidentDataService.CreateAttachment(newincidentReportFileAttach,file);
                                       // }
                                    }
                                    transaction.Commit();
                                }
                            }
                            catch (Exception e)
                            {
                                transaction.Rollback();
                                throw new ShowMessageException(e.InnerException.Message);
                            }
                        }
                       
                    }
                    
                }
                else
                { 
                 
                    return Json(new { success = false, message = "Error" });
                }

                return View("Success");
                //return Json(new { success = true, message = "" });
            }

            catch (Exception e)
            {
               // transaction.Rollback();
                throw new ShowMessageException(e.InnerException.Message);
            }
           
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Success()
        {
            return View("Success");
        }

        private static MemoryStream GenerateMemoryStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
