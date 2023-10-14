using MVC;

namespace Tests.MVC.Editor
{
    public class TestView : IView
    {
        public object Model { get; private set; }

        public void SetModel(object model)
        {
            Model = model;
        }

    }
}