using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    // Logger with Singleton
    public sealed class LogSingleton
    {
        private static LogSingleton _instance = null!;
        private static object _protect = new object();

        private LogSingleton()
        {
        }

        public static LogSingleton GetInstance()
        {
            // Utilizo el lock para proteger el hilo de mi instancia.
            lock (_protect)
            {
                if (_instance == null)
                {
                    _instance = new LogSingleton();
                }
            }

            return _instance;
        }

        public void Add(IUnitOfWork _unitOfWork ,Models.Logger logger)
        {
            _unitOfWork.Logs.Add(logger);
        }
    }
}
