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
    }
}
