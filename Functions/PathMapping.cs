using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace payment_posting.Functions
{
    public class PathMapping
    {
        public static void CheckIfFileExists(string pathis, string fileExt, string archivepath)
        {
            if (Directory.GetFiles(pathis).Length == 0)
            {
                Console.WriteLine(pathis + " is empty.");
            }
            else
            {
                ArchiveFiles(pathis, fileExt, archivepath);
            }
        }

        public static void ArchiveFiles(string paths, string ext, string archive)
        {
            ext = "*" + ext;
            string[] files = Directory.GetFiles(paths, ext);
            string timeis = DateTime.Now.ToString("hhmmss");

            foreach (var i in files)
            {
                File.Move(i, Path.Combine(archive, timeis + Path.GetFileName(i)));
                //Console.WriteLine("Archived file " + i.ToString());
            };
        }

    }
}
