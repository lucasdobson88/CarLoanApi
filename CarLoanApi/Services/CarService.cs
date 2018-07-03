using CarLoanApi.Models;
using CarLoanApi.Repository.Interfaces;
using CarLoanApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarLoanApi.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository) {
            _carRepository = carRepository;
        }

        public IEnumerable<Car> GetAllCars() {
            var allCars = _carRepository.GetAll();
            var availableCars = allCars.Where(c => c.CarStatus.RentedDate == null || (DateTime.Now - Convert.ToDateTime(c.CarStatus.RentedDate)).TotalHours > 24).ToList();
            return availableCars;
        }

        public IEnumerable<Car> UpdateCarStatus(int id) {
            var car = _carRepository.GetCar(id);
            car.CarStatus.RentedDate = DateTime.Now;

            return _carRepository.SetStatus(car).Where(c => c.CarStatus.RentedDate == null || (DateTime.Now - Convert.ToDateTime(c.CarStatus.RentedDate)).TotalHours > 24).ToList();
        }

        public void ResetData() {
            _carRepository.ResetData();
        }
    }
}
