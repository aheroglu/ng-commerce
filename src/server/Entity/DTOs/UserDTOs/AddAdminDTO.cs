namespace Entity.DTOs.UserDTOs
{
    public class AddAdminDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
