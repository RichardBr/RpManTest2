using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.SwaggerGeneration.Processors.Security;

namespace RpMan.WebApi.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomizedApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(
                options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);

                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;
                });
            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });

            return services;
        }

        public static IServiceCollection AddCustomizedNSwag(this IServiceCollection services)
        {
            var operationSecurityScopeProcessor = new OperationSecurityScopeProcessor("custom-auth");
            var securityDefinitionAppender = new SecurityDefinitionAppender("custom-auth", new SwaggerSecurityScheme
            {
                Type = SwaggerSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = SwaggerSecurityApiKeyLocation.Header,
                Description = "Copy 'Bearer ' + valid JWT token into field"
            });

            var swaggerDocument = new SwaggerDocument
            {
                Info =
                {
                    Title = "RpMan",
                    Description = "A simple ASP.NET Core web API",
                    TermsOfService = "None",
                    Contact = new NSwag.SwaggerContact
                    {
                        Name = "Simon B",
                        Email = string.Empty,
                        Url = "https://example.com/license"
                    },
                    License = new NSwag.SwaggerLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    }
                }
            };

            // got NSwag code below from "https://github.com/RSuter/NSwag/blob/master/src/NSwag.Sample.NETCore20/Startup.cs"
            // Adds the NSwag services
            services
                // Register a Swagger 2.0 document generator
                .AddSwaggerDocument(document =>
                {
                    document.DocumentName = "swagger";
                    //document.ApiGroupNames = new[] { "v1","v2" }; // includes all version when null

                    // Add operation security scope processor
                    document.OperationProcessors.Add(operationSecurityScopeProcessor);

                    // Add custom document processors, etc.
                    document.DocumentProcessors.Add(securityDefinitionAppender);

                    // Post process the generated document
                    document.PostProcess = d =>
                    {
                        d.Info.Title = swaggerDocument.Info.Title;
                        d.Info.Description = swaggerDocument.Info.Description;
                        d.Info.TermsOfService = swaggerDocument.Info.TermsOfService;
                        d.Info.Contact = swaggerDocument.Info.Contact;
                        d.Info.License = swaggerDocument.Info.License;
                    };
                })
                // Register an OpenAPI 3.0 document generator
                .AddOpenApiDocument(document =>
                {
                    document.DocumentName = "openapi";
                    document.ApiGroupNames = new[] { "v3" };

                    // Add operation security scope processor
                    document.OperationProcessors.Add(operationSecurityScopeProcessor);

                    // Add custom document processors, etc.
                    document.DocumentProcessors.Add(securityDefinitionAppender);

                    // Post process the generated document
                    document.PostProcess = d =>
                    {
                        d.Info.Title = swaggerDocument.Info.Title;
                        d.Info.Description = swaggerDocument.Info.Description;
                        d.Info.TermsOfService = swaggerDocument.Info.TermsOfService;
                        d.Info.Contact = swaggerDocument.Info.Contact;
                        d.Info.License = swaggerDocument.Info.License;
                    };
                })
                ;

            return services;
        }






    }
}
