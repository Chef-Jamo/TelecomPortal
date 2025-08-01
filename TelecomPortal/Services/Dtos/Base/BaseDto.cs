namespace TelecomPortal.Services.Dtos.Base
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
    }
}
