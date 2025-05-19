using DomainModels.DTO;


namespace DomainModels.Mapper
{
    public static class ControllerLogsMapper
    {
        public static ControllerLogs toCreateControllerLog(this CreateControllerLogsDTO controllerLogsDTO)
        {
            return new ControllerLogs
            {
                PlayerID = controllerLogsDTO.PlayerID,
                TimeStamp = controllerLogsDTO.TimeStamp,
                InputType = controllerLogsDTO.InputType
            };
        }
    }
}
