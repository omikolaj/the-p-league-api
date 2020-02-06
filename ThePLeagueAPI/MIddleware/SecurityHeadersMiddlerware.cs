﻿using Microsoft.AspNetCore.Builder;
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
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            // Microsoft.Net.Http.Headers.HeaderNames.Vary
            context.Response.Headers.Add("Referrer-Policy", "no-referrer");
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-Frame-Options", "DENY");

            context.Response.Headers.Add(
        "Content-Security-Policy",
        "default-src 'self'; " +
        "img-src 'self' myblobacc.blob.core.windows.net; " +
        "font-src 'self'; " +
        "style-src 'self'; " +
        "script-src 'self' 'nonce-KIBdfgEKjb34ueiw567bfkshbvfi4KhtIUE3IWF' " +
        " 'nonce-rewgljnOIBU3iu2btli4tbllwwe'; " +
        "frame-src 'self';" +
        "connect-src 'self';");



            await _next(context);

        }
    }

    public static class ApplicationBuilderExtensions
    {
        public static void UseSecurityHeaders(this IApplicationBuilder app)
        {
            app.UseMiddleware<SecurityHeadersMiddleware>();
        }
    }
}