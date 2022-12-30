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
using System.Security.Principal;

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
                            noteModel.userId = sqlDataReader.IsDBNull("userId") ? 0 : sqlDataReader.GetInt32("userId");
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
        public UpdateNoteModel UpdateNotes(UpdateNoteModel updateNote, int userId, int noteId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("sp_update", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@noteId", noteId);
                    command.Parameters.AddWithValue("@title", updateNote.title);
                    command.Parameters.AddWithValue("@discription", updateNote.discription);
                    command.Parameters.AddWithValue("@reminder", updateNote.reminder);
                    command.Parameters.AddWithValue("@colour", updateNote.colour);
                    command.Parameters.AddWithValue("@image", updateNote.image);
                    command.Parameters.AddWithValue("@archive", updateNote.archive);
                    command.Parameters.AddWithValue("@pinNotes", updateNote.pinNotes);
                    command.Parameters.AddWithValue("@trash", updateNote.trash);
                    command.Parameters.AddWithValue("@modified", updateNote.modified);

                    connection.Open();
                    int deleteOrNot = command.ExecuteNonQuery();

                    if (deleteOrNot >= 1)
                    {
                        return updateNote;
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
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public bool DeleteNote(int userId, int noteId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPDeleteNotes", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@noteId", noteId);

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
        public bool PinNote(bool pinNote, int userID, int noteID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("sp_PinNote", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userID", userID);
                    command.Parameters.AddWithValue("@noteID", noteID);
                    command.Parameters.AddWithValue("@pinNotes", pinNote);

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
        public bool ArchiveNote(bool archiveNote, int userId, int noteId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("sp_Archieve", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@noteId", noteId);
                    command.Parameters.AddWithValue("@archive", archiveNote);

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
        public bool TrashNote(bool trashNote, int userId, int noteId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("sp_Trash", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@noteId", noteId);
                    command.Parameters.AddWithValue("@trash", trashNote);

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
        public bool Colour(string colour, int userId, int noteId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("sp_colour", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@noteId", noteId);
                    command.Parameters.AddWithValue("@colour", colour);

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
        public bool Reminder(string reminder, int userId, int noteId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("sp_Reminder", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@noteId", noteId);
                    command.Parameters.AddWithValue("@remindr", reminder);

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
      
    }
}


            
    
