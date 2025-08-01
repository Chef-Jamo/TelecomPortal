namespace TelecomPortal.Data.Repository.Entities.Base
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateLastModified { get; set; } = DateTime.UtcNow;
    }
}
