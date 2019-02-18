using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RpMan.Application.Customers.Commands.CreateCustomer;
using RpMan.Application.Customers.Queries.GetCustomersList;
using RpMan.Application.Infrastructure;
using RpMan.Application.Infrastructure.AutoMapper;
using RpMan.Application.Interfaces;
using RpMan.Common;
using RpMan.Domain.Entities;
using RpMan.Infrastructure;
using RpMan.Persistence;
using RpMan.WebApi.Filters;
using RpMan.WebApi.Helpers;


namespace RpMan.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add AutoMapper
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            // Add framework services.
            services.AddTransient<INotificationService, NotificationService>();   //TODO: see what the purpose of this 'notification' is all about
            services.AddTransient<IDateTime, MachineDateTime>();

            // Add MediatR

            // TODO: sort out this duplicate, not a problem in RpMan
            // *** not sure the purpose of line below as mediatR works without it! In fact with it the "RequestLogger.cs" fires twice!
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddMediatR(typeof(GetCustomersListQueryHandler).GetTypeInfo().Assembly);

            // Add DbContext using SQL Server Provider
            services.AddDbContext<RpManDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("RpManDatabase")));

            services
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(CustomExceptionFilterAttribute));

                    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                    options.Filters.Add(new AuthorizeFilter(policy)); // This means all controllers will have an implicit [Authorize]
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCustomerCommandValidator>());

            // Api versioning
            services.AddCustomizedApiVersioning();

            // Add OpenAPI/Swagger document
            services.AddCustomizedNSwag();

            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;   // stop Automatic model state validation
            });


            IdentityBuilder identityBuilder = services.AddIdentityCore<User>(opt =>
            {
                // below is not recommended. Just done to allow weak password during testing!
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            });
            identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(Role), identityBuilder.Services);
            identityBuilder.AddEntityFrameworkStores<RpManDbContext>();
            identityBuilder.AddRoleValidator<RoleValidator<Role>>();
            identityBuilder.AddRoleManager<RoleManager<Role>>();
            identityBuilder.AddSignInManager<SignInManager<User>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                // Add OpenAPI/Swagger middlewares
                app.UseCustomizedNSwag();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            // app.UseMvc();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
