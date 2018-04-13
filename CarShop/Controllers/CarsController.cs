using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarShop.Model;
using CarShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Controllers
{

    public class CarsController : Controller
    {
        private CarDbContext _dbContaxt;
        private IMapper _mapper;

        public CarsController(CarDbContext dbContaxt, IMapper mapper)
        {
            _dbContaxt = dbContaxt;
            _mapper = mapper;
        }

        [Route("api/[controller]")]
        public List<CarModelDataView> GetList(int skip = 0, int take = 1000, int? companyId = null)
        {
            IEnumerable<CarModel> carModels = _dbContaxt.CarModels.Include(c => c.CarCompany);

            if (companyId.HasValue)
                carModels = carModels.Where(m => m.CarCompany.Id == companyId);

            var response = carModels.OrderBy(c => c.Id)
                  .Skip(skip)
                  .Take(take)
                  .ToList();

            return _mapper.Map<List<CarModelDataView>>(response);
        }

        [Route("api/[controller]/{id}")]
        public CarModelDataView GetDetails(int id)
        {
            var carModel = _dbContaxt.CarModels.FirstOrDefault(c => c.Id == id);

            if (carModel == null)
                return null; //TODO: add middleware for exception handling

            return _mapper.Map<CarModelDataView>(carModel);
        }
    }
}
