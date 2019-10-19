namespace LaXiS.ImageHash.WebApi.Domain.Models
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
