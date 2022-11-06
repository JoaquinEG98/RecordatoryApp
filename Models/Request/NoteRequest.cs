namespace Models.Request
{
    public class NoteRequest
    {
        public string? Description { get; set; }
        public DateTime? FinishDate { get; set; }
        public int UserId { get; set; }
    }
}
