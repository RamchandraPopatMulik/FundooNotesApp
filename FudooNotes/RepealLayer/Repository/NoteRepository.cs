using BussinessLayer;
using FundooModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class NoteRepository
    {
        string connectionString;
        public NoteRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("UserDbConnection");
        }
        public NoteModel createNote(NoteModel noteModel, int userId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_craeteNote", connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@title", noteModel.title);
                    sqlCommand.Parameters.AddWithValue("@discription", noteModel.discription);
                    sqlCommand.Parameters.AddWithValue("@reminder", noteModel.reminder);
                    sqlCommand.Parameters.AddWithValue("@colour", noteModel.colour);
                    sqlCommand.Parameters.AddWithValue("@image", noteModel.image);
                    sqlCommand.Parameters.AddWithValue("@archive", noteModel.archive);
                    sqlCommand.Parameters.AddWithValue("@pinNotes", noteModel.pinNotes);
                    sqlCommand.Parameters.AddWithValue("@trash", noteModel.trash);
                    sqlCommand.Parameters.AddWithValue("@created", noteModel.created);
                    sqlCommand.Parameters.AddWithValue("@modified", noteModel.modified);
                    connection.Open();
                    int store = sqlCommand.ExecuteNonQuery();

                    if (store >= 1)
                    {
                        return noteModel;
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


            
    
