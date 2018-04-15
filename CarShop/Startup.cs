using AutoMapper;
using CarShop.Model;
using CarShop.Models;
using CarShop.Services;
using CarShop.Services.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

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

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            var config = builder.Build();
            var connection = config.GetConnectionString("CarShopDB");

            services.AddDbContext<CarDbContext>(options => options.UseSqlServer(connection));
            services.AddTransient<ICarOrderService, CarOrderService>();
            services.AddSingleton<IMapper>(_prepareMapper());
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
                   .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.CarFeatureModel.CarFeature.CarFeatureType))
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
