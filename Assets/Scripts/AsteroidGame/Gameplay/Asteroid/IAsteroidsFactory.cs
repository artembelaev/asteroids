namespace AsteroidGame
{
    public interface IAsteroidsFactory
    {
        Character Create(int level);
        void ClearAll();
    }
}