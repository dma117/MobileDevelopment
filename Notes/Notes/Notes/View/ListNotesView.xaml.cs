using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Notes.ViewModel;
using Notes.Service;
using System.Collections.ObjectModel;

namespace Notes.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListNotesView : ContentPage
    {
        double x = 0, y = 0;
        double contentX = 0, contentY = 0;
        bool horizontal = false;
        bool vertical = false;
        public ListNotesView()
        {
            BindingContext = new ListNotesViewModel(Navigation);
            InitializeComponent();
        }

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    ((StackLayout)sender).TranslationX = 0;
                    ((StackLayout)sender).TranslationY = 0;
                    break;
                case GestureStatus.Completed:
                    if (horizontal)
                    {
                        (BindingContext as ListNotesViewModel)?.SwipeCommand.Execute(((StackLayout)sender).BindingContext);
                        ((StackLayout)sender).TranslateTo(0, y);
                        x = 0;
                    }

                    horizontal = false;
                    vertical = false;
                    contentY = stack.TranslationY;

                    break;
                case GestureStatus.Running:
                    if (!horizontal)
                    {
                        if (Math.Abs(e.TotalY) > 40f)
                        {
                            /*if (stack.Height > 500)
                            {*/
                                //var translationY = Math.Max(Math.Min(0, contentY + e.TotalY), -Math.Abs(stack.Height - 500));
                                
                                    stack.TranslateTo(contentX, contentY + e.TotalY);
                                    vertical = true;

                              /*  }*/
                        }
                       
                    }
                    if (!vertical)
                    {
                        if (Math.Abs(e.TotalX) > 10f)
                        {
                            if ((StackLayout)sender != main)
                            {
                                ((StackLayout)sender).TranslateTo(x + e.TotalX, y);
                                x = e.TotalX;
                                horizontal = true;
                            }
                        }
                    }
                    break;
                default:
                    ((StackLayout)sender).TranslateTo(0, y);
                    break;
            }
        }
    }
}