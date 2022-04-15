using Helpers;
using Models;
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

            Logger logger = new Logger()
            {
                Description = "Creó una nota",
                LogDate = DateTime.Now,
                User = note.User,
            };
            LogSingleton.GetInstance().Add(_unitOfWork, logger);

            await _unitOfWork.SaveAsync();

            return note;
        }

        public async Task <Models.Note> Update(Models.Note note)
        {
            ValidateNote(note);

            _unitOfWork.Notes.Update(note);

            Logger logger = new Logger()
            {
                Description = "Editó una nota",
                LogDate = DateTime.Now,
                User = note.User,
            };
            LogSingleton.GetInstance().Add(_unitOfWork, logger);

            await _unitOfWork.SaveAsync();

            return note;
        }

        public async Task<Models.Note> Get(int noteId)
        {
            Models.Note note = await _unitOfWork.Notes.GetAsync(noteId);

            Logger logger = new Logger()
            {
                Description = "Obtuvo una nota",
                LogDate = DateTime.Now,
                User = note.User
            };
            LogSingleton.GetInstance().Add(_unitOfWork, logger);
            await _unitOfWork.SaveAsync();

            return note;
        }

        public async Task<IEnumerable<Models.Note>> GetAll(int userId)
        {
            IEnumerable<Models.Note> list = await _unitOfWork.Notes.GetAllAsync();
            IEnumerable<Models.Note> getAll = list.Where(x => x.User!.Id == userId && x.Finished == false).OrderBy(f => f.FinishDate);

            if (getAll.Count() > 0)
            {
                Logger logger = new Logger()
                {
                    Description = "Obtuvo todas sus notas",
                    LogDate = DateTime.Now,
                    User = getAll.FirstOrDefault()!.User,
                };
                LogSingleton.GetInstance().Add(_unitOfWork, logger);
                await _unitOfWork.SaveAsync();
            }

            return getAll!;
        }
        #endregion

        #region Tools
        private void ValidateNote(Models.Note note)
        {
            if (string.IsNullOrWhiteSpace(note.Description)) throw new Exception("Se debe completar la descripción de la nota");
            if (note.FinishDate == null) throw new Exception("Se debe elegir una fecha de finalización");
        }
        #endregion
    }
}
