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
            // TODO:�ڸ���5.0ʱ��ӱ��ػ������ڰ汾Blazor�����⣩
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
            services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 95;
                config.VisibleStateDuration = 3000;
            });

            services.AddRazorPages();
            services.AddServerSideBlazor();
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}