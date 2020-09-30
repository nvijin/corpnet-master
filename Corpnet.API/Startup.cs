using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corpnet.Data;
using Corpnet.Data.Interfaces;
using Corpnet.Data.Model;
using Corpnet.Data.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Corpnet.Services.Interfaces;
using Corpnet.Services.Services;
using Corpnet.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using Corpnet.API.Controllers;
using Microsoft.AspNetCore.Server.IISIntegration;

namespace Corpnet.API
{
    public class Startup
    {
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();

            //Auto Mapper Default
            //services.AddAutoMapper(typeof(Startup));

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Data
            services.AddDbContextPool<DataContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Default"]));
            services.AddScoped<IRoles, RolesRepo>();
            services.AddScoped<IMenuService, MenuService>();

            services.AddScoped<IDirectory, DirectoryRepo>();
            services.AddScoped<IDirectoryService, DirectoryService>();

            services.AddScoped<IDocument, DocumentRepo>();
            services.AddScoped<IDocumentService, DocumentService>();

            services.AddScoped<IGeneric, GenericRepo>();
            services.AddScoped<IGenericService, GenericService>();

            services.AddScoped<IFavourite, FavouriteRepo>();
            services.AddScoped<IFavouriteService, FavouriteService>();

            services.AddScoped<IDocumentUpload, DocumentUploadRepo>();
            services.AddScoped<IDocumentUploadService, DocumentUploadService>();

            services.AddScoped<IRecentLinks, RecentLinksRepo>();
            services.AddScoped<IRecentLinksService, RecentLinksService>();

            services.AddScoped<IAdmin, AdminRepo>();
            services.AddScoped<IAdminService, AdminService>();

            services.AddScoped<IAdminDirectoryAccess, AdminDirectoryAccessRepo>();
            services.AddScoped<IAdminDirectoryAccessService, AdminDirectoryAccessService>();

            services.AddScoped<IErrorLog, ErrorlogRepocs>();
            services.AddScoped<IErrorlogService, ErrorlogService>();

            //CORS policy
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));


            services.AddControllers();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddAuthentication(IISDefaults.AuthenticationScheme);

            services.AddAuthentication(AzureADDefaults
                .AuthenticationScheme)
                .AddAzureAD(options => Configuration.Bind
                ("AzureAD", options));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();
            loggerFactory.AddFile("Logs/log-{Date}.log");


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MyPolicy");

            //app.UseCors(builder => builder.WithOrigins("https://localhost:44306")
            //                  .AllowAnyMethod()
            //                  .WithHeaders("authorization", "accept", "content-type", "origin"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Corpnet V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseExceptionHandler(
                            options =>
                            {
                                options.Run(async context =>
                                {
                                    context.Response.StatusCode = 500;//Internal Server Error
                                    context.Response.ContentType = "application/json";
                                    await context.Response.WriteAsync("Internal Server Error: We are woirking on it");
                                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                                    if (ex != null)
                                    {
                                        _logger.LogError($"Something went wrong: {ex.Error.Message }");
                                    }
                                });
                            }
                        );

            ////Test AD auth
            //app.Use(async (context, next) =>
            //{
            //    // Create a user on current thread from provided header
            //    if (context.Request.Headers.ContainsKey("X-MS-CLIENT-PRINCIPAL-ID"))
            //    {
            //        // Read headers from Azure
            //        var azureAppServicePrincipalIdHeader = context.Request.Headers["X-MS-CLIENT-PRINCIPAL-ID"][0];
            //        var azureAppServicePrincipalNameHeader = context.Request.Headers["X-MS-CLIENT-PRINCIPAL-NAME"][0];
            //        Console.Write(azureAppServicePrincipalNameHeader);

            //        #region extract claims via call /.auth/me
            //        //invoke /.auth/me
            //        var cookieContainer = new CookieContainer();
            //        HttpClientHandler handler = new HttpClientHandler()
            //        {
            //            CookieContainer = cookieContainer
            //        };
            //        string uriString = $"{context.Request.Scheme}://{context.Request.Host}";
            //        foreach (var c in context.Request.Cookies)
            //        {
            //            cookieContainer.Add(new Uri(uriString), new Cookie(c.Key, c.Value));
            //        }
            //        string jsonResult = string.Empty;
            //        using (HttpClient client = new HttpClient(handler))
            //        {
            //            var res = await client.GetAsync($"{uriString}/.auth/me");
            //            jsonResult = await res.Content.ReadAsStringAsync();
            //        }

            //        //parse json
            //        var obj = JArray.Parse(jsonResult);
            //        string user_id = obj[0]["user_id"].Value<string>(); //user_id
            //        Console.Write(user_id);

            //        // Create claims id
            //        List<Claim> claims = new List<Claim>();
            //        foreach (var claim in obj[0]["user_claims"])
            //        {
            //            claims.Add(new Claim(claim["typ"].ToString(), claim["val"].ToString()));
            //        }

            //        // Set user in current context as claims principal
            //        var identity = new GenericIdentity(azureAppServicePrincipalIdHeader);
            //        identity.AddClaims(claims);
            //        #endregion

            //        // Set current thread user to identity
            //        context.User = new GenericPrincipal(identity, null);
            //    };

            //    await next.Invoke();
            //});
        }
    }
}
