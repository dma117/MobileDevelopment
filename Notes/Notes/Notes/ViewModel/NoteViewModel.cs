using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Notes.Model;
using Newtonsoft.Json;

namespace Notes.ViewModel
{
    public class NoteViewModel : INotifyPropertyChanged, IEquatable<NoteViewModel>, IComparable<NoteViewModel>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ListNotesViewModel _listNotesViewModel;
        [JsonProperty]
        private Note _note;
        private int _noteMaxSize;

        public NoteViewModel()
        {
            _note = new Note();
            _noteMaxSize = 6;
        }

        public double Height { get; set; }

        [JsonIgnore]
        public ListNotesViewModel ListNotesViewModel
        {
            get => _listNotesViewModel;

            set
            {
                if (value != _listNotesViewModel)
                {
                    _listNotesViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonIgnore]
        public string Message
        {
            get => _note.Message;

            set
            {
                if (value != _note.Message)
                {
                    _note.Message = value;
                    OnPropertyChanged();
                    MessageSize = value.Length;
                }
            }
        }

        [JsonIgnore]
        public DateTime Date
        {
            get => _note.Date;

            set
            {
                if (value != _note.Date)
                {
                    _note.Date = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonIgnore]
        public int MessageSize
        {
            get => _note.MessageSize;

            set
            {
                if (value != _note.MessageSize)
                {
                    _note.MessageSize = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonIgnore]
        public int NoteMaxSize
        {
            get => _noteMaxSize;

            set
            {
                if (value != _noteMaxSize)
                {
                    _noteMaxSize = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Changed()
        {
            var previousMessage = _note.PreviousMessage;
            _note.PreviousMessage = _note.Message;

            return _note.Message != previousMessage;
        }

        public bool Empty()
        {
            return _note.MessageSize == 0;
        }

        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public bool Equals(NoteViewModel other)
        {
            return other != null && (other as NoteViewModel) == this;
        }

        public int CompareTo(NoteViewModel other)
        {
            if (other == null) //TODO should we check this == null?
            {
                throw new NullReferenceException();
            }

            if (this.Date < other.Date)
            {
                return 1;
            }
            if (this.Date > other.Date)
            {
                return -1;
            }

            return 0;
        }
    }
}
