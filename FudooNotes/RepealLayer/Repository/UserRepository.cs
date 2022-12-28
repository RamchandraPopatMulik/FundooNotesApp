using BussinessLayer;
using FundooModel;
using FundooRepository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace FundooRepository.Repository 
{
    public class UserRepository : IUserRepository
    {
        string connectionString;
        private readonly IConfiguration config;
         public UserRepository(IConfiguration configuration,IConfiguration config)
        {
            connectionString = configuration.GetConnectionString("UserDbConnection");
            this.config = config;
        }
        public static string EncryptPass(string password)
        {
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(encode);
        }
        public string GenerateJWTToken(string emailId,int userId)
        {
            try
            {
                var TokenHandler = new JwtSecurityTokenHandler();
                var TokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.config[("Jwt:Key")]));
                var TokenDiscriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, emailId),
                        new Claim("userId", userId.ToString()),

                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(TokenKey,SecurityAlgorithms.HmacSha256Signature)
                };
                var token = TokenHandler.CreateToken(TokenDiscriptor);
                return TokenHandler.WriteToken(token);
            }
            catch(Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public UserModel Register(UserModel userModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_register",connection);
                  
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@firstName",userModel.firstName);
                    sqlCommand.Parameters.AddWithValue("@lastName", userModel.lastName);
                    sqlCommand.Parameters.AddWithValue("@emailId", userModel.emailId);
                    sqlCommand.Parameters.AddWithValue("@passWord", EncryptPass(userModel.passWord));
                    connection.Open();
                    int store = sqlCommand.ExecuteNonQuery();

                    if(store >=1)
                    {
                        return userModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public string Login (UserLogin userLogin)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                UserModel userModel = new UserModel();
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_login", connection);
                    
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                   
                    sqlCommand.Parameters.AddWithValue("@emailId", userLogin.emailId);
                    sqlCommand.Parameters.AddWithValue("@passWord", EncryptPass(userLogin.passWord));

                    connection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            userModel.userId = sqlDataReader.IsDBNull("userId") ? 0 : sqlDataReader.GetInt32("userId");
                        }
                        var token = GenerateJWTToken(userLogin.emailId, userModel.userId);
                        return token;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public string Forgot(string emailId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                UserModel userModel = new UserModel();
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_forgot", connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@emailId", emailId);

                    connection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            userModel.userId = sqlDataReader.IsDBNull("userId") ? 0 : sqlDataReader.GetInt32("userId");
                            userModel.firstName = sqlDataReader.IsDBNull("firstName") ? String.Empty : sqlDataReader.GetString("firstName");
                        }
                        var token = GenerateJWTToken(emailId, userModel.userId);
                        MSMQModel mSMQModel = new MSMQModel();
                        mSMQModel.SendMessage(token,emailId,userModel.firstName);

                        return token.ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public bool ResetPass(UserResetPassWordModel userResetPassWordModel, string emailId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_ResetPassWord", connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@emailId", emailId);

                    sqlCommand.Parameters.AddWithValue("@passWord", EncryptPass(userResetPassWordModel.passWord));
                    connection.Open();
                    int store = sqlCommand.ExecuteNonQuery();

                    if (store >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
