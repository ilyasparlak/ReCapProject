using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car> { 
                new Car{CarId=1, BrandId=1, ColorId=1, Description="Kalite", ModelYear=new DateTime(2021)},
                new Car{CarId=2, BrandId=1, ColorId=2, Description="Stil", ModelYear=new DateTime(2020)},
                new Car{CarId=3, BrandId=2, ColorId=2, Description="Konfor", ModelYear=new DateTime(2021)},
                new Car{CarId=4, BrandId=2, ColorId=2, Description="Hız", ModelYear=new DateTime(2021)},
                new Car{CarId=5, BrandId=3, ColorId=3, Description="Dayanıklılık", ModelYear=new DateTime(2020)}
            };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c=>c.CarId==car.CarId);
            _cars.Remove(carToDelete);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetById(int id)
        {
            return _cars.Where(c => c.CarId == id).ToList();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c=>c.CarId==car.CarId);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.BrandId;
            carToUpdate.Description = car.Description;
            carToUpdate.ModelYear = car.ModelYear;
        }
    }
}
