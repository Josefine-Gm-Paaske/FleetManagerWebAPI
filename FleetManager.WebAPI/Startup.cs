using FleetManager.WebAPI.Data;
using FleetManager.WebAPI.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FleetManager.WebAPI
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
            // TODO: (Step 2) change the call to the dao factory so it recieves the data context for the SQL Server database
            /**
             * services.AddScoped(s => DaoFactory.Create<Car>(MemoryDataContext.Instance)); og 
             * services.AddScoped(s => DaoFactory.Create<Location>(MemoryDataContext.Instance)); er implementeret som singleton
             * Der er iøvrigt lavet injection!
             * De er skrevet som singleton, men det er ikke god praksis i WEB Application
             */
            // services.AddScoped(s => DaoFactory.Create<Car>(MemoryDataContext.Instance));
            // services.AddScoped(s => DaoFactory.Create<Location>(MemoryDataContext.Instance));

            //Når vi skriver "new", så laves der en hård kobling, men det må der gerne laves en hård kobling i samme assembly
            services.AddScoped(s => DaoFactory.Create<Car>(new SQLServerDataContext()));
            services.AddScoped(s => DaoFactory.Create<Location>(new SQLServerDataContext()));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                //Version 1 af webAPI, så her kan tilføjes flere versioner
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FleetManager.WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FleetManager.WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
