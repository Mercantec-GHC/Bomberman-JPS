namespace DomainModels
{
public class PlayerInput
{
    public string MoveDirection { get; set; } = "Idle";
    public bool BombPlaced { get; set; }
    public bool PowerUpUsed { get; set; }

    public void ResetActions()
    {
        BombPlaced = false;
        PowerUpUsed = false;
    }
}
}