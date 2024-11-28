using System.Security.Claims;

namespace TeddyCourseYT.Extensions;

public static class ClainsExtension
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        return user.Claims
            .SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"))
            .Value;
    }
}