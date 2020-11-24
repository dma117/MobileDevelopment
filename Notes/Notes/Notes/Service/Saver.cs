using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Notes.ViewModel;

namespace Notes.Service
{
    class Saver
    {
        private static readonly Lazy<Saver> _instance = new Lazy<Saver>(() => new Saver());
        private string _path;
        private Saver()
        {
            _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "data.json");
        }

        public static Saver Instance 
        {
            get => _instance.Value;
        }

        public async void SaveDataAsync(List<NoteViewModel> list)
        {
            using (var sw = new StreamWriter(_path))
            {
                string task = await Task.Run(() =>
                 (JsonConvert.SerializeObject(list)));

                sw.Write(task);
            }
        }

        public List<NoteViewModel> LoadData()
        {
            if (!File.Exists(_path))
            {
                return new List<NoteViewModel>();
            }

            using (var sr = new StreamReader(_path))
            {
                return JsonConvert.DeserializeObject<List<NoteViewModel>>(sr.ReadLine());
            }
        }
    }
}
