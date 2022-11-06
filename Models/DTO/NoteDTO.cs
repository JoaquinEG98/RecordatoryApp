namespace Models.DTO
{
    public class NoteDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool Finished { get; set; }

        public static NoteDTO FillObject(Note note)
        {
            return new NoteDTO()
            {
                Id = note.Id,
                Description = note.Description,
                StartDate = note.StartDate,
                FinishDate = note.FinishDate,
                Finished = note.Finished
            };
        }
    }
}
