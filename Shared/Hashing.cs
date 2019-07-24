using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using SixLabors.Primitives;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace LaXiS.ImageHash.Shared
{
    public static class Hashing
    {
        public static string Md5Hash(FileStream stream)
        {
            using (var md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(stream);

                StringBuilder str = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                    str.Append(data[i].ToString("x2"));

                return str.ToString();
            }
        }

        // http://www.hackerfactor.com/blog/index.php?/archives/432-Looks-Like-It.html
        public static UInt64 AverageHash(FileStream stream)
        {
            using (Image<Rgba32> image = Image.Load<Rgba32>(stream))
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
        public static UInt64 DifferenceHash(FileStream stream)
        {
            using (Image<Rgba32> image = Image.Load<Rgba32>(stream))
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

        public static int HammingDistance(UInt64 hash1, UInt64 hash2)
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
}
