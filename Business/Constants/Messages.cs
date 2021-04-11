using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarAdded = "Eklendi";
        public static string CarDeleted = "Silindi";
        public static string CarUpdated = "Güncellendi";
        public static string CarNameInvalid = "İsim geçersiz. En az 2 karakter olmalı";
        public static string CarPriceInvalid = "Fiyat geçersiz. Sıfırdan büyük olmalı";
        public static string ThatNameAlreadyExists = "Bu isimde bir kayıt mevcut";
        public static string CarImageAdded = "Resim başarıyla eklendi";
        public static string CarImageDeleted = "Resim başarıyla silindi";
        public static string CarIdDosntExists = "Araç bilgisi eksik";
        public static string CarImagesListed;
        public static string CarImageUpdated;
        public static string CarImageLimitExceded;
        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string UserRegistered;
        public static string UserNotFound;
        public static string PasswordError;
        public static string SuccessfulLogin;
        public static string UserAlreadyExists;
        public static string AccessTokenCreated;
    }
}
