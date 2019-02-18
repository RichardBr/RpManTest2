using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.Processors.Security;

namespace RpMan.WebApi.Helpers
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomizedNSwag(this IApplicationBuilder app)
        {
            //app.UseSwagger(); // Serves the registered OpenAPI/Swagger documents by default on `/swagger/{documentName}/swagger.json`
            //app.UseSwaggerUi3(); // Serves the Swagger UI 3 web ui to view the OpenAPI/Swagger documents by default on `/swagger`

            //// Add OpenAPI and Swagger middlewares to serve documents and web UIs

            // URLs: 
            // - http://localhost:44309/swagger/v1/swagger.json
            // - http://localhost:44309/swagger
            // - http://localhost:44309/redoc
            // - http://localhost:44309/openapi
            // - http://localhost:44309/openapi_redoc



            // Add Swagger 2.0 document serving middleware
            app.UseSwagger(options =>
            {
                options.DocumentName = "swagger";
                options.Path = "/swagger/v1/swagger.json";
            });
            // Add web UIs to interact with the document
            app.UseSwaggerUi3(options =>
            {
                // Define web UI route
                options.Path = "/swagger";

                // Define OpenAPI/Swagger document route (defined with UseSwaggerWithApiExplorer)
                options.DocumentPath = "/swagger/v1/swagger.json";
            });
            app.UseReDoc(options =>
            {
                options.Path = "/redoc";
                options.DocumentPath = "/swagger/v1/swagger.json";
            });



            // Add OpenAPI 3.0 document serving middleware
            app.UseSwagger(options =>
            {
                options.DocumentName = "openapi";
                options.Path = "/openapi/v1/openapi.json";
            });

            // Add web UIs to interact with the document
            app.UseSwaggerUi3(options =>
            {
                options.Path = "/openapi";
                options.DocumentPath = "/openapi/v1/openapi.json";
            });
            app.UseReDoc(options =>
            {
                options.Path = "/openapi_redoc";
                options.DocumentPath = "/openapi/v1/openapi.json";
            });



            // Add Swagger UI with multiple documents
            app.UseSwaggerUi3(options =>
            {
                // Add multiple OpenAPI/Swagger documents to the Swagger UI 3 web frontend
                options.SwaggerRoutes.Add(new SwaggerUi3Route("Swagger", "/swagger/v1/swagger.json"));
                options.SwaggerRoutes.Add(new SwaggerUi3Route("Openapi (V3 only)", "/openapi/v1/openapi.json"));
                options.SwaggerRoutes.Add(new SwaggerUi3Route("Petstore", "http://petstore.swagger.io/v2/swagger.json"));

                // Define web UI route
                options.Path = "/swagger_all";
            });

            return app;
        }

    }
}
