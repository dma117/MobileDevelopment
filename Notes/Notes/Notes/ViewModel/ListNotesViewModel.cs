using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Notes.Model;
using Xamarin.Forms;
using Notes.View;
using System.Runtime.CompilerServices;

namespace Notes.ViewModel
{
    public class ListNotesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private NoteViewModel _selectedNote;
        private bool _canOpen;

        public ListNotesViewModel(INavigation navigation)
        {
            ListNotesLeft = new ObservableCollection<NoteViewModel>();
            ListNotesRight = new ObservableCollection<NoteViewModel>();

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
                Console.WriteLine("HeightLeft = " + HeightLeft);
                Console.WriteLine("HeightRight = " + HeightRight);

                if (HeightLeft > HeightRight)
                {
                    AddNoteToTheList(ListNotesRight, noteViewModel);
                }
                else
                {
                    AddNoteToTheList(ListNotesLeft, noteViewModel);
                }
            }

            Back();
        }

        private void AddNoteToTheList(ObservableCollection<NoteViewModel> list, NoteViewModel note)
        {
            if (!ListNotesRight.Contains(note) && !ListNotesLeft.Contains(note))
            {
                list.Add(note);
            }
            else
            {
                note.Date = DateTime.Now;
            }
        }

        private void Back()
        {
            CanOpen = true;
            Navigation.PopAsync();
        }

        private void OnTap(object obj)
        {
            NoteViewModel selectedNote = obj as NoteViewModel;

            if (selectedNote != null)
            {
                SelectedNote = selectedNote;
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
                if (!ListNotesLeft.Remove(selectedNote))
                {
                    ListNotesRight.Remove(selectedNote);
                }

                Console.WriteLine("size of left list: " + ListNotesLeft.Count);
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
