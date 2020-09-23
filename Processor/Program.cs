using LaXiS.ImageHash.Models.Resources;
using LaXiS.ImageHash.Shared;
using LaXiS.ImageHash.WebApi.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LaXiS.ImageHash.Processor
{
    class Program
    {
        static readonly List<string> mimeTypesFilter = new List<string>
        {
            "image/jpeg",
            "image/png",
            "image/bmp"
        };

        static void Main()
        {
            ILogger log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            ImagesClient client = new ImagesClient("http://10.0.1.202/");

            List<Task> taskList = new List<Task>();

            DirectoryInfo dir = new DirectoryInfo(@"test/");

            int count = 0;
            foreach (FileInfo file in dir.EnumerateFiles())
            {
                count++;
                if (count > 100)
                    break;
                if (count % 200 == 0)
                    log.Information("{count}", count);

                //if (taskList.Count > 2)
                //{
                //    Task.WaitAny(taskList.ToArray());
                //}

                Task imageTask = new Task(() =>
                {
                    FileStream imageStream = file.OpenRead();
                    if (!mimeTypesFilter.Contains(ImageInfo.GetMimeType(imageStream)))
                        return;

                    imageStream.Position = 0;
                    string md5Hash = Hashing.Md5Hash(imageStream);

                    // imageStream.Position = 0;
                    // UInt64 averageHash = AverageHash(imageStream);

                    imageStream.Position = 0;
                    string differenceHash = Hashing.DifferenceHash(imageStream);

                    imageStream.Dispose();

                    ImageWriteResource image = new ImageWriteResource
                    {
                        Name = file.Name,
                        Url = file.FullName,
                        Md5 = md5Hash,
                        DifferenceHash = differenceHash
                    };

                    log.Information("{fileName}", file.Name);
                    try
                    {
                        client.Post(image);
                    }
                    catch (Exception e)
                    {
                        log.Error(e, "{fileName}", file.Name);
                    }

                    // return $"{md5Hash},{averageHash:x16},{differenceHash:x16},{file.Name}";
                    //return $"{md5Hash},{differenceHash:x16},{file.Name}";
                });
                taskList.Add(imageTask);
                imageTask.Start();
            }

            try
            {
                Task.WaitAll(taskList.ToArray());
            }
            catch (Exception e)
            {
                log.Error(e, "WaitAll()");
            }

            //count = 0;
            //using (StreamWriter file = new StreamWriter(@"data.csv"))
            //{
            //    foreach (Task<string> t in taskList)
            //    {
            //        Console.WriteLine($"{count},{t.Result}");
            //        //file.WriteLine($"{count},{t.Result}");

            //        count++;
            //    }
            //}
        }
    }
}
