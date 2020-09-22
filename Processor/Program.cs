using LaXiS.ImageHash.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LaXiS.ImageHash.Processor
{
    class Program
    {
        static void Main()
        {
            List<string> mimeTypesFilter = new List<string>
            {
                "image/jpeg",
                "image/png",
                "image/bmp"
            };

            // LiteDatabase db = new LiteDatabase(@"data.db");
            // LiteCollection<ImageFile> imageFilesCollection = db.GetCollection<ImageFile>("imagefiles");

            List<Task> taskList = new List<Task>();

            DirectoryInfo dir = new DirectoryInfo(@"test/");

            int count = 0;
            foreach (FileInfo file in dir.EnumerateFiles())
            {
                FileStream stream = file.OpenRead();
                if (!mimeTypesFilter.Contains(ImageInfo.GetMimeType(stream)))
                    continue;

                count++;
                // if (count > 500)
                //     continue;

                taskList.Add(Task<string>.Factory.StartNew(o =>
                {
                    string md5Hash = "";
                    // UInt64 averageHash = 0;
                    UInt64 differenceHash = 0;

                    FileStream imageStream = o as FileStream;

                    imageStream.Position = 0;
                    md5Hash = Hashing.Md5Hash(imageStream);

                    // imageStream.Position = 0;
                    // averageHash = AverageHash(imageStream);

                    imageStream.Position = 0;
                    differenceHash = Hashing.DifferenceHash(imageStream);

                    imageStream.Dispose();

                    // return $"{md5Hash},{averageHash:x16},{differenceHash:x16},{file.Name}";
                    return $"{md5Hash},{differenceHash:x16},{file.Name}";

                    // ImageFile imageFileRecord = imageFilesCollection.FindOne(x => x.Md5Hash == md5Hash);
                    // if (imageFileRecord == null)
                    // {
                    //     imageFileRecord = new ImageFile()
                    //     {
                    //         Name = file.Name,
                    //         Md5Hash = md5Hash,
                    //         AverageHash = averageHash
                    //     };
                    //     imageFilesCollection.Insert(imageFileRecord);
                    // }
                    // else
                    // {
                    //     imageFileRecord.AverageHash = averageHash;
                    //     imageFilesCollection.Update(imageFileRecord);
                    // }

                    // imageFilesCollection.EnsureIndex("Md5Hash");

                    // Console.WriteLine($"{file.Name} {md5Hash} {averageHash:x16}");
                }, stream));
            }

            Task.WaitAll(taskList.ToArray());

            count = 0;
            using (StreamWriter file = new StreamWriter(@"data.csv"))
            {
                foreach (Task<string> t in taskList)
                {
                    // Console.WriteLine($"{count},{t.Result}");
                    file.WriteLine($"{count},{t.Result}");

                    count++;
                }
            }
        }
    }

    // public class ImageFile
    // {
    //     public Guid Id { get; set; }
    //     public string Name { get; set; }
    //     public string Md5Hash { get; set; }
    //     public UInt64 AverageHash { get; set; }
    // }
}
