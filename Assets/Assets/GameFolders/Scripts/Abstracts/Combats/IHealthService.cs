namespace BumperCarGamePrototype.Abstracts.Combats
{
    public interface IHealthService
    {
        int CurrentHealth { get; set; }
        bool IsDead { get; set; }
    }
}