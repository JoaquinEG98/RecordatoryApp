using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Models.Response;

namespace API.Controllers
{
    [ApiController]
    public class NoteController
    {
        #region Dependency inversion
        private readonly INote _noteService;

        public NoteController(INote noteService)
        {
            _noteService = noteService;
        }
        #endregion

        #region GET Methods
        [HttpGet]
        [Route("api/{controller}/getnote/{noteId}")]
        [Authorize]
        public async Task<Response> GetNote([FromRoute] int noteId)
        {
            Response response = await _noteService.GetNote(noteId);
            return response;
        }

        [HttpGet]
        [Route("api/{controller}/getnotes/{userId}")]
        [Authorize]
        public async Task<Response> GetNotes([FromRoute] int userId)
        {
            Response response = await _noteService.GetNotes(userId);
            return response;
        }
        #endregion

        #region PUT Methods
        [HttpPut]
        [Route("api/{controller}/updatenote/{noteId}")]
        [Authorize]
        public async Task<Response> UpdateNote([FromRoute] int noteId, [FromBody] NoteRequest noteRequest)
        {
            Response response = await _noteService.UpdateNote(noteId, noteRequest);
            return response;
        }

        [HttpPut]
        [Route("api/{controller}/finishnote/{noteId}")]
        [Authorize]
        public async Task<Response> FinishNote([FromRoute] int noteId)
        {
            Response response = await _noteService.FinishNote(noteId);
            return response;
        }

        [HttpPut]
        [Route("api/{controller}/unfinishnote/{noteId}")]
        [Authorize]
        public async Task<Response> UnfinishNote([FromRoute] int noteId)
        {
            Response response = await _noteService.UnfinishNote(noteId);
            return response;
        }
        #endregion

        #region POST Methods
        [HttpPost]
        [Route("api/{controller}/addnote")]
        [Authorize]
        public async Task<Response> AddNote([FromBody] NoteRequest noteRequest)
        {
            Response response = await _noteService.AddNote(noteRequest);
            return response;
        }
        #endregion
    }
}
