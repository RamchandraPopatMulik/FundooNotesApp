using BussinessLayer;
using FundooManager.Interface;
using FundooModel;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Model
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;

        public UserManager (IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public UserModel Register(UserModel userModel)
        {
            try
            {
                return this.userRepository.Register (userModel);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string Login(UserLogin userLogin)
        {
            try
            {
                return this.userRepository.Login(userLogin);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string Forgot(string emailId)
        {
            try
            {
                return this.userRepository.Forgot(emailId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ResetPass(UserResetPassWordModel userResetPassWordModel, string emailId)
        {
            try
            {
                return this.userRepository.ResetPass(userResetPassWordModel, emailId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
