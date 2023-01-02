using FundooModel;
using FundooRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class CollabraterRepository : ICollabraterRepository
    {
        string connectionString;
        public CollabraterRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("UserDbConnection");
        }
        public bool AddCollabrater(int noteId,int userId,string collabraterEmail)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_addCollabrater", connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@userId", userId);
                    sqlCommand.Parameters.AddWithValue("@noteId", noteId);
                    sqlCommand.Parameters.AddWithValue("@collabraterEmail", collabraterEmail);
                    sqlCommand.Parameters.AddWithValue("@collabraterModifiedTime", DateTime.Now);
                    connection.Open();
                    int store = sqlCommand.ExecuteNonQuery();

                    if (store >= 1)
                    {
                        return true ;
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
            finally
            {
                connection.Close();
            }
        }
        public bool DeleteCollabrater(int collabraterId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("sp_Remove", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@collabraterId", collabraterId);
                    connection.Open();
                    int deleteOrNot = command.ExecuteNonQuery();

                    if (deleteOrNot >= 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public List<CollabraterModel> RetriveCollabrater(int noteId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                List<CollabraterModel> collabraterModel = new List<CollabraterModel>();
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_Retrive", connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@noteId", noteId);


                    connection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            CollabraterModel collabraterModel1 = new CollabraterModel()
                            {
                                collabraterId =sqlDataReader.IsDBNull("collabraterId") ? 0 : sqlDataReader.GetInt32("collabraterId"),
                                collabraterEmail =sqlDataReader.IsDBNull("collabraterEmail") ? String.Empty : sqlDataReader.GetString("collabraterEmail"),
                                collabraterModifiedTime = sqlDataReader.IsDBNull("collabraterModifiedTime") ? DateTime.MinValue : sqlDataReader.GetDateTime("collabraterModifiedTime"),
                               userId =sqlDataReader.IsDBNull("userId") ? 0 : sqlDataReader.GetInt32("userId"),
                                noteId =sqlDataReader.IsDBNull("noteId") ? 0 : sqlDataReader.GetInt32("noteId"),
                            };
                            collabraterModel.Add(collabraterModel1);
                        }
                        return collabraterModel;
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
    }
}
