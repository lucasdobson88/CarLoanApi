using System;
using CarLoanApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarLoanApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Car")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        /// <summary>
        /// Retrieves all cars that are available for rent.
        /// If there are no cars available, Call the reset endpoint to reset the data
        /// </summary>
        /// <returns>returns a list of the available cars</returns>

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_carService.GetAllCars());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Whoops! something went wrong here. how about we try again :)");
            }
        }
        /// <summary>
        /// Sets the car to be booked.
        /// </summary>
        /// <param name="id">The id of the car you wish to rent between 1 - 5</param>
        /// <returns>returns a new list of the other available cars</returns>

        [HttpPut("BookCar/{id}")]
        public IActionResult Update(int id)
        {
            try
            {
                return Ok(_carService.UpdateCarStatus(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "I would say you just entered a number that was not between 1 and 5, but i could be wrong. Please try again :)");
            }
        }

        /// <summary>
        /// Resets the data back to default
        /// </summary>

        [HttpGet("Reset")]
        public IActionResult Reset()
        {
            try
            {
                _carService.ResetData();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Resetting data shouldn't be this hard. How about we try it again :D");
            }
        }
    }
}