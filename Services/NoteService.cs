﻿using Helpers;
using Interfaces;
using Models;
using Models.DTO;
using Models.Request;
using Models.Response;

namespace API.Services
{
    public class NoteService : INote
    {
        #region Dependency injection
        private readonly Library.Note _noteService;
        private readonly Library.User _userService;

        public NoteService(IUnitOfWork unitOfWork)
        {
            _userService = new Library.User(unitOfWork);
            _noteService = new Library.Note(unitOfWork);
        }
        #endregion

        #region Methods
        public async Task<Response> AddNote(NoteRequest noteRequest)
        {
            try
            {
                if (noteRequest == null) throw new Exception("La nota que se recibió está vacía.");

                Note note = new Note()
                {
                    Description = noteRequest.Description,
                    FinishDate = noteRequest.FinishDate,
                    User = await _userService.Get(noteRequest.UserId),
                    StartDate = DateTime.Now,
                    Finished = false,
                    List = null // Las listas todavía no están implementadas.
                };

                await _noteService.Add(note);

                return Response.FillObject(null!, System.Net.HttpStatusCode.OK, "Nota creada con éxito!");
            }
            catch (Exception ex)
            {
                return Response.FillObject(null!, System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response> GetNote(int noteId)
        {
            try
            {
                Note note = await _noteService.Get(noteId);

                return Response.FillObject(NoteDTO.FillObject(note), System.Net.HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                return Response.FillObject(null!, System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response> GetNotes(int userId)
        {
            try
            {
                IEnumerable<Note> notes = await _noteService.GetAll(userId);

                if (notes == null) throw new Exception("Este usuario aún no tiene notas");

                List<NoteDTO> notesDTO = new List<NoteDTO>();
                foreach (Note note in notes)
                {
                    notesDTO.Add(NoteDTO.FillObject(note));
                }    

                return Response.FillObject(notesDTO, System.Net.HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                return Response.FillObject(null!, System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response> UpdateNote(int noteId, NoteRequest note)
        {
            try
            {
                Note noteGet = await _noteService.Get(noteId);

                noteGet.Description = note.Description;
                noteGet.FinishDate = note.FinishDate;

                Note noteUpdate = await _noteService.Update(noteGet);
                return Response.FillObject(NoteDTO.FillObject(noteUpdate), System.Net.HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                return Response.FillObject(null!, System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response> FinishNote(int noteId)
        {
            try
            {
                Note noteGet = await _noteService.Get(noteId);

                noteGet.Finished = true;

                Note noteUpdate = await _noteService.Update(noteGet);
                return Response.FillObject(NoteDTO.FillObject(noteUpdate), System.Net.HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                return Response.FillObject(null!, System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response> UnfinishNote(int noteId)
        {
            try
            {
                Note noteGet = await _noteService.Get(noteId);

                noteGet.Finished = false;

                Note noteUpdate = await _noteService.Update(noteGet);
                return Response.FillObject(NoteDTO.FillObject(noteUpdate), System.Net.HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                return Response.FillObject(null!, System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
