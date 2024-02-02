namespace Entity.DTOs.MailDTOs
{
    public class MailDTO
    {
        public int Id { get; set; }
        public string For { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
