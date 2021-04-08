using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        ICarService _carService;

        public CarImageManager(ICarImageDal carImageDal, ICarService carService)
        {
            _carImageDal = carImageDal;
            _carService = carService;
        }





        [ValidationAspect(typeof(CarImageValidator))]

        public IResult Add(CarImage carImage, IFormFile file)
        {
            IResult result = BusinessRules.Run(CheckIfImageLimitExceded(carImage.CarId));
            if (result != null)
            {
                return result;
            }

            carImage.ImagePath = FileHelper.AddAsync(file);
            carImage.Date = DateTime.Now;

            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }


        public IResult Delete(CarImage carImage)
        {
            var oldpath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot"))
                          + _carImageDal.Get(p => p.Id == carImage.Id).ImagePath;
            IResult result = BusinessRules.Run(FileHelper.DeleteAsync(oldpath));

            if (result != null)
            {
                return result;
            }


            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.CarImageDeleted);

        }

        public IDataResult<CarImage> Get(int id)
        {


            return new SuccessDataResult<CarImage>(_carImageDal.Get(p => p.Id == id));
        }

        public IDataResult<List<CarImage>> GetByCarId(int id)
        {
            IResult result = BusinessRules.Run(CheckIfCarIdExists(id), CheckIfImageNotExists(id));
            if (result == null)
            {
                CarImage carImage = new CarImage();
                carImage.CarId = id;
                carImage.ImagePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot")) + "\\Images\\nophoto.jpg";
                List<CarImage> list = new List<CarImage>();
                list.Add(carImage);
                return new SuccessDataResult<List<CarImage>>(list);
            }
            else
            {
                if (result.Message == Messages.CarIdDosntExists)
                {
                    return new ErrorDataResult<List<CarImage>>(_carImageDal.GetAll(p => p.CarId == id), Messages.CarIdDosntExists);

                }
            }




            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(p => p.CarId == id));
        }

        public IDataResult<List<CarImage>> GetAll()
        {

            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.CarImagesListed);
        }


        public IDataResult<List<CarImageDto>> GetCarImageByCarId(int id)
        {

            return new SuccessDataResult<List<CarImageDto>>(_carImageDal.GetCarImageByCarId(id));

        }





        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Update(CarImage carImage, IFormFile file)
        {

            var oldpath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot"))
                          + _carImageDal.Get(p => p.Id == carImage.Id).ImagePath;

            carImage.ImagePath = FileHelper.UpdateAsync(oldpath, file);
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);



        }

        private IResult CheckIfImageNotExists(int carId)
        {


            var result = _carImageDal.GetAll(p => p.CarId == carId).Count;
            if (result <= 0)
            {

                return new SuccessResult();

            }
            else
            {
                return new ErrorResult();

            }

        }

        private IResult CheckIfCarIdExists(int carId)
        {
            var carToGet = _carService.GetById(carId);

            if (carToGet.Data != null)
            {
                return new SuccessResult();

            }
            return new ErrorResult(Messages.CarIdDosntExists);
        }




        private IResult CheckIfImageLimitExceded(int carId)
        {
            var carImageCount = _carImageDal.GetAll(p => p.CarId == carId).Count;
            if (carImageCount > 5)
            {
                return new ErrorResult(Messages.CarImageLimitExceded);
            }
            return new SuccessResult();
        }
    }
}
