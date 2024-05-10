using BuisinessLayer.Interface;
using ModelLayer.DTO.Request;
using ModelLayer.DTO.Responce;
using ModelLayer.Entities;
using ModelLayer.GlobalException;
using RepositaryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisinessLayer.Service
{
    public class UserServiceBL(IUserRL userRepo):IUserBL
    {
        private User MapToEntity(UserRequest request)
        {
            return new User
            {
                name = request.name,
                email = request.email,
                password = Encrypt(request.password),
                mobileNumber = request.mobileNumber
            };
        }
        private String Encrypt(String password)
        {

            byte[] passByte = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passByte);

        }
        private String Decrypt(String encryptedPass)
        {
            byte[] passbyte = Convert.FromBase64String(encryptedPass);
            string s = Encoding.UTF8.GetString(passbyte);
            return s;
        }
        private UserResponce MapToResponce(User responce)
        {
            return new UserResponce
            {
                userId = responce.userId,
                name = responce.name,
                email = responce.email,
                mobileNumber = responce.mobileNumber
            };
        }

        public int Register(UserRequest request)
        {
            return userRepo.Register(MapToEntity(request));
        }

        public UserResponce Login(string Email, string password)
        {
            User entity;
            try
            {
                entity = userRepo.GetUserByEmail(Email);
            }
            catch (AggregateException e)
            {
                throw new UserNotFoundException("UserNotFoundByEmailId");
            }
            if (password.Equals(Decrypt(entity.password)))
            {
                return MapToResponce(entity);
            }
            else
            {
                throw new PasswordMissmatchException("Incorrect Password");
            }

        }
    }
}
