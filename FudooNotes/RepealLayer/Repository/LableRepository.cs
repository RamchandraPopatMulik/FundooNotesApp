using FundooModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundooRepository.Interface;
using System.Reflection.Emit;
using BussinessLayer;

namespace FundooRepository.Repository
{
    public class LableRepository : ILableRepository
    {
        string connectionString;

        public LableRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("UserDbConnection");
        }
        public bool AddLable(string lable,int noteId,int userId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_addLabel", connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@LabelName", lable);
                    sqlCommand.Parameters.AddWithValue("@userId", userId);
                    sqlCommand.Parameters.AddWithValue("@noteId", noteId);
                   
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
            finally
            {
                connection.Close();
            }
        }
        public List <LableModel> GetLabels(int noteId)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                List<LableModel> list = new List<LableModel>();
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_GetLabels", connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@noteId", noteId);

                    connection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            LableModel labelModel = new LableModel()
                            {
                                LabelId = reader.IsDBNull("LabelId") ? 0 : reader.GetInt32("LabelId"),
                                LabelName = reader.IsDBNull("LabelName") ? String.Empty : reader.GetString("LabelName"),
                                noteId = reader.IsDBNull("noteId") ? 0 : reader.GetInt32("noteId"),
                                userId = reader.IsDBNull("userId") ? 0 : reader.GetInt32("userId")
                            };
                            list.Add(labelModel);
                        }
                        return list;
                    }
                    return null;
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
        public bool UpdateLabel(string nlable,int LabelId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_UpdateLabel", connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@LabelName", nlable);
                    sqlCommand.Parameters.AddWithValue("@LabelId", LabelId);

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
            finally
            {
                connection.Close();
            }
        }
        public bool DeleteLabel(int LabelId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_DeleteLabels", connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@LabelId", LabelId);

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
            finally
            {
                connection.Close();
            }
        }
    }
}
