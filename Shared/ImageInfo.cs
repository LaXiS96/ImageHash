using SixLabors.ImageSharp;
using System.IO;

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