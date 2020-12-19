using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Notes.Service
{
    class Share
    {
        private static readonly Lazy<Share> _instance = new Lazy<Share>(() => new Share());

        public static Share Instance
        {
            get => _instance.Value;
        }

        public async Task ShareText(string text)
        {
            await Xamarin.Essentials.Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text"
            });
        }
    }
}
