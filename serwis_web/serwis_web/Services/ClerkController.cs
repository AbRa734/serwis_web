using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace serwis_web.Services;
[Route("clerk-callback")]
public class ClerkCallbackController : Controller
{
    [HttpGet]
    public IActionResult Callback()
    {
        string token = null;
        
        token = Request.Query["__clerk_jwt"].ToString();
        
        if (string.IsNullOrEmpty(token))
            token = Request.Query["__clerk_handshake"].ToString();
        
        if (string.IsNullOrEmpty(token) && Request.Cookies.TryGetValue("__clerk_db_jwt", out string cookieToken))
            token = cookieToken;
        
        Console.WriteLine($"Clerk token: {token}");
        
        if (string.IsNullOrEmpty(token))
            return BadRequest("Missing token");

        try 
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            
            var email = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
            
            if (string.IsNullOrEmpty(email))
                email = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            if (string.IsNullOrEmpty(email))
                email = jwtToken.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
                
            if (!string.IsNullOrEmpty(email))
            {
                HttpContext.Session.SetString("email", email);
            }
            else
            {
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == "id" || c.Type == "user_id")?.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    HttpContext.Session.SetString("email", userId);
                }
            }
        }
        catch (Exception ex)
        {
            // ignored
        }

        return Redirect("/");
    }
}