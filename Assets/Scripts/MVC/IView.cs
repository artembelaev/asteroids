namespace MVC
{
    public interface IView
    {
        object Model { get; }
        void SetModel(object model);

    }
}