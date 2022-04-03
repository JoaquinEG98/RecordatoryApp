using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Note
    {
        #region Dependency injection
        private readonly IUnitOfWork _unitOfWork;

        public Note(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<Models.Note> Add(Models.Note note)
        {
            ValidateNote(note);

            await _unitOfWork.Notes.AddAsync(note);
            await _unitOfWork.SaveAsync();

            return note;
        }

        public Models.Note Update(Models.Note note)
        {
            ValidateNote(note);

            _unitOfWork.Notes.Update(note);
            _unitOfWork.Save();

            return note;
        }

        public async Task<Models.Note> Get(int noteId)
        {
            return await _unitOfWork.Notes.GetAsync(noteId);
        }

        public async Task<IEnumerable<Models.Note>> GetAll(int userId)
        {
            IEnumerable<Models.Note> lista = await _unitOfWork.Notes.GetAllAsync();
            return lista.Where(x => x.User!.Id == userId);
        }
        #endregion

        #region Tools
        private void ValidateNote(Models.Note note)
        {
            if (note.Description == null) throw new Exception("Se debe completar la descripción de la nota");
        }
        #endregion
    }
}
