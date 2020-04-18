using DAL._Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WEB._Core.DI;
using WEB._Core.Logging;

namespace WEB {

    public class Startup {

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Construtor
        /// </summary>
        public Startup(IConfiguration configuration) {

            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            Log.Logger = LoggerDefault.createLogger(Configuration);

            StartupDI.register(services, Configuration);
            
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {

            if (env.IsDevelopment()) {

                app.UseDeveloperExceptionPage();

            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseExceptionHandler("/errors/500");

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
