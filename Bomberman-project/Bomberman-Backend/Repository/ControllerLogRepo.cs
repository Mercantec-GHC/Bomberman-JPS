using Bomberman_Backend.Data;
using Bomberman_Backend.Repository.Interfaces;
using DomainModels;
using DomainModels.DTO;

namespace Bomberman_Backend.Repository
{
    public class ControllerLogRepo : IControllerLogRepo
    {
        private readonly DatabaseContext _databaseContext;
        public ControllerLogRepo(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ControllerLogs CreateControllerLog(CreateControllerLogsDTO createDTO)
        {
            return null;
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
