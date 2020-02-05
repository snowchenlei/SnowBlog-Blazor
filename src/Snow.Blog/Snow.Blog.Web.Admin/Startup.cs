using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MatDemo.Data;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Snow.Blog.Service.Bloggers;
using Snow.Blog.IRepository.Bloggers;
using Snow.Blog.SqlServer;
using Snow.Blog.Service.Categories;
using Snow.Blog.IDAL.Categories;
using System.Reflection;
using System.Runtime.Loader;
using AutoMapper;
using MatBlazor;
using Snow.Blog.Service.Enums;
using Snow.Blog.Web.Admin.Middleware;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.ResponseCompression;

namespace Snow.Blog.Web.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // TODO:在更新5.0时添加本地化（现在版本Blazor有问题）
            var application = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("Snow.Blog.Service"));
            //var web = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("Snow.Blog.Web"));
            services.AddAutoMapper(application);
            services.AddTransient<IEnumService, EnumService>();
            services.AddTransient<IBloggerService, BloggerService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IBloggerRepository, BloggerRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddScoped<HttpClient>();
            services.AddSingleton<WeatherForecastService>();
    //        services.AddLogging(builder => builder
    //    .AddBrowserConsole() // Add Blazor.Extensions.Logging.BrowserConsoleLogger
    //    .SetMinimumLevel(LogLevel.Trace)
    //);
            services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 95;
                config.VisibleStateDuration = 3000;
            });
            var webCore = Assembly.Load(new AssemblyName("Snow.Blog.Web.Core")); //类库的程序集名称

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DateFormatString = "yyyy/MM/dd HH:mm:ss";
                })
                .AddApplicationPart(webCore);
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            services.AddRazorPages();
            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression(); // This must be before the other Middleware if that manipulates Response

            app.UseCustomerExceptionHandler();
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //}

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();

                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}