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

        public async void CreateFromCSV() {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Players");
            var collection = database.GetCollection<Dictionary<string, string>>("playerAttributes");

            using (var sr = new StreamReader(sourcePath)) {
                var parser = new CsvParser(sr);
                var withPhotos = photosNames(picturesPath, ".png");
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
                    await collection.InsertOneAsync(attributes);
                } 
            }   
        }


        public static HashSet<string> photosNames(string path, string fileExtension) {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            return new HashSet<string>(dirInfo.GetFiles()
                .Where(file => file.Extension.Equals(fileExtension))
                .Select(file => Path.GetFileNameWithoutExtension(file.Name)));
             
        }

        
    }
}
