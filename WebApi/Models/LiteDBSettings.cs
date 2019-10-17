namespace LaXiS.ImageHash.WebApi.Models
{
    public class LiteDBSettings : ILiteDBSettings
    {
        public string ConnectionString { get; set; }
    }

    public interface ILiteDBSettings
    {
        string ConnectionString { get; set; }
    }
}
