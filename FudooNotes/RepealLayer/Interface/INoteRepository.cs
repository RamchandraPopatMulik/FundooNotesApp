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
    }
}
