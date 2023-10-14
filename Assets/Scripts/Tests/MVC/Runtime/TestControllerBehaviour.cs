using MVC;
using Tests.MVC.Editor;

public class TestControllerBehaviour : ControllerBehaviour
{
    protected override object CreateModel()
    {
        return new TestModel();
    }
}