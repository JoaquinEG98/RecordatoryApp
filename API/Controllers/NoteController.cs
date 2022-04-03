using API.Interfaces;
using API.Request;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class NoteController
    {
        #region Dependency inversion
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }
        #endregion

        #region GET Methods
        [HttpGet]
        [Route("api/{controller}/getnote/{noteId}")]
        public async Task<Response.Response> GetNote([FromRoute] int noteId)
        {
            Response.Response response = await _noteService.GetNote(noteId);
            return response;
        }

        [HttpGet]
        [Route("api/{controller}/getnotes/{userId}")]
        public async Task<Response.Response> GetNotes([FromRoute] int userId)
        {
            Response.Response response = await _noteService.GetNotes(userId);
            return response;
        }
        #endregion

        #region PUT Methods
        [HttpPut]
        [Route("api/{controller}/updatenote/{noteId}")]
        public async Task<Response.Response> UpdateNote([FromRoute] int noteId, [FromBody] NoteRequest noteRequest)
        {
            Response.Response response = await _noteService.UpdateNote(noteId, noteRequest);
            return response;
        }

        [HttpPut]
        [Route("api/{controller}/finishnote/{noteId}")]
        public async Task<Response.Response> FinishNote([FromRoute] int noteId)
        {
            Response.Response response = await _noteService.FinishNote(noteId);
            return response;
        }

        [HttpPut]
        [Route("api/{controller}/unfinishnote/{noteId}")]
        public async Task<Response.Response> UnfinishNote([FromRoute] int noteId)
        {
            Response.Response response = await _noteService.UnfinishNote(noteId);
            return response;
        }
        #endregion

        #region POST Methods
        [HttpPost]
        [Route("api/{controller}/addnote")]
        public async Task<Response.Response> AddNote([FromBody] NoteRequest noteRequest)
        {
            Response.Response response = await _noteService.AddNote(noteRequest);
            return response;
        }
        #endregion
    }
}
