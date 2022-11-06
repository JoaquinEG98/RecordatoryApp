using Models.Request;
using Models.Response;

namespace Interfaces
{
    public interface INote
    {
        public Task<Response> AddNote(NoteRequest noteRequest);
        public Task<Response> UpdateNote(int noteId, NoteRequest noteRequest);
        public Task<Response> FinishNote(int noteId);
        public Task<Response> UnfinishNote(int noteId);
        public Task<Response> GetNote(int noteId);
        public Task<Response> GetNotes(int userId);
    }
}
