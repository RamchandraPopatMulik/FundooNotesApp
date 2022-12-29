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
        public bool Delete(int userId, int noteId)
        {
            try
            {
                return this.noteRepository.Delete(userId,noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public UpdateNoteModel UpdateNote(UpdateNoteModel updateNoteModel, int userId, int noteId)
        {
            try
            {
                return this.noteRepository.UpdateNote(updateNoteModel,userId, noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool PinNotes(bool pin, int userId, int noteId)
        {
            try
            {
                return this.noteRepository.PinNotes(pin, userId, noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Archieve(bool arch, int userId, int noteId)
        {
            try
            {
                return this.noteRepository.Archieve(arch, userId, noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Trash(bool trash, int userId, int noteId)
        {
            try
            {
                return this.noteRepository.Archieve(trash, userId, noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
