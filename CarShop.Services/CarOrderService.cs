using CarShop.Model;
using CarShop.Services.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CarShop.Services
{
    public class CarOrderService : ICarOrderService
    {
        private CarDbContext _dbContext;

        public CarOrderService(CarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CarOrder> CreateCarOrder(CarOrderData data)
        {
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

                return Convert.ToBase64String(tokenData);
            }
        }
    }
}
