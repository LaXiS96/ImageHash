using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Cryptography;
// using LiteDB;
using SixLabors.Primitives;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace LaXiS.ImageHash
{
    class Program
    {
        static void Main(string[] args)
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

            DirectoryInfo dir = new DirectoryInfo(@"\\10.10.0.201\priv\p\1\");

            int count = 0;
            foreach (FileInfo file in dir.EnumerateFiles())
            {
                FileStream fileStream = file.OpenRead();
                IImageFormat format = Image.DetectFormat(fileStream);
                if (format == null || !mimeTypesFilter.Contains(format.DefaultMimeType))
                    continue;

                count++;
                if (count > 500)
                    continue;

                taskList.Add(Task<string>.Factory.StartNew(o =>
                {
                    string md5Hash = "";
                    UInt64 averageHash = 0;
                    UInt64 differenceHash = 0;

                    FileStream imageStream = o as FileStream;

                    imageStream.Position = 0;
                    md5Hash = Md5Hash(imageStream);

                    // imageStream.Position = 0;
                    // averageHash = AverageHash(imageStream);

                    imageStream.Position = 0;
                    differenceHash = DifferenceHash(imageStream);

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
                }, fileStream));
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

        private static string Md5Hash(FileStream file)
        {
            using (var md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(file);

                StringBuilder str = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                    str.Append(data[i].ToString("x2"));

                return str.ToString();
            }
        }

        // http://www.hackerfactor.com/blog/index.php?/archives/432-Looks-Like-It.html
        private static UInt64 AverageHash(FileStream file)
        {
            using (Image<Rgba32> image = Image.Load<Rgba32>(file))
            {
                // Convert to grayscale and resize to 8x8
                image.Mutate(o =>
                {
                    o.Grayscale();
                    o.Resize(new ResizeOptions
                    {
                        Size = new Size(8),
                        Mode = ResizeMode.Stretch
                    });
                });

                // Calculate average pixel value
                int sum = 0;
                for (int i = 0; i < 64; i++)
                {
                    sum += image[i, 0].R;
                }
                float average = (float)sum / 64;

                // Construct 64 bit hash
                UInt64 hash = 0;
                for (int i = 0; i < 64; i++)
                {
                    if (image[i, 0].R >= average)
                        hash |= (UInt64)1 << (63 - i);
                }

                return hash;
            }
        }

        // http://www.hackerfactor.com/blog/index.php?/archives/529-Kind-of-Like-That.html
        private static UInt64 DifferenceHash(FileStream file)
        {
            using (Image<Rgba32> image = Image.Load<Rgba32>(file))
            {
                // Convert to grayscale and resize to 9x8
                image.Mutate(o =>
                {
                    o.Grayscale();
                    o.Resize(new ResizeOptions
                    {
                        Size = new Size(9, 8),
                        Mode = ResizeMode.Stretch
                    });
                });

                // Compute differences between adjacent pixels and construct hash
                UInt64 hash = 0;
                for (int y = 0, i = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++, i++)
                    {
                        // Console.WriteLine($"{x} {y} {i} {image[x, y].R} {image[x + 1, y].R}");
                        if (image[x, y].R > image[x + 1, y].R)
                            hash |= (UInt64)1 << (63 - i);
                    }
                }

                return hash;
            }
        }

        private static int HammingDistance(UInt64 hash1, UInt64 hash2)
        {
            UInt64 xor = hash1 ^ hash2;
            int distance = 0;

            while (xor > 0)
            {
                distance += (int)xor & 1;
                xor >>= 1;
            }

            return distance;
        }
    }

    public class ImageFile
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Md5Hash { get; set; }
        public UInt64 AverageHash { get; set; }
    }
}
