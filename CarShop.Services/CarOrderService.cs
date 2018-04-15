using CarShop.Model;
using CarShop.Services.Models;
using log4net;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CarShop.Services
{
    public class CarOrderService : ICarOrderService
    {
        private CarDbContext _dbContext;
        private ILog _logger;

        public CarOrderService(CarDbContext dbContext, ILog logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<CarOrder> CreateCarOrder(CarOrderData data)
        {
            _logger.Info("Creating car order");
            _logger.Info(JsonConvert.SerializeObject(data));

            var carModel = _dbContext.CarModels.FirstOrDefault(c => c.Id == data.CarModelId);

            if (carModel == null)
                throw new ArgumentException("invalid car model id");

            var features = _dbContext.CarFeatureModels.Where(cf => data.SelectedFeaturesIds.Contains(cf.Id));

            var order = new CarOrder()
            {
                CarModel = carModel,
                Email = data.Email,
                FullName = data.FullName,
                SecureCode = _generateSecureToken(),
                CarFeatures = features.Select(f => new CarOrderFeature() { CarFeatureModel = f }).ToList()
            };

            _dbContext.CarOrders.Add(order);

            await _dbContext.SaveChangesAsync();
            return order;
        }

        private string _generateSecureToken()
        {
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);

                var rez=  Convert.ToBase64String(tokenData);
                rez = rez.Replace("/", "").Replace("-", "").Replace("+", "").Replace("=", "");
                return rez;
            }
        }
    }
}
