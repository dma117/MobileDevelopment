using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Notes.Model;

namespace Notes.ViewModel
{
    public class NoteViewModel :  INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ListNotesViewModel _listNotesViewModel;
        private Note _note;
        private int _noteMaxSize;

        public NoteViewModel()
        {
            _note = new Note();
            _noteMaxSize = 6;
        }

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

        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
