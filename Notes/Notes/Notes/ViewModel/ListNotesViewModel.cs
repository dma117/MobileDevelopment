using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Notes.View;
using System.Runtime.CompilerServices;
using Notes.Service;

namespace Notes.ViewModel
{
    public class ListNotesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private NoteViewModel _selectedNote;
        private bool _canOpen;
        private List<NoteViewModel> _notes;

        public ListNotesViewModel(INavigation navigation)
        {
            SetData();
            
            _canOpen = true;
            Navigation = navigation;

            AddCommand = new Command(AddNote);
            SaveCommand = new Command(SaveNote);
            BackCommand = new Command(Back);
            TapCommand = new Command(OnTap);
            SwipeCommand = new Command(OnSwipe);

        }
        public ObservableCollection<NoteViewModel> ListNotesLeft { get; private set; }
        public ObservableCollection<NoteViewModel> ListNotesRight { get; private set; }

        public double HeightLeft { private get; set; }
        public double HeightRight { private get; set; }

        public INavigation Navigation { get; private set; }

        public ICommand AddCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand TapCommand { get; private set; }
        public ICommand SwipeCommand { get; private set; }

        private void SetData()
        {
            _notes = Saver.Instance.LoadData();

            foreach(var element in _notes)
            {
                element.ListNotesViewModel = this;
            }

            ListNotesLeft = new ObservableCollection<NoteViewModel>();
            ListNotesRight = new ObservableCollection<NoteViewModel>();

            SortNotes();
        }

        private void AddNote()
        {
            CanOpen = false;
            Navigation.PushAsync(new NoteView(new NoteViewModel() { ListNotesViewModel = this }));
        }

        private void SaveNote(object note)
        {
            var noteViewModel = note as NoteViewModel;

            if (noteViewModel != null)
            {
                if (noteViewModel.Empty())
                {
                    DeleteNote(note);
                }
                else
                {
                    if (!_notes.Contains(noteViewModel)) //TODO refactor code: add new note to NoteViewModel ?
                    {
                        _notes.Add(noteViewModel);
                    }
                    if (noteViewModel.Changed())
                    {
                        noteViewModel.Date = DateTime.Now;
                    }

                    SortNotes();
                }
            }

            Back();
        }

        private void SortNotes()
        {
            ListNotesLeft.Clear();
            ListNotesRight.Clear();

            _notes.Sort();

            for (int i = 0; i < _notes.Count; i++)
            {
                if (i % 2 == 0)
                {
                    ListNotesLeft.Add(_notes[i]);
                }
                else
                {
                    ListNotesRight.Add(_notes[i]);
                }
            }
        }

        private void Back()
        {
            Saver.Instance.SaveDataAsync(_notes);
            CanOpen = true;
            Navigation.PopAsync();
        }

        private void OnTap(object obj)
        {
            if (CanOpen)
            {
                CanOpen = false;

                NoteViewModel selectedNote = obj as NoteViewModel;

                if (selectedNote != null)
                {
                    SelectedNote = selectedNote;
                }
            }
        }

        private async void OnSwipe(object obj)
        {
            if (await Application.Current.MainPage.DisplayAlert("Delete",
                        "Click ok to delete the note", "OK", "Cancel"))
            {
                DeleteNote(obj);
            }
        }

        private void DeleteNote(object obj)
        {
            NoteViewModel selectedNote = obj as NoteViewModel;

            if (selectedNote != null)
            {
                _notes.Remove(selectedNote);

                SortNotes();
            }
        }

        public bool CanOpen
        {
            get => _canOpen;
            
            set
            {
                if (value != _canOpen)
                {
                    _canOpen = value;

                    OnPropertyChanged();
                }
            }
        }

        public NoteViewModel SelectedNote
        {
            get => _selectedNote;

            set
            {
                if (value != _selectedNote)
                {
                    NoteViewModel tmpNote = value;
                    _selectedNote = null;

                    OnPropertyChanged();
                    Navigation.PushAsync(new NoteView(tmpNote));
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
