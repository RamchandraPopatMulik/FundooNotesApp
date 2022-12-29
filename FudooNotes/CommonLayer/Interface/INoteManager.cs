using FundooModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface INoteManager
    {
        public NoteModel CreateNode(NoteModel noteModel, int userId);
        public NoteModel DisplayNodes(int userId);
        public bool Delete(int userId, int noteId);
        public UpdateNoteModel UpdateNote(UpdateNoteModel updateNoteModel, int userId, int noteId);
        public bool PinNotes(bool pin, int userId, int noteId);
        public bool Archieve(bool arch, int userId, int noteId);
        public bool Trash(bool trash, int userId, int noteId);
    }
}
