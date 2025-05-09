namespace DomainModels
{
    public class PowerUp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Effect Effect { get; set; }
        public int duration { get; set; }

    }

    public enum Effect
    {
        placeholder,
        placeholder2,
        placeholder3
    }
}