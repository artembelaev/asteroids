namespace MVC
{
    public interface IController
    {
        object Model { get; }

        T GetModel<T>() where T : class;
    }
}