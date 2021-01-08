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
  
    public class IncidentDataService :  IDataService
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
            //var uploadPageDbContext = _dbContext as Upload_PageContext;
            //var user = FindUser();
            //var dateTimeUtcNow = DateTime.UtcNow;

            //var entity = new File();

            //entity.FileName = model.FileName;
            //entity.FileSize = model.FileSize;
            //// Calculated
            //entity.CreateUserId = user.Id;
            //entity.CreatedDateTime = entity.ModifiedDateTime = dateTimeUtcNow;
            //// One-to-one
            //{
            //    var property = new FileProperty()
            //    {
            //        File = entity,
            //        RowCount = model.RowCount,
            //        Comment = model.Comment
            //    };
            //    uploadPageDbContext.FileProperty.Add(property);
            //    uploadPageDbContext.Entry(property).State = EntityState.Added;
            //}
            //// One-to-many
            //{
            //    var currentIndex = 0d;
            //    while (currentIndex < model.FileContent.Length)
            //    {
            //        var endIndex = currentIndex + Global.MaxContentByteLength - 1;
            //        endIndex = endIndex > model.FileContent.Length - 1 ? model.FileContent.Length - 1 : endIndex;
            //        var content = new FileContent()
            //        {
            //            File = entity,
            //            Content = model.FileContent.SubArray(currentIndex, endIndex)
            //        };
            //        uploadPageDbContext.FileContent.Add(content);
            //        uploadPageDbContext.Entry(content).State = EntityState.Added;
            //        currentIndex += Global.MaxContentByteLength;
            //    }
            //}

            //uploadPageDbContext.File.Add(entity);
            //uploadPageDbContext.Entry(entity).State = EntityState.Added;

            //uploadPageDbContext.SaveChanges();


            ////if (templateName == "GetList")
            ////{
            ////    MocListViewModel model = _emailDataService.GetMocList() as MocListViewModel;

            ////    msg.Subject = "MOC List";
            ////    var toAddress = model.ToAddress;
            ////    msg.To.Add(toAddress);
            ////    string view = "~/EmailTemplates/" + templateName;
            ////    var htmlBody = await RenderPartialViewToString($"{view}.cshtml", model);

            ////    msg.Body = htmlBody;
            ////    msg.IsBodyHtml = true;

            ////    using (var smtp = new SmtpClient(email_smtp))
            ////    {
            ////        smtp.UseDefaultCredentials = true;
            ////        smtp.EnableSsl = true;
            ////        await smtp.SendMailAsync(msg);
            ////    }
            ////}
            //model.ID = (int)entity.Id;
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
