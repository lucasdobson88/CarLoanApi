using CarLoanApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarLoanApi.Services.Interfaces
{
    public interface ICarService
    {
        IEnumerable<Car> GetAllCars();
        IEnumerable<Car> UpdateCarStatus(int id);
        void ResetData();
    }
}
