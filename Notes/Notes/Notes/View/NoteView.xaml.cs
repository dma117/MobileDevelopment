using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Notes.ViewModel;

namespace Notes.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteView : ContentPage
    {
        public NoteView(NoteViewModel noteViewModel)
        {
            InitializeComponent();
            BindingContext = noteViewModel;
        }

        protected override bool OnBackButtonPressed()
        {
            (BindingContext as NoteViewModel)?.ListNotesViewModel.SaveCommand.Execute(BindingContext);
            return false;
        }
    }
}