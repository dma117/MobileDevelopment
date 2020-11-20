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
    public partial class ListNotesView : ContentPage
    {
        public ListNotesView()
        {
            BindingContext = new ListNotesViewModel(Navigation);
            InitializeComponent();
        }
    }
}