namespace Entity.DTOs.MailDTOs
{
    public class AddMailDTO
    {
        public string For { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
