using DomainModels;
using DomainModels.DTO;


namespace Bomberman_Backend.Repository.Interfaces
{
    public interface IControllerLogRepo
    {
        Task CreateControllerLog(CreateControllerLogsDTO createDTO, InputType inputType);
        List<ControllerLogs> GetControllerLogs();
        void DeleteControllerLog(int id);
    }
}
