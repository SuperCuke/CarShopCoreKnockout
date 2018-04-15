using CarShop.Model;
using CarShop.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Services
{
    public interface ICarOrderService
    {
        Task<CarOrder> CreateCarOrder(CarOrderData data);
    }
}
