using MVC;
using Tests.MVC.Editor;

public class TestControllerBehaviour : ControllerBehaviour<TestModel>
{
    protected override TestModel CreateModel()
    {
        return new TestModel();
    }
}