using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Notes.Service
{
    class PanContainer : ContentView
    {
        double x;
        double y;

        public PanContainer()
        {
            // Set PanGestureRecognizer.TouchPoints to control the
            // number of touch points needed to pan
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            GestureRecognizers.Add(panGesture);
        }

        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                /*case GestureStatus.Started:

                case GestureStatus.Running:
                    Console.WriteLine("RUNNING");
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                    Content.TranslationX =
                      Math.Max(Math.Min(0, x + e.TotalX), -Math.Abs(Content.Width - 100));
                    Content.TranslationY =
                      Math.Max(Math.Min(0, y + e.TotalY), -Math.Abs(Content.Height - 100));

                    break;

                case GestureStatus.Completed:
                    Console.WriteLine("COMPLETED");
                    // Store the translation applied during the pan
                    x = Content.TranslationX;
                    y = Content.TranslationY;
                    break;*/

                case GestureStatus.Started:
                    //степень перемещенеия элемента
                    Content.TranslationX = 0;
                    break;
                case GestureStatus.Completed:
                    Content.TranslationX = 0;
                    break;
                case GestureStatus.Running:
                    if (Math.Abs(e.TotalX) > 20f && Math.Abs(e.TotalY) < 20f)
                    {
                        if (e.TotalX > 0)
                        {
                            Content.TranslationX = e.TotalX;
                            x = e.TotalX;
                        }
                        else if (e.TotalX < 0)
                        {
                            Content.TranslationX = e.TotalX;
                            x = e.TotalX;
                        }
                    }


                    break;
            }
        }
    }
}

/* <SwipeGestureRecognizer Direction = "Left"
                                                                    Threshold="5"
                                                                    Command="{Binding ListNotesViewModel.SwipeCommand}"
                                                                    CommandParameter="{Binding}"/>*/

/*var translationY = Math.Max(Math.Min(0, y + e.TotalY), -Math.Abs(stack.Height - 100));
stack.TranslateTo(contentX, translationY);*/
