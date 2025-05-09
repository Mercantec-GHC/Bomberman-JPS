namespace DomainModels
{
    public class Bomb
    {
        public int Id { get; set; }
 
        public string yCordinate { get; set; }
        public string xCordinate { get; set; }
        public int explosionRadius { get; set; }
        public int fuseTime { get; set; }

    }
}