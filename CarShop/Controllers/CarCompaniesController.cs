using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarShop.Model;
using CarShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Controllers
{
    [Route("api/[controller]")]
    public class CarCompaniesController : Controller
    {
        private CarDbContext _dbContaxt;
        private IMapper _mapper;

        public CarCompaniesController(CarDbContext dbContaxt, IMapper mapper)
        {
            _mapper = mapper;
            _dbContaxt = dbContaxt;
        }


        // GET: api/CarCompany
        [HttpGet]
        public List<CarCompanyDataView> Get()
        {
            return _mapper.Map<List<CarCompanyDataView>>(_dbContaxt.CarCompanies.ToList());
        }
    }
}
