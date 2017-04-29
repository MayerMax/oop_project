using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using CsvHelper;
using System.IO;

namespace oopProject
{
    public class MongDbHolder
    {
        private string sourcePath;
        private string picturesPath;

        public MongDbHolder(string sourcePath, string picturesPath) {
            this.sourcePath = sourcePath;
            this.picturesPath = picturesPath;

        }
        private Dictionary<string, int> CreateHeaderFromArray(string[] arrayHeader) {
            var dict = new Dictionary<string, int>();
            for (int i = 0; i < arrayHeader.Length; i++)
                dict[arrayHeader[i]] = i;
            return dict;
        }

        public void CreateFromCSV() {
            var client = new MongoClient();
            var database = client.GetDatabase("PlayersCollection");
            var collection = database.GetCollection<Dictionary<string, string>>("playerAttributes");

            using (var sr = new StreamReader(sourcePath)) {
                var parser = new CsvParser(sr);
                var withPhotos = photosNames(picturesPath, "PNG");
                var header = parser.Read().ToList();
                var namePos = header.IndexOf("Name");
                while (true) {
                    var info = parser.Read();
                    if (info == null)
                        break;
                    if (!withPhotos.Contains(info[namePos]))
                        continue;
                    var attributes = header.Zip(info, (k, v) => new { Key = k, Value = v })
                                           .ToDictionary(x => x.Key, x => x.Value);
                    collection.InsertOne(attributes);
                } 
            }   
        }


        public static HashSet<string> photosNames(string path, string fileExtension) {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            return new HashSet<string>(dirInfo.GetFiles()
                .Where(file => file.Extension.Equals(fileExtension))
                .Select(file => file.Name));
             
        }

        
    }
}
