using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Model
{
    class Note
    {
        public Note()
        {
            Date = DateTime.Now;
        }
        public string Message { get; set; } 
        public DateTime Date { get; set; }
        public int MessageSize { get; set; }
    }
}
