﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IDataResult<List<CarImage>> GetAll();
        IDataResult<CarImage> Get(int id);
        IDataResult<List<CarImage>> GetByCarId(int id);
        IDataResult<List<CarImageDto>> GetCarImageByCarId(int id);
        IResult Add(CarImage carImage, IFormFile file);

        IResult Update(CarImage carImage, IFormFile file);
        IResult Delete(CarImage carImage);


    }
}
