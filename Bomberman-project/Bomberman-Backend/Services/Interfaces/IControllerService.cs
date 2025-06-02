using DomainModels;
using DomainModels.DTO;

namespace Bomberman_Backend.Services.Interfaces
{
    public interface IControllerService
    {
        public Task<Controller> GetControllerByIdAsync(int id);
        public Task<IEnumerable<Controller>> GetAllControllersAsync();
        public Task<Controller> AddControllerAsync(Controller controller);
        public Task<UpdateControllerLEDBrightnessDTO> UpdateLEDBrightness(UpdateControllerLEDBrightnessDTO controller);
        public Task<UpdateControllerPlayerColorDTO> UpdatePlayerColorAsync(UpdateControllerPlayerColorDTO controller);
        public Task<UpdateControllerPlayerDTO> UpdatePlayerAsync(UpdateControllerPlayerDTO controller);
        public Task<UpdateControllerDTO> UpdateControllerAsync(UpdateControllerDTO controller);
        public Task<bool> DeleteControllerAsync(int id);
        public Task<bool> ControllerExistsAsync(int id);
    }
}
