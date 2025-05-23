﻿using DomainModels.DTO;
using DomainModels;

namespace Bomberman_Backend.Services.Interfaces
{
    public interface IControllerLogService
    {
        public Task CreateControllerLog(CreateControllerLogsDTO createDTO);
        public List<ControllerLogs> GetControllerLogs();
        public void DeleteControllerLog(int id);
    }
}
