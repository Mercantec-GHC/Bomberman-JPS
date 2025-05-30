using Bomberman_Backend.Repository.Interfaces;
using Bomberman_Backend.Services.Interfaces;
using DomainModels;
using DomainModels.DTO;

namespace Bomberman_Backend.Services
{
    public class ControllerService : IControllerService
    {
        private readonly IControllerRepo _controllerRepo;
        public ControllerService(IControllerRepo controllerRepo)
        {
            _controllerRepo = controllerRepo;
        }
        public Task<Controller> AddControllerAsync(Controller controller)
        {
            return _controllerRepo.AddControllerAsync(controller);
        }

        public Task<bool> ControllerExistsAsync(int id)
        {
            return _controllerRepo.ControllerExistsAsync(id);
        }

        public Task<bool> DeleteControllerAsync(int id)
        {
            return _controllerRepo.DeleteControllerAsync(id);
        }

        public Task<IEnumerable<Controller>> GetAllControllersAsync()
        {
            return _controllerRepo.GetAllControllersAsync();
        }

        public Task<Controller> GetControllerByIdAsync(int id)
        {
           return _controllerRepo.GetControllerByIdAsync(id);
        }

        public Task<UpdateControllerDTO> UpdateControllerAsync(UpdateControllerDTO controller)
        {
            return _controllerRepo.UpdateControllerAsync(controller);
        }

        public Task<UpdateControllerLEDBrightnessDTO> UpdateLEDBrightness(UpdateControllerLEDBrightnessDTO controller)
        {
            return _controllerRepo.UpdateLEDBrightness(controller);
        }

        public Task<UpdateControllerPlayerDTO> UpdatePlayerAsync(UpdateControllerPlayerDTO controller)
        {
            return _controllerRepo.UpdatePlayerAsync(controller);
        }

        public Task<UpdateControllerPlayerColorDTO> UpdatePlayerColorAsync(UpdateControllerPlayerColorDTO controller)
        {
            return _controllerRepo.UpdatePlayerColorAsync(controller);
        }
    }
}
