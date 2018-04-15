using AutoMapper;
using CarShop.Model;
using CarShop.Models;
using CarShop.Services;
using CarShop.Services.Models;
using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace CarShop
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
            services.AddMvc();

            services.AddDbContext<CarDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CarShopDB")));
            services.AddTransient<ICarOrderService, CarOrderService>();
            services.AddSingleton<IMapper>(_prepareMapper());


            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));

            var repo = log4net.LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
            log4net.LogManager.GetLogger(typeof(Program));
            services.AddSingleton<ILog>(log4net.LogManager.GetLogger(typeof(Program)));
        }

        private static IMapper _prepareMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CarCompany, CarCompanyDataView>();
                cfg.CreateMap<CarModel, CarModelDataView>();
                cfg.CreateMap<CarFeatureModel, CarFeatureDataView>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.CarFeature.Description))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CarFeature.Name))
                    .ForMember(dest => dest.CarFeatureType, opt => opt.MapFrom(src => src.CarFeature.CarFeatureType));
                cfg.CreateMap<CarOrderFeature, CarFeatureDataView>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CarFeatureModel.Id))
                   .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.CarFeatureModel.Price))
                   .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.CarFeatureModel.CarFeature.Description))
                   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CarFeatureModel.CarFeature.Name))
                   .ForMember(dest => dest.CarFeatureType, opt => opt.MapFrom(src => src.CarFeatureModel.CarFeature.CarFeatureType));

                cfg.CreateMap<OrderDataView, CarOrderData>()
                    .ForMember(dest => dest.CarModelId, opt => opt.MapFrom(src => src.CarModel.Id))
                    .ForMember(dest => dest.SelectedFeaturesIds, opt => opt.MapFrom(src => src.CarFeatures.Select(x => x.Id).ToList()));

                cfg.CreateMap<CarOrder, OrderDataView>();
            });

            return new Mapper(mapperConfig);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
          
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
