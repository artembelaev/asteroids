using MVC;

namespace Tests.MVC.Editor
{
    public class TestView : IView<TestModel>
    {
        public TestModel Model { get; private set; }

        public void SetModel(TestModel model)
        {
            Model = model;
        }
    }
}