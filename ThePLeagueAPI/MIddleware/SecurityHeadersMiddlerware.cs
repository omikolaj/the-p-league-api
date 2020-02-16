using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThePLeagueAPI.MIddleware
{
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {      
            //context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Referrer-Policy", "no-referrer");
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-Frame-Options", "DENY");            
            context.Response.Headers.Add("Content-Security-Policy",
                                        "default-src 'none'; " +
                                        "base-uri 'self'; " +
                                        "frame-ancestors 'none'; " +
                                        "child-src 'none'; " +
                                        "img-src 'self' data: https://res.cloudinary.com https://via.placeholder.com/300.png/09f/fff; " +
                                        "form-action 'none'; " +
                                        "media-src 'none'; " +
                                        "object-src 'none'; " +
                                        "font-src 'self' https://fonts.gstatic.com; " +
                                        "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com; " +                                        
                                        "script-src 'self'; " +
                                        "frame-src 'self'; " +
                                        "connect-src 'self';");

            await _next(context);
        }
    }

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SecurityHeadersMiddleware>();
        }
    }
}
