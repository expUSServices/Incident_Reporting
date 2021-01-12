using System.Collections.Generic;
using Incident_Reporting.Models;
using Incident_Reporting.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Incident_Reporting.Extensions;
using System.Linq;
using System;

namespace Incident_Reporting.Data
{
  
    public class IncidentDataService :  IDataService<IncidentDataVM>
    {
        private readonly static object userLock = new object();

        protected readonly IHttpContextAccessor _httpContextAccessor;

        protected readonly DbContext _dbContext;
        public IncidentDataService(TCPL_Keystone_XL_Safety_ReportsContext dbContext) 
        {
        }

        public IncidentDataService(TCPL_Keystone_XL_Safety_ReportsContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        //public IncidentDataService(DbContext dbContext, IHttpContextAccessor httpContextAccessor)
        //{
        //}
        //public IEnumerable<IncidentDataVM> Read(bool showDeleted = false)
        //{
        //    // return GetAll(showDeleted: showDeleted);
        //    return null;
        //}

        public void Create(IncidentDataVM model)
        {
            var incidentDC = _dbContext as TCPL_Keystone_XL_Safety_ReportsContext;
           
            var newincidentReport = new IncidentReport()
            {
                IncidentTypeId = model.IncidentTypeId,
                UserId = model.UserId,
                ProjectId = model.ProjectId,
                DateTimeIncidentUtc = model.DateTimeIncidentUtc,
                ReporterCompanyName = model.ReporterCompanyName,
                LocationClassId = model.LocationId,
                Description = model.Description,
                ActionTaken = model.ActionTaken,
                DateTimeReportedUtc = DateTime.UtcNow
            };
            incidentDC.IncidentReports.Add(newincidentReport);
            incidentDC.Entry(newincidentReport).State = EntityState.Added;
            incidentDC.SaveChanges();
           
            model.Id = (int)newincidentReport.Id;

            var incidentToState = new IncidentToStateProvince()
            {
                IncidentId = model.Id,
                StateProvinceId = model.StateId
            };
            incidentDC.IncidentToStateProvinces.Add(incidentToState);
            incidentDC.Entry(incidentToState).State = EntityState.Added;
            incidentDC.SaveChanges();
        }
        public void CreateAttachment(IncidentDataVM model)
        {
            var incidentDC = _dbContext as TCPL_Keystone_XL_Safety_ReportsContext;

            var attachment = new Attachment()
            {
                FileLocation = model.FileLocation,
                FileExtension = model.FileExtension
            };
            incidentDC.Attachments.Add(attachment);
            incidentDC.Entry(attachment).State = EntityState.Added;
            incidentDC.SaveChanges();
            int attachmentId = attachment.Id;

            var incidentToAttach = new IncidentToAttachment()
            {
                IncidentId = model.Id,
                AttachmentId = attachmentId
            };
            incidentDC.IncidentToAttachments.Add(incidentToAttach);
            incidentDC.Entry(incidentToAttach).State = EntityState.Added;
            incidentDC.SaveChanges();
        }
        public void Update(IncidentDataVM model)
        {
            //var uploadPageDbContext = _dbContext as Upload_PageContext;
            //var user = FindUser();
            //var dateTimeUtcNow = DateTime.UtcNow;

            //var entity = uploadPageDbContext.File
            //    .FirstOrDefault(o => o.Id == model.ID);
            //if (entity == null) return;

            //uploadPageDbContext.File.Attach(entity);

            //if (entity.FileName != model.FileName) { entity.FileName = model.FileName; }
            //if (entity.FileSize != model.FileSize) { entity.FileSize = model.FileSize; }

            //// System
            //entity.ModifyUserId = user.Id;
            //entity.ModifiedDateTime = dateTimeUtcNow;

            //// One-to-one
            //{
            //    var property = uploadPageDbContext.FileProperty.FirstOrDefault(o => o.FileId == model.ID);
            //    if (property != null)
            //    {
            //        if (property.Comment != model.Comment) { property.Comment = model.Comment; }
            //    }
            //}

            //uploadPageDbContext.SaveChanges();
        }

        public void Destroy(IncidentDataVM model)
        {
            //var uploadPageDbContext = _dbContext as Upload_PageContext;

            //var entity = uploadPageDbContext.File
            //    .FirstOrDefault(o => o.Id == model.ID);
            //if (entity == null) return;

            //var user = FindUser();
            //var dateTimeUtcNow = DateTime.UtcNow;
            //entity.Deleted = true;
            //entity.ModifyUserId = user.Id;
            //entity.ModifiedDateTime = dateTimeUtcNow;

            //uploadPageDbContext.SaveChanges();
        }

        public User FindUser(string userName = null)
        {
            string UserEmail = null;
            lock (userLock)
            {
                var dbContext = _dbContext as TCPL_Keystone_XL_Safety_ReportsContext;

                var update = false;
                dynamic userInfo = null;

                if (string.IsNullOrEmpty(userName))
                {
                    userInfo = _httpContextAccessor.HttpContext.User.GetLoggedInUserInfo();
                    //  userName = userInfo.userName;
                    UserEmail = userInfo.UserEmail;
                    update = true;
                }

                var user = dbContext.Users.FirstOrDefault(u => u.Email == UserEmail);

                if (update)
                {
                    if (user != null)
                    {
                        if (!string.Equals(user.Email, userInfo.UserEmail, StringComparison.OrdinalIgnoreCase) || !string.Equals(user.FirstName, userInfo.FullName, StringComparison.OrdinalIgnoreCase))
                        {
                            user.Email = !string.IsNullOrEmpty(userInfo.UserEmail) ? userInfo.UserEmail : null;
                            user.FirstName = !string.IsNullOrEmpty(userInfo.FirstName) ? userInfo.FirstName : null;
                            user.LastName = !string.IsNullOrEmpty(userInfo.LastName) ? userInfo.LastName : null;
                            dbContext.SaveChanges();
                        }
                    }
                    else
                    {
                        var newUser = new User()
                        {
                            FirstName = !string.IsNullOrEmpty(userInfo.FirstName) ? userInfo.FirstName : null,
                            Email = !string.IsNullOrEmpty(userInfo.UserEmail) ? userInfo.UserEmail : null,
                            LastName = !string.IsNullOrEmpty(userInfo.LastName) ? userInfo.LastName : null
                        };

                        dbContext.Users.Add(newUser);
                        dbContext.Entry(newUser).State = EntityState.Added;
                        dbContext.SaveChanges();

                        user = newUser;
                    }
                }

                return user;
            }
        }
    }
}
