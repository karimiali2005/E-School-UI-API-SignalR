using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElmahCore;
using ElmahCore.Mvc;
using Es.DataLayerCore.Context;
using ESchool.Api.Helpers;
using ESchool.Api.Need;
using EsServiceCore.Interface;
using EsServiceCore.Service;
using EsServiceCore.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ESchool.Api
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
            Configuration.GetSection("RoomChatSatus").Bind(SettingContext.RoomChatSatus.Instance);
            Configuration.GetSection("PathStoreFiles").Bind(SettingContext.PathStoreFiles.Instance);
            Configuration.GetSection("PathStoreProfileImage").Bind(SettingContext.PathStoreProfileImage.Instance);

            #region swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Mainswg", new OpenApiInfo { Title = "My API" });
            });

            #endregion
            #region Elmah
            services.AddElmah();
            services.AddElmah<XmlFileErrorLog>(options =>
            {
                options.LogPath = "~/ErrorsLog"; // OR options.LogPath = "с:\errors";
            });
            #endregion
            services.AddControllers();

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            #region DataBase Context

            services.AddDbContext<ESchoolContext>(options =>
            {
                string con = Configuration.GetConnectionString("DefaultConnectionString");
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"));
            }
            );

            #endregion

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            #region IoC

            services.AddTransient<IHomeWorkService, HomeWorkService>();
            services.AddTransient<IRoomChatService, RoomChatService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IVersioningService, VersioningService>();


            #endregion

            #region LargFile

           

            services.Configure<FormOptions>(x =>
            {
                x.MultipartBodyLengthLimit = 2147483648;
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });
            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = int.MaxValue;
            });
            #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseElmah();
            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/Mainswg/swagger.json", "My API V1");
            });
            #endregion

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
