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
using FundooRepository.Interface;
using static System.Formats.Asn1.AsnWriter;

namespace FundooRepository.Repository
{
    public class NoteRepository : INoteRepository
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
                    SqlCommand sqlCommand = new SqlCommand("SPCreateNote", connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@userId", userId);
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
        public NoteModel DisplayNodes(int userId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                NoteModel noteModel = new NoteModel();
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("SPGetNotes", connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@userId",userId);
                    

                    connection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            noteModel.userId = sqlDataReader.IsDBNull("userId") ? 0 : sqlDataReader.GetInt32("userId");
                            noteModel.title = sqlDataReader.IsDBNull("title") ? string.Empty : sqlDataReader.GetString("title");
                            noteModel.discription = sqlDataReader.IsDBNull("discription") ? string.Empty : sqlDataReader.GetString("discription");
                            noteModel.reminder = sqlDataReader.IsDBNull("reminder") ? DateTime.MinValue : sqlDataReader.GetDateTime("reminder");
                            noteModel.colour = sqlDataReader.IsDBNull("colour") ? string.Empty : sqlDataReader.GetString("colour");
                            noteModel.image = sqlDataReader.IsDBNull("image") ? string.Empty : sqlDataReader.GetString("image");
                            noteModel.archive = sqlDataReader.IsDBNull("archive") ? false : sqlDataReader.GetBoolean("archive");
                            noteModel.pinNotes = sqlDataReader.IsDBNull("pinNotes") ? false : sqlDataReader.GetBoolean("pinNotes");
                            noteModel.trash = sqlDataReader.IsDBNull("trash") ? false : sqlDataReader.GetBoolean("trash");
                            noteModel.created = sqlDataReader.IsDBNull("created") ? DateTime.MinValue : sqlDataReader.GetDateTime("created");
                            noteModel.modified = sqlDataReader.IsDBNull("modified") ? DateTime.MinValue : sqlDataReader.GetDateTime("modified");
                            noteModel.noteId = sqlDataReader.IsDBNull("noteId") ? 0 : sqlDataReader.GetInt32("noteId");
                        }
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
        public bool Delete(int userId,int noteId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                NoteModel noteModel = new NoteModel();
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("SPDeleteNotes", connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;

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
        public UpdateNoteModel UpdateNote(UpdateNoteModel updateNoteModel,int userId,int noteId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_update", connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@userId", userId);
                    sqlCommand.Parameters.AddWithValue("@noteId", noteId);
                    sqlCommand.Parameters.AddWithValue("@title", updateNoteModel.title);
                    sqlCommand.Parameters.AddWithValue("@discription", updateNoteModel.discription);
                    sqlCommand.Parameters.AddWithValue("@reminder", updateNoteModel.reminder);
                    sqlCommand.Parameters.AddWithValue("@colour", updateNoteModel.colour);
                    sqlCommand.Parameters.AddWithValue("@image", updateNoteModel.image);
                    sqlCommand.Parameters.AddWithValue("@archive", updateNoteModel.archive);
                    sqlCommand.Parameters.AddWithValue("@pinNotes", updateNoteModel.pinNotes);
                    sqlCommand.Parameters.AddWithValue("@trash", updateNoteModel.trash);
                    sqlCommand.Parameters.AddWithValue("@modified", DateTime.UtcNow);

                    connection.Open();
                    int store = sqlCommand.ExecuteNonQuery();

                    if (store >= 1)
                    {
                        return updateNoteModel;
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
        public bool PinNotes(bool pin,int userId,int noteId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_PinNote", connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@userId", userId);
                    sqlCommand.Parameters.AddWithValue("@noteId", noteId);
                    sqlCommand.Parameters.AddWithValue("@pinNotes", pin);
                   

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
        public bool Archieve(bool arch, int userId, int noteId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_Archieve", connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@userId", userId);
                    sqlCommand.Parameters.AddWithValue("@noteId", noteId);
                    sqlCommand.Parameters.AddWithValue("@archive", arch);


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
        public bool Trash(bool trash, int userId, int noteId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_Trash", connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@userId", userId);
                    sqlCommand.Parameters.AddWithValue("@noteId", noteId);
                    sqlCommand.Parameters.AddWithValue("@trash", trash);


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


            
    
