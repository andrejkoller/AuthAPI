namespace AuthAPI.DTOs
{
    public class UpdateNewsletterSubscribeRequestDTO
    {
        public int Id { get; set; }
        public bool IsNewsletterSubscribed { get; set; }
    }
}
