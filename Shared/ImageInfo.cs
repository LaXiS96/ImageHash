using System.IO;
using SixLabors.ImageSharp;

namespace LaXiS.ImageHash.Shared
{
    public static class ImageInfo
    {
        public static string GetMimeType(FileStream stream)
        {
            return Image.DetectFormat(stream)?.DefaultMimeType ?? string.Empty;
        }
    }
}