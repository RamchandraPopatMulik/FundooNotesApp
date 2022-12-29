using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooModel
{
    public class UpdateNoteModel
    {
        public string title { get; set; }
        public string discription { get; set; }
        public DateTime reminder { get; set; }
        public string colour { get; set; }
        public string image { get; set; }
        public bool archive { get; set; }
        public bool pinNotes { get; set; }
        public bool trash { get; set; }
    }
}
