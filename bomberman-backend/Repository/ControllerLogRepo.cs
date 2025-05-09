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

    }
}
