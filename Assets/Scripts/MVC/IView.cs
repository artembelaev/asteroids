namespace MVC
{
    public interface IView<T> where T : class
    {
        T Model { get; }
        void SetModel(T model);

    }
}