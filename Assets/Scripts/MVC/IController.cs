namespace MVC
{
    public interface IController<T> where T : class
    {
        T Model { get; }
    }
}