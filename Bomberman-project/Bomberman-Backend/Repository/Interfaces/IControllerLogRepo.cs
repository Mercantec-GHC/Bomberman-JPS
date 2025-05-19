using DomainModels;
using DomainModels.DTO;


namespace Bomberman_Backend.Repository.Interfaces
{
    public interface IControllerLogRepo
    {
        public Task CreateControllerLogs(CreateControllerLogsDTO createDTO);
        List<ControllerLogs> GetControllerLogs();
        public void DeleteControllerLog(int id);
    }
}
