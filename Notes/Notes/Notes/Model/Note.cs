using System;
namespace Notes.Model
{
    class Note
    {
        public Note()
        {
            Date = DateTime.Now;
        }
        public string Message { get; set; }
        public string PreviousMessage { get; set; }
        public DateTime Date { get; set; }
        public int MessageSize { get; set; }
    }
}
