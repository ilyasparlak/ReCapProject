using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarImageDal : EfEntityRepositoryBase<CarImage, CarRentalContext>, ICarImageDal
    {
        public List<CarImageDto> GetCarImageByCarId(int id)
        {
            using (CarRentalContext context = new CarRentalContext())
            {


                var result = from ca in context.Cars
                             join b in context.Brands
                             on ca.BrandId equals b.BrandId
                             join co in context.Colors
                             on ca.ColorId equals co.ColorId
                             join ci in context.CarImages
                             on ca.CarId equals ci.CarId
                             where ca.CarId == id

                             select new CarImageDto
                             {
                                 CarId = ca.CarId,
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 ModelYear = ca.ModelYear,
                                 DailyPrice = ca.DailyPrice,
                                 Description = ca.Description,
                                 Date = ci.Date,
                                 ImagePath = ci.ImagePath
                             };
                return result.ToList();
            }
        }
    }
}
