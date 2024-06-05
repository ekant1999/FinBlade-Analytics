using api.Modles;
using api.Dtos;
using api.Dtos.Comment;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Claims;

namespace api.extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
            var userName = claimsPrincipal.Claims.SingleOrDefault(x=>x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
            return userName;
        }
    }
}