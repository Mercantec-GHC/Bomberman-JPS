using bomberman_backend.Data;
using bomberman_backend.Repository.Interfaces;
using DomainModels;
using DomainModels.DTO;

namespace bomberman_backend.Repository
{
   public class ControllerLogRepo : IControllerLogRepo
   {
        private readonly DatabaseContextcs _databaseContext;
        public ControllerLogRepo(DatabaseContextcs databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ControllerLogs CreateControllerLog(CreateControllerLogsDTO createDTO)
        {

        }

        public List<ControllerLogs> GetControllerLogs() 
        {
            var controllerlogs = _databaseContext.controllerLogs.ToList();
            List<ControllerLogs> controllerLogsList = new List<ControllerLogs>();
            foreach (var controllerlog in controllerlogs)
            {
                var controllerLog = new ControllerLogs
                {
                    Id = controllerlog.Id,
                    Player = controllerlog.Player,
                    TimeStamp = controllerlog.TimeStamp
                };
                controllerLogsList.Add(controllerlog);
            }
            return controllerLogsList;
        }

        public void DeleteControllerLog(int id)
        {
            var controllerLog = _databaseContext.controllerLogs.Find(id);

            if (controllerLog == null)
            {
                throw new Exception("Controller log not found");
            }
            _databaseContext.controllerLogs.Remove(controllerLog);
            _databaseContext.SaveChanges();
        }
   }
}
