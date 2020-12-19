using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Notes.Model;
using Newtonsoft.Json;
using System.Windows.Input;
using Xamarin.Forms;
using Notes.Service;

namespace Notes.ViewModel
{
    public class NoteViewModel : INotifyPropertyChanged, IEquatable<NoteViewModel>, IComparable<NoteViewModel>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ListNotesViewModel _listNotesViewModel;
        [JsonProperty]
        private Note _note;
        private int _noteMaxSize;
        private bool _canOpen;

        public NoteViewModel()
        {
            _note = new Note();
            _noteMaxSize = 6;
            _canOpen = true;

            ShareCommand = new Command(Share, (_) => CanOpen);
            SaveCommand = new Command(SaveNote, (_) => CanOpen);
        }

        public double Height { get; set; }

        public ICommand ShareCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

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

        [JsonIgnore]
        public bool CanOpen
        {
            get => _canOpen;

            set
            {
                if (value != _canOpen)
                {
                    _canOpen = value;
                    (SaveCommand as Command)?.ChangeCanExecute();
                    (ShareCommand as Command)?.ChangeCanExecute();
                }
            }
        }


        public async void Share(object obj)
        {
            CanOpen = false;

            string text = obj as string;

            if (text == null)
            {
                await Service.Share.Instance.ShareText(String.Empty);
            }
            else
            {
                await Service.Share.Instance.ShareText(text);
            }

            CanOpen = true;
        }

        public void SaveNote(object obj)
        {
            CanOpen = false;
            _listNotesViewModel.SaveCommand.Execute(obj);
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

        public void Reset()
        {
            CanOpen = true;
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
            if (other == null)
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
