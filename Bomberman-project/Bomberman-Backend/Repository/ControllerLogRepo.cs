using Bomberman_Backend.Data;
using Bomberman_Backend.Repository.Interfaces;
using DomainModels;
using DomainModels.DTO;
using Microsoft.EntityFrameworkCore;

namespace Bomberman_Backend.Repository
{
    public class ControllerLogRepo : IControllerLogRepo
    {
        private readonly DatabaseContext _databaseContext;
        public ControllerLogRepo(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task CreateControllerLog(CreateControllerLogsDTO createDTO, InputType inputType)
        {
            var log = new ControllerLogs
            {
                Player = createDTO.Player,
                TimeStamp = createDTO.TimeStamp,
                InputType = inputType
            };

            await _databaseContext.controllerLogs.AddAsync(log);
            await _databaseContext.SaveChangesAsync();
        }

        public List<ControllerLogs> GetControllerLogs()
        {
            return _databaseContext.controllerLogs
            .Include(cl => cl.InputType)
            .ToList(); 
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
