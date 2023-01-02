using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooModel
{
    public class CollabraterModel
    {
        public int collabraterId { get; set; }
        public string collabraterEmail { get; set; }
        public DateTime collabraterModifiedTime { get; set; }
        public int userId { get; set; }
        public int noteId { get; set; }

    }
}
