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
        public UserModel Register(UserModel userModel);

        public string Login(UserLogin userLogin);

        public string Forgot(string emailId);

        public bool ResetPass(UserResetPassWordModel userResetPassWordModel, string emailId);

    }
}
