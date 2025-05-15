using Bomberman_Backend.Repository.Interfaces;
using Bomberman_Backend.Services.Interfaces;
using DomainModels.DTO;
using DomainModels;

namespace Bomberman_Backend.Services
{
    public class ControllerLogService : IControllerLogSerivce
    {
        private readonly IControllerLogRepo _controllerLogRepo;

        public Task CreateControllerLog(CreateControllerLogsDTO createDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteControllerLog(int id)
        {
             _controllerLogRepo.DeleteControllerLog(id);
        }

        public List<ControllerLogs> GetControllerLogs()
        {
            return _controllerLogRepo.GetControllerLogs();
        }
    }
}
