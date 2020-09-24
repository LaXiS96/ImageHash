namespace LaXiS.ImageHash.Models.Domain
{
    public enum BackendDbType
    {
        LiteDb,
        MongoDb
    }

    public class WebApiSettings
    {
        public BackendDbType BackendDb { get; set; }
    }
}
