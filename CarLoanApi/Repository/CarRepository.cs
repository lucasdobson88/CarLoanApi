using CarLoanApi.Models;
using CarLoanApi.Repository.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarLoanApi.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly string _pathToJson;

        public CarRepository(IHostingEnvironment hostingEnvironment)
        {
            _pathToJson = hostingEnvironment.ContentRootPath + "/App_Data/cars.json";
        }

        public IEnumerable<Car> GetAll()
        {
            CheckForExpiredCarDates();
            return ReadData();
        }

        public Car GetCar(int id)
        {
            var cars = ReadData();
            CheckForExpiredCarDates();
            return cars.Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Car> SetStatus(Car model)
        {
            var cars = ReadData().ToList();

            foreach (var item in cars.Where(c => c.Id == model.Id))
            {
                item.CarStatus.RentedDate = model.CarStatus.RentedDate;
            }

            return WriteData(cars);
        }

        public void ResetData() {
            CheckForExpiredCarDates(true);
        }

        private IEnumerable<Car> ReadData()
        {
            return JsonConvert.DeserializeObject<IEnumerable<Car>>(File.ReadAllText(_pathToJson));
        }

        private IEnumerable<Car> WriteData(IEnumerable<Car> model)
        {
            File.WriteAllText(_pathToJson, JsonConvert.SerializeObject(model));
            return ReadData();
        }

        private bool CheckForExpiredCarDates(bool resetData = false) {
            var cars = ReadData();
            var updateRequired = false;
            foreach (var item in cars) {
                if ((DateTime.Now - Convert.ToDateTime(item.CarStatus.RentedDate)).TotalHours > 24)
                {
                    item.CarStatus.RentedDate = null;
                    updateRequired = true;
                }
                else if (resetData) {
                    item.CarStatus.RentedDate = null;
                }
            }
            if (updateRequired || resetData) {
                WriteData(cars);
            }
            return updateRequired;
        }
    }
}
