using Newtonsoft.Json;
using System;
using System.IO;

namespace Backuper
{
    public partial class Settings
    {
        static readonly string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Backuper");
        static readonly string _file = Path.Combine(_path, "settings.json");

        private Settings() { }

        public static Settings InitAndDeserialize()
        {
            Directory.CreateDirectory(_path); // ensure directories are created
            if (File.Exists(_file))
                return JsonConvert.DeserializeObject<Settings>(File.ReadAllText(_file)) ?? new();
            return new();
        }

        public void Serialize()
        {
            // create backup
            if (File.Exists(_file))
            {
                var bak = _file + ".bak";
                File.Delete(bak);
                File.Move(_file, bak);
            }
            // serialize using tmp-file
            var tmp = Path.GetTempFileName();
            File.WriteAllText(tmp, JsonConvert.SerializeObject(this));
            File.Move(tmp, _file);
        }
    }
}
