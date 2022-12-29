using FundooModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface INoteRepository
    {
        public NoteModel createNote(NoteModel noteModel, int userId);
        public NoteModel DisplayNodes(int userId);
        public UpdateNoteModel UpdateNotes(UpdateNoteModel updateNote, int userId, int noteId);
        public bool DeleteNote(int userId, int noteId);
        public bool PinNote(bool pinNote, int userID, int noteID);
        public bool ArchiveNote(bool archiveNote, int userId, int noteId);
        public bool TrashNote(bool trashNote, int userId, int noteId);
        public bool Colour(string colour, int userId, int noteId);
        public bool Reminder(string reminder, int userId, int noteId);

    }
}
