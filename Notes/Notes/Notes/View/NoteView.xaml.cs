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
        public Command GoBackCommand { get; set; }
        public NoteView(NoteViewModel noteViewModel)
        {
            InitializeComponent();
            BindingContext = noteViewModel;
        }
        protected override bool OnBackButtonPressed()
        {
            Console.WriteLine("HERE");
            (BindingContext as NoteViewModel)?.ListNotesViewModel.SaveCommand.Execute(BindingContext);
            return true;
        }


        /*protected override void OnDisappearing()
        {
            (BindingContext as NoteViewModel)?.ListNotesViewModel.SaveCommand.Execute(BindingContext);
        }*/

        /*  protected override bool OnBackButtonPressed()
          {
              (BindingContext as NoteViewModel)?.ListNotesViewModel.SaveCommand.Execute(BindingContext);
              return false;
          }*/
    }
}