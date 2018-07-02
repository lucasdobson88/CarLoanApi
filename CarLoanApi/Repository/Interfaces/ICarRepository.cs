using CarLoanApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarLoanApi.Repository.Interfaces
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAll();
        IEnumerable<Car> SetStatus(Car model);
        Car GetCar(int id);
        void ResetData();
    }
}
