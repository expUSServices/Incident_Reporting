
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Incident_Reporting.Extensions
{
    public static class UserManagerExtensions
    {
        public static string GetCurrentUser(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetLoggedInUserName(this ClaimsPrincipal user)
        {
            var loggedInName = user.FindFirst("name")?.Value;
            if (string.IsNullOrEmpty(loggedInName))
            {
                var firstName = user.FindFirst(ClaimTypes.GivenName)?.Value;
                var lastName = user.FindFirst(ClaimTypes.Surname)?.Value;
                if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                    loggedInName = firstName + " " + lastName;
                else
                    loggedInName = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            return loggedInName;
        }

        public static dynamic GetLoggedInUserInfo(this ClaimsPrincipal user)
        {
            var userName = string.Empty;

            if (string.IsNullOrEmpty(userName))
            {
                userName = user.GetCurrentUser();
                if (string.IsNullOrEmpty(userName))
                    userName = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            var fullName = user.FindFirst("name")?.Value;

            var userEmail = user.FindFirst("email")?.Value;

            var firstName = user.FindFirst("givenname")?.Value;

            var lastName = user.FindFirst("surname")?.Value;
            if (!userEmail.IsValidEmail())
            {
                // try email claim type
                userEmail = user.FindFirst(ClaimTypes.Email)?.Value;
                if (!userEmail.IsValidEmail())
                {
                    // try upn
                    userEmail = user.FindFirst(ClaimTypes.Upn)?.Value;
                    // try name
                    if (!userEmail.IsValidEmail())
                        userEmail = user.FindFirst(ClaimTypes.Name)?.Value;
                }
            }
            if (string.IsNullOrEmpty(fullName))
            {

                if (string.IsNullOrEmpty(fullName))
                {
                    foreach (var claim in user.Claims.ToList())
                    {
                        if (claim.Type == "fullName")
                        {
                            fullName = claim.Value;
                        }
                        
                    }
                }
                    //userName = user.FindFirst(user.Claims.GetType['fullName'].Name)?.Value;
            }
          

            if (string.IsNullOrEmpty(firstName))
            {

                if (string.IsNullOrEmpty(firstName))
                {
                    foreach (var claim in user.Claims.ToList())
                    {
                        if (claim.Type == "firstName")
                        {
                            firstName = claim.Value;
                        }
                        if (claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/firstName")
                        {
                            firstName = claim.Value;
                        }
                        else if(claim.Type == "preferred_username")
                        {
                            firstName = claim.Value;
                        }
                        else if (claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")
                        {
                            firstName = claim.Value;
                        }
                        else if (claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/given_name")
                        {
                            firstName = claim.Value;
                        }
                        else if (claim.Type == "given_name")
                        {
                            firstName = claim.Value;
                        }
                        else if (claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/firstName")
                        {
                            firstName = claim.Value;
                        }
                        //else
                        //{
                        //    firstName = "no first name";
                        //}
                    }

                  
                }

               
                //userName = user.FindFirst(user.Claims.GetType['fullName'].Name)?.Value;
            }
            if (string.IsNullOrEmpty(lastName))
            {

                if (string.IsNullOrEmpty(lastName))
                {
                    foreach (var claim in user.Claims.ToList())
                    {
                        
                        if (claim.Type == "lastName")
                        {
                            lastName = claim.Value;
                        }

                       else if (claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/lastName")
                        {
                            lastName = claim.Value;
                        }
                        else if (claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")
                        {
                            lastName = claim.Value;
                        }
                        else if (claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/family_name")
                        {
                            lastName = claim.Value;
                        }
                        //else 
                        //{
                        //    lastName = "no last name";
                        //}
                    }
                }
                //userName = user.FindFirst(user.Claims.GetType['fullName'].Name)?.Value;
            }
            return new { UserName = userName, FullName = fullName, UserEmail = userEmail,FirstName= firstName,LastName= lastName };
        }
        //public static DateTime ToTimeZoneTime(this DateTime time, TimeZoneInfo tzi)
        //{
        //    return TimeZoneInfo.ConvertTimeFromUtc(time, tzi);
        //}
    }
}
