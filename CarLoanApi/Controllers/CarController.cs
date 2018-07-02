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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        /// <summary>
        /// Sets the car to be rented out.
        /// </summary>
        /// <param name="Id">The Id of the car you wish to rent</param>
        /// <returns>returns a new list of the other available cars</returns>

        [HttpPut("SetStatus/{id}")]
        public IActionResult Update(int id)
        {
            try
            {
                return Ok(_carService.UpdateCarStatus(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Resets the data's dates back to null
        /// </summary>

        [HttpGet("Reset")]
        public IActionResult Reset()
        {
            try
            {
                _carService.ResetData();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}