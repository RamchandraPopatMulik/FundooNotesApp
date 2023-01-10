using BussinessLayer;
using FundooModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface IUserManager
    {

        public string GenerateJWTToken(string emailId, int userId);
        public UserModel Register(UserModel userModel);

        public UserModel Login(UserLogin userLogin);

        public string Forgot(string emailId);

        public bool ResetPass(UserResetPassWordModel userResetPassWordModel, string emailId);

        public UserModel GetData(UserModel Model, int userId);

    }
}
