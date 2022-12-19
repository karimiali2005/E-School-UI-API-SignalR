using System;
using System.Globalization;
using Es.DataLayerCore.Context;
using ESchool.AppServer;
using ESchool.Need.MiddleWare;
using ESchool.Server;
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


namespace ESchool
{
    public class Startup
    {
        public static string _connection;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.GetSection("ConnectionStrings").Bind(ESchool.Need.SettingContext.ConnectionStrings.Instance);
            Configuration.GetSection("AdminPanel").Bind(ESchool.Need.SettingContext.AdminPanel.Instance);
            Configuration.GetSection("RoomChatSatus").Bind(ESchool.Need.SettingContext.RoomChatSatus.Instance);
            Configuration.GetSection("PathStoreFiles").Bind(ESchool.Need.SettingContext.PathStoreFiles.Instance);
            Configuration.GetSection("UploadSize").Bind(ESchool.Need.SettingContext.UploadSize.Instance);
            Configuration.GetSection("PathStoreProfileImage").Bind(ESchool.Need.SettingContext.PathStoreProfileImage.Instance);
        
            //Elmah
           /*services.AddElmah();
            services.AddElmah(options => options.Path = "~/ESchoolElmah");
            services.AddElmah(options =>
            {
                options.CheckPermissionAction = context => context.User.Identity.IsAuthenticated;
            });
            services.AddElmah<XmlFileErrorLog>(options =>
            {
                options.LogPath = "~/App_Data/ErrorsLog"; // OR options.LogPath = "с:\errors";
            });*/
            //


            services.AddHttpContextAccessor();

            services.AddControllersWithViews();

            services.AddMvc().AddSessionStateTempDataProvider();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMvc(options => options.Filters.Add(typeof(Need.AuthorizationFilter)));
            /*services.AddMvc(options => options.Filters.Add(typeof(Need.CutomFilter.ElmahRequestValidationErrorFilter)));*/
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(3);
            });

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

            services.AddAuthentication(option =>
            {
                option.DefaultScheme = "AccountAuth";
            })
            .AddCookie("AccountAuth", "AccountAuth", options =>
            {
                options.Cookie.Name = "IdentityOverviewESchool";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Accounts/Login");
                options.LogoutPath = "/Accounts/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
            })
            .AddCookie("AdminAuth", "AdminAuth", options =>
            {
                options.Cookie.Name = "IdentityOverviewESchool";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Panel/Admin");
                options.LogoutPath = "/Panel/Admin/Logout";
                options.AccessDeniedPath = "/Panel/Admin/AccessDenied";
            });


            #region DataBase Context

            services.AddDbContext<ESchoolContext>(options =>
            {
                string con = Configuration.GetConnectionString("DefaultConnectionString");
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"));
            }
            );

            #endregion
            #region IoC

            services.AddTransient<IHomeWorkService, HomeWorkService>();
            services.AddTransient<IRoomChatService, RoomChatService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IExamService, ExamService>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<IPreRegistrationService, PreRegistrationService>();
            services.AddTransient<IGalleryService, GalleryService>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IIntroductionService, IntroductionService>();
            services.AddTransient<IReportCardService, ReportCardService>();
            services.AddTransient<IFinancialService, FinancialService>();
            services.AddTransient<IDisciplineService, DisciplineService>();
            services.AddTransient<ICeremonyService, CeremonyService>();
            services.AddTransient<IMessageService, MessageService>();

            #endregion

            #region SignalR
            services.AddSignalR();
            services.AddSingleton<IChatGroupSignalService, ChatGroupSignalService>();

            #endregion

            _connection = Configuration.GetConnectionString("DefaultConnectionString");

            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Base/Error");
                app.UseHsts();
            }
          

            app.UseSession();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
            app.UseStaticHttpContext();

            /*app.UseWebSockets(AppServer.wsConfig.GetOptions());
            app.Use(async (context, next) =>
            {
                if(context.WebSockets.IsWebSocketRequest)
                {
                    if(context.Request.Path == AppServer.wsConfig.wsChat)
                    {
                        wsManager manager = new wsManager();
                        await manager.ListenAcceptAsync(context);
                    }
                }
                else
                {
                    await next();
                }

            });*/

            app.UseOnlineUsers();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            /*app.UseElmah();*/
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Student}/{action=Index}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();

            });


           

            //app.UseMiddleware<EndRequestMiddleware>();


            //#region SignalR

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //    endpoints.MapHub<ChatGroupHub>("/chatGroupHub");
            //});

            //#endregion
            //CultureInfo culture = new CultureInfo("fa-IR");
            //app.Use(async (context, next) =>
            //{
            //    CultureInfo.CurrentCulture = culture;
            //    CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture;

            //    await next.Invoke();
            //});


        }
    }
}
