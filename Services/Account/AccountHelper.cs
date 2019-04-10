using Infrastructure.Entities;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Services.Validation;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Account
{
    public class AccountHelper
    {
        Json _json;
        User _user;
        ValidationHelper _validation;
        AccountService _service;
        public AccountHelper()
        {
            _json = new Json();
            _user = new User();
            _validation = new ValidationHelper();
            _service = new AccountService();
        }



        /// <summary>
        /// Шифрует пароль путем хеширования и солирования.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="password"></param>
        /// <returns>hashed</returns>
        public static Tuple<string, string> PasswordEncryption(string password)
        {
            // https://docs.microsoft.com/ru-ru/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-2.1

            byte[] salt = Encoding.UTF8.GetBytes(password);
            using (MD5 md5 = MD5.Create())
            {
                salt = md5.ComputeHash(salt);
            }

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

            return Tuple.Create(hashed, Convert.ToBase64String(salt));
        }



        /// <summary>
        /// Создает нового пользователя.
        /// </summary>
        /// <param name="dataStr">JSON-строка данных.</param>
        /// <returns></returns>
        public Codes.States Create(string dataStr)
        {
            Dictionary<string, string> data = _json.From(dataStr);

            if (!_validation.LimitMin(data["FirstName"], 2))
                return Codes.States.ErrorValidationLimitMin;

            _user.FirstName = _validation.LimitMaxCrop(data["FirstName"], 45).Item2;


            if (!_validation.Email(data["Email"]))
                return Codes.States.ErrorValidationEmail;

            if (_service.CheckEmail(data["Email"]))
                return Codes.States.ErrorAccountEmailIsBusy;

            _user.Email = data["Email"];


            if (!_validation.Password(data["Password"]))
                return Codes.States.ErrorValidationPassword;

            var encryptPass = PasswordEncryption(data["Password"]);
            _user.Password = encryptPass.Item1 + encryptPass.Item2;

            _service.AddUser(_user);
            return Codes.States.Success;
        }

        public Codes.States Auth(string dataStr)
        {
            Dictionary<string, string> data = _json.From(dataStr);

            if (!_service.CheckEmail(data["Email"]))
                return Codes.States.ErrorAccountDoesNotExist;

            User user = _service.GetCurrentUser(data["Email"]);
            var encryptPass = PasswordEncryption(data["Password"]);
            
            string password = encryptPass.Item1 + encryptPass.Item2;
            if(user.Password != password)
                return Codes.States.ErrorAccountIncorrectPassword;

            return Codes.States.Success;
        }
    }
}