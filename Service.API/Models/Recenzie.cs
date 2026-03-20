namespace Service.API.Models
{
    public class Recenzie
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        // FK
        public Guid ServiciuId { get; set; }

        public string Text { get; set; } = default!;   // continutul recenziei
        public byte Rating { get; set; }               // 1–5
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
