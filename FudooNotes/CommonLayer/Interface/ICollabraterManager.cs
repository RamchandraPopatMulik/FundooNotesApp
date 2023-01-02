using FundooModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface ICollabraterManager
    {
        public bool AddCollabrater(int noteId, int userId, string collabraterEmail);
        public bool DeleteCollabrater(int collabraterId);
        public List<CollabraterModel> RetriveCollabrater(int noteId);
    }
}
