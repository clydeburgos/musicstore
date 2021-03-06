﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OData.Edm;
using MusicStore.API.Automapper;
using MusicStore.Data.Models;
using MusicStore.Service;
using MusicStore.Service.AzureServices;
using Newtonsoft.Json.Serialization;

namespace MusicStore.API
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                          p => p.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod());
            });

            services.AddDbContext<MusicStoreDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MusicStoreConnection")),
            (ServiceLifetime.Transient));

            services.AddOData();
            var autoMapperConfig = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperProfile());
            });
            var autoMapper = autoMapperConfig.CreateMapper();
            services.AddSingleton(autoMapper);

            services.AddMvc()
                .AddJsonOptions(options => {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

       
        }
        private static IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder(serviceProvider);
            builder.EntitySet<Artist>("Artists");
            builder.EntitySet<Track>("Tracks");
            builder.EntitySet<Customer>("Customers");
            return builder.GetEdmModel();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<BingImageSearchService>();
            builder.RegisterType<ArtistService>().As<IArtistService>();
            builder.RegisterType<TrackService>().As<ITrackService>();
            builder.RegisterType<AlbumService>().As<IAlbumService>();
            builder.RegisterType<CustomerService>().As<ICustomerService>();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc(routerBuilder => {
                routerBuilder.EnableDependencyInjection();
                routerBuilder.Expand().Filter().Select().OrderBy().MaxTop(null).Count();
                routerBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel(app.ApplicationServices));
            });
        }
    }
}
