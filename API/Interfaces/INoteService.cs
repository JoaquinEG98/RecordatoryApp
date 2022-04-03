using API.Request;

namespace API.Interfaces
{
    public interface INoteService
    {
        public Task<Response.Response> AddNote(NoteRequest noteRequest);
        public Task<Response.Response> UpdateNote(int noteId, NoteRequest noteRequest);
        public Task<Response.Response> FinishNote(int noteId);
        public Task<Response.Response> UnfinishNote(int noteId);
        public Task<Response.Response> GetNote(int noteId);
        public Task<Response.Response> GetNotes(int userId);
    }
}
