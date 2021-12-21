﻿using System.Linq;
using Ant.Platform.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Ant.Platform.Configurations
{
    internal static class SwaggerConfiguration
    {
        internal static void AddPlatformSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new SwaggerOptions(configuration);

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(t =>
                {
                    if (string.IsNullOrWhiteSpace(t.FullName))
                    {
                        return t.Name;
                    }

                    if (t.FullName.Contains('+'))
                    {
                        return t.FullName.Split(".").Last().Replace("+", "");
                    }

                    return t.Name;
                });
                c.SwaggerDoc("v1", new OpenApiInfo { Title = options.ApplicationTitle, Version = "v1" });
            });
        }

        internal static void UsePlatformSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var options = new SwaggerOptions(configuration);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // OpenAPI URL: http://localhost:5000/swagger/v1/swagger.json
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{options.ApplicationTitle} v1");
            });
        }
    }
}
