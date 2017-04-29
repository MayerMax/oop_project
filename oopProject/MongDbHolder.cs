using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private void CreateFromCSV() {
            using (var sr = new StreamReader(sourcePath)) {
                var parser = new CsvParser(sr);
                var withPhotos = photosNames(picturesPath, "PNG");
                int namePos = parser.Read().ToList().IndexOf("Name");

                while (true) {
                    var curRow = parser.Read();
                    if (curRow == null)
                        break;
                    if (withPhotos.Contains(curRow[namePos]))
                        break;
                    //TODO
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
