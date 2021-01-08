using Incident_Reporting.Extensions;
using Incident_Reporting.Data.Entities;
using Incident_Reporting.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Incident_Reporting.Data
{
    public class BaseDataService
    {
        private readonly static object userLock = new object();

        protected readonly IHttpContextAccessor _httpContextAccessor;

        protected readonly DbContext _dbContext;

        public BaseDataService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BaseDataService(DbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public DbContext DbContext { get { return _dbContext; } }

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
                            user.FirstName = !string.IsNullOrEmpty(userInfo.FullName) ? userInfo.FullName : null;
                            user.LastName = !string.IsNullOrEmpty(userInfo.FullName) ? userInfo.FullName : null;
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


        public DbSet<T> GetValues<T>() where T : class
        {
            System.Reflection.PropertyInfo property = _dbContext.GetType().GetProperty(typeof(T).Name);
            return property.GetValue(_dbContext) as DbSet<T>;
        }

        public IEnumerable<object> GetValues(string T)
        {
            System.Reflection.PropertyInfo property = _dbContext.GetType().GetProperty(T);
            return property.GetValue(_dbContext) as IEnumerable<object>;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return _dbContext.SaveChanges(acceptAllChangesOnSuccess);
        }

        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return _dbContext.Entry<TEntity>(entity);
        }

        public EntityEntry Entry(object entity)
        {
            return _dbContext.Entry(entity);
        }

        public void AddRange(IEnumerable<object> entities)
        {
            _dbContext.AddRange(entities);
        }

        public void AddRange(params object[] entities)
        {
            _dbContext.AddRange(entities);
        }

        public EntityEntry Add(object entity)
        {
            return _dbContext.Add(entity);
        }

        public EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class
        {
            return _dbContext.Add(entity);
        }

        public IEnumerable<TEntity> FromSql<TEntity>(string sql) where TEntity : class
        {
            return GetValues<TEntity>().FromSqlRaw(sql);
        }

        //public int ExecuteSqlCommand(string sql)
        //{
        //    return _dbContext.Database.ExecuteSqlCommand(sql);
        //}
    }
}
