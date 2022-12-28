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
    }
}
