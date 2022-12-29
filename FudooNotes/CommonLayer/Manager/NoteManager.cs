using BussinessLayer;
using FundooManager.Interface;
using FundooModel;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class NoteManager : INoteManager
    {
        private readonly INoteRepository noteRepository;

        public NoteManager(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }
        public NoteModel CreateNode(NoteModel noteModel, int userId)
        {
            try
            {
                return this.noteRepository.createNote(noteModel,userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public NoteModel DisplayNodes(int userId)
        {
            try
            {
                return this.noteRepository.DisplayNodes(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public UpdateNoteModel UpdateNotes(UpdateNoteModel updateNote, int userId, int noteId)
        {
            try
            {
                return this.noteRepository.UpdateNotes(updateNote, userId, noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteNote(int userId, int noteId)
        {
            try
            {
                return this.noteRepository.DeleteNote(userId, noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool PinNote(bool pinNote, int userId, int noteId)
        {
            try
            {
                return this.noteRepository.PinNote(pinNote, userId, noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ArchiveNote(bool archiveNote, int userId, int noteId)
        {
            try
            {
                return this.noteRepository.ArchiveNote(archiveNote, userId, noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool TrashNote(bool trashNote, int userID, int noteID)
        {
            try
            {
                return this.noteRepository.TrashNote(trashNote, userID, noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
