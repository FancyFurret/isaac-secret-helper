using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace Basement
{
    public class SaveFile
    {
        public ReadOnlyCollection<Achievement> Secrets { get; private set; }

        private byte[] _bytes;

        public SaveFile(string filename)
        {
            LoadData().Wait();
            LoadFile(filename);
        }

        private async Task LoadData()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var achievements = assembly.GetManifestResourceStream("Basement.achievements.json");
            Secrets = Array.AsReadOnly((await JsonSerializer.DeserializeAsync<Achievement[]>(achievements!, new JsonSerializerOptions
            {
                
            }))!);
        }

        private void LoadFile(string filename)
        {
            _bytes = File.ReadAllBytes(filename);
            LoadSections();
        }

        private void LoadSections()
        {
            var currentOffset = 20;
            var secrets = GetBoolSection(currentOffset);
            for (var i = 1; i < secrets.Length; i++)
                Secrets[i - 1].Unlocked = secrets[i];
        }

        private bool[] GetBoolSection(int offset)
        {
            return GetSection(offset).Select(b => b == 1).ToArray();
        }

        private byte[] GetSection(int offset)
        {
            var sectionLength = BitConverter.ToUInt32(_bytes, offset + 8);
            var sectionBytes = new byte[sectionLength];

            offset += 12;
            Array.Copy(_bytes, offset, sectionBytes, 0, sectionLength);
            return sectionBytes;
        }
    }
}