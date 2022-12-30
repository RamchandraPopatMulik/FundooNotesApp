using FundooModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface ILabelManager
    {
        public bool AddLable(string lable, int noteId, int userId);
        public List<LabelModel> GetLable(int noteId);
        public bool UpdateLabel(string nlable, int LabelId);
        public bool DeleteLabel(int LabelId);
    }
}
