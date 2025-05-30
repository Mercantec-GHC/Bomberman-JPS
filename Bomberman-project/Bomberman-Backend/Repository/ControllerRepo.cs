using Bomberman_Backend.Data;
using Bomberman_Backend.Repository.Interfaces;
using DomainModels;
using DomainModels.DTO;

namespace Bomberman_Backend.Repository
{
    public class ControllerRepo : IControllerRepo
    {
        private readonly DatabaseContext _context;
        public ControllerRepo(DatabaseContext context) 
        {
            _context = context;
        }
        public Task<Controller> AddControllerAsync(Controller controller)
        {
            _context.Gyroscopes.Add(controller.Gyroscope);
            _context.Buttons.Add(controller.Buttons);
            _context.Controllers.Add(controller);
           _context.SaveChanges();
            return Task.FromResult(controller);
        }

        public Task<bool> ControllerExistsAsync(int id)
        {
            var controller = _context.Controllers.Find(id);
            if(controller == null)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<bool> DeleteControllerAsync(int id)
        {
            var controller = _context.Controllers.Find(id);
            if (controller == null)
            {
                return Task.FromResult(false);
            }
            _context.Controllers.Remove(controller);
            _context.SaveChangesAsync();
            return Task.FromResult(true);
        }

        public Task<IEnumerable<Controller>> GetAllControllersAsync()
        {
            var controllers = _context.Controllers.ToList();
            return Task.FromResult<IEnumerable<Controller>>(controllers);
        }

        public Task<Controller> GetControllerByIdAsync(int id)
        {
            var controller = _context.Controllers.Find(id);
            if (controller == null)
            {
                return Task.FromResult<Controller>(null);
            }
            return Task.FromResult(controller);
        }

        public Task<UpdateControllerLEDBrightnessDTO> UpdateLEDBrightness(UpdateControllerLEDBrightnessDTO controller)
        {
            var existingController = _context.Controllers.Find(controller.Id);
            if (existingController == null)
            {
                return Task.FromResult<UpdateControllerLEDBrightnessDTO>(null);
            }
            existingController.ledBrightness = controller.LEDBrightness;
            _context.Controllers.Update(existingController);
            _context.SaveChanges();
            return Task.FromResult(new UpdateControllerLEDBrightnessDTO
            {
                Id = existingController.Id,
                LEDBrightness = existingController.ledBrightness
            });
        }

        public Task<UpdateControllerPlayerColorDTO> UpdatePlayerColorAsync(UpdateControllerPlayerColorDTO controller)
        {
            var existingController = _context.Controllers.Find(controller.Id);
            if (existingController == null)
            {
                return Task.FromResult<UpdateControllerPlayerColorDTO>(null);
            }
            existingController.playerColor = controller.PlayerColor;
            _context.Controllers.Update(existingController);
            _context.SaveChangesAsync();
            return Task.FromResult(new UpdateControllerPlayerColorDTO
            {
                Id = existingController.Id,
                PlayerColor = existingController.playerColor
            });
        }
        
        public Task<UpdateControllerPlayerDTO> UpdatePlayerAsync(UpdateControllerPlayerDTO controller)
        {
            var existingController = _context.Controllers.Find(controller.Id);
            if (existingController == null)
            {
                return Task.FromResult<UpdateControllerPlayerDTO>(null);
            }
            existingController.playerId = controller.playerId;
            _context.Controllers.Update(existingController);
            _context.SaveChangesAsync();
            return Task.FromResult(new UpdateControllerPlayerDTO
            {
                Id = existingController.Id,
                playerId = existingController.playerId
            });
        }

        public Task<UpdateControllerDTO> UpdateControllerAsync(UpdateControllerDTO controller)
        {
            var existingController = _context.Controllers.Find(controller.Id);
            if (existingController == null)
            {
                return Task.FromResult<UpdateControllerDTO>(null);
            }
            existingController.playerColor = controller.playerColor;
            existingController.ledBrightness = controller.ledBrightness;
            existingController.playerId = controller.playerId;
            _context.Controllers.Update(existingController);
            _context.SaveChangesAsync();
            return Task.FromResult(new UpdateControllerDTO
            {
                Id = existingController.Id,
                playerColor = existingController.playerColor,
                ledBrightness = existingController.ledBrightness,
                playerId = existingController.playerId
            });
        }
    }
}
