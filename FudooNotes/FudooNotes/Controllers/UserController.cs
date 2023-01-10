using BussinessLayer;
using FundooManager.Interface;
using FundooModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace FudooNotes.Controllers
{
    [Route("fundoo/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;
        private readonly ILogger<UserController> logger1;

        public UserController(IUserManager userManager, ILogger<UserController> logger1)
        {
            this.userManager = userManager;
            this.logger1 = logger1;
        }
        [HttpPost]
        [Route("fundoo/register")]
        public IActionResult Register(UserModel userModel)
        {
            try
            {
                UserModel userData = this.userManager.Register(userModel);
                if(userData != null)
                {
                    logger1.LogInformation("Register");
                    return this.Ok(new { success = true, message = "Registartion Successful" , result= userData});
                }
                return this.Ok(new { success = true, message = "User Already Registerd" });
            }
            catch(ApplicationException ex)
            {
                return this.BadRequest(new { success =false, meassage =ex.Message});
            }
        }
        [HttpPost]
        [Route("fundoo/login")]
        public IActionResult Login(UserLogin userLogin )
        {
            try
            {
                UserModel userData = this.userManager.Login(userLogin);
                logger1.LogInformation("Login");
                if (userData != null)
                {
                    string logintoken = this.userManager.GenerateJWTToken(userData.emailId,userData.userId);
                    SetSession(userData);
                    string name = HttpContext.Session.GetString("UserName");
                    string Email = HttpContext.Session.GetString("UserEmail");
                    var UserId = HttpContext.Session.GetInt32("UserId");

                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();
                    string firstName = database.StringGet("firstName");
                    string lastName = database.StringGet("lastName");
                    int userId = Convert.ToInt32(database.StringGet("userId"));
                    UserModel userModel = new UserModel()
                    {
                        userId = userId,
                        firstName =firstName,
                        lastName=lastName,
                        emailId = userLogin.emailId
                    };
                    return this.Ok(new { success = true, message = "Login Successful", result = logintoken });
                }
                return this.Ok(new { success = true, message = "Enter Valid EmailId And Password" });
            }
            catch (ApplicationException ex)
            {
                return this.BadRequest(new { success = false, meassage = ex.Message });
            }
        }
        [HttpGet]
        [Route("fundoo/forgot")]
        public IActionResult Forgot(string emailId)
        {
            try
            {
                string userData1 = this.userManager.Forgot(emailId);
                if (userData1 != null)
                {
                    return this.Ok(new { success = true, message = "Forgot Successful", result = userData1 });
                }
                return this.Ok(new { success = true, message = "Enter Valid EmailId" });
            }
            catch (ApplicationException ex)
            {
                return this.BadRequest(new { success = false, meassage = ex.Message });
            }
        }
        [HttpPut]
        [Route("fundoo/reset")]
        public IActionResult Reset(UserResetPassWordModel userResetPassWordModel,string emailId)
        {
            try
            {
                if (userResetPassWordModel.passWord == userResetPassWordModel.ConfirmPassWord)
                {
                    bool userData = this.userManager.ResetPass(userResetPassWordModel, emailId);
                    if(userData)
                    {
                        return this.Ok(new { success = true, message = "Registartion Successful", result = userData });
                    }
                }
                return this.Ok(new { success = true, message = "User Already Registerd" });
            }
            catch (ApplicationException ex)
            {
                return this.BadRequest(new { success = false, meassage = ex.Message });
            }
        }
        [HttpGet]
        [Route("fundoo/getdata")]
        public IActionResult GetData(UserModel Model, int userId)
        {
            try
            {
                var userData1 = this.userManager.GetData(Model, userId);
                if (userData1 != null)
                {
                    return this.Ok(new { success = true, message = "Gate Data Successful", result = userData1 });
                }
                return this.Ok(new { success = true, message = "Gate Data Not Successful" });
            }
            catch (ApplicationException ex)
            {
                return this.BadRequest(new { success = false, meassage = ex.Message });
            }
        }

        private void SetSession(UserModel user)
        {
            this.HttpContext.Session.SetString("UserName",user.firstName+"   "+user.lastName);
            this.HttpContext.Session.SetString("UserEmail",user.emailId);
            this.HttpContext.Session.SetInt32("UserId",user.userId);
        }
    }
}
