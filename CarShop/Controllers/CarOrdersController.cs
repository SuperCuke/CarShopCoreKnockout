using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarShop.Model;
using CarShop.Models;
using CarShop.Services;
using CarShop.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Controllers
{
   
    public class CarOrdersController : Controller
    {
        private CarDbContext _dbContext;
        private IMapper _mapper;
        private ICarOrderService _carOrderService;

        public CarOrdersController(CarDbContext dbContext, IMapper mapper, ICarOrderService carOrderService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _carOrderService = carOrderService;
        }


        // GET: api/CarCompany
        [Route("api/[controller]/{id}")]
        [HttpGet]
        public OrderDataView Get(int id, string secureCode)
        {
            var order = _dbContext.CarOrders
                 .Include(o => o.CarModel)
                    .ThenInclude(m => m.CarCompany)
                 .Include(o => o.CarFeatures)
                    .ThenInclude(f => f.CarFeatureModel)
                      .ThenInclude(cf => cf.CarFeature)
                 .FirstOrDefault(o => o.Id == id && o.SecureCode == secureCode);

            if (order == null)
                throw new ArgumentException("Order not found");

            return _mapper.Map<OrderDataView>(order);
        }

        // GET: api/CarCompany
        [Route("api/[controller]")]
        [HttpPost]
        public async Task<RedirectModel> Post([FromBody] OrderDataView order)
        {
            var result = await _carOrderService.CreateCarOrder(_mapper.Map<CarOrderData>(order));
            return new RedirectModel() { Uri = $"/order-details/{result.Id}/?secureCode={result.SecureCode}" };
        }
    }
}
