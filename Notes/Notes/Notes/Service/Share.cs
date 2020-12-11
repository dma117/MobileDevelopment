using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Notes.Service
{
    public static class Share
    {
        public static async Task ShareText(string text)
        {
            await Xamarin.Essentials.Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text"
            });
        }
    }
}
