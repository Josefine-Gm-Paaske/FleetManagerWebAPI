using FleetManager.WebAPI.Data;
using FleetManager.WebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FleetManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IDao<Car> _carDao;
        private readonly IDao<Location> _locationDao;

        public CarsController(IDao<Car> carDao, IDao<Location> locationDao)
        {
            _carDao = carDao;
            _locationDao = locationDao;
        }

        // GET api/cars
        [HttpGet]
        public IEnumerable<CarDto> Get()
        {
            return _carDao.Read().Select(c => c.Map());
        }

        // GET api/cars/5
        [HttpGet("{id}")]
        public CarDto Get(int id)
        {
            return _carDao.Read(c => c.Id == id).Single().Map();
        }

        // POST api/cars
        [HttpPost]
        public void Post(CarDto dto)
        {
            Car car = dto.Map();
            int? locationId = DtoEntityHelpers.GetIdFromHref(dto.LocationHref);
            if (locationId.HasValue)
            {
                car.Location = _locationDao.Read(l => l.Id == locationId.Value).Single();
            }
            Car newCar = _carDao.Create(car);

            if (newCar.Id > 0)
            {
                Response.StatusCode = StatusCodes.Status201Created;
                Response.Headers["Location"] = @$"/api/locations/{newCar.Id}"; //Der må ikke returneres noget, derved skrives denne kode
            }
            else
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }

        // PUT api/cars/5
        [HttpPut("{id}")]
        public void Put(int id, CarDto dto)
        {
            Car car = dto.Map();
            car.Id = id;
            _carDao.Update1(car);
        }

        // DELETE api/cars/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Car car = _carDao.Read(c => c.Id == id).Single();
            _carDao.Delete(car);
        }
    }
}
