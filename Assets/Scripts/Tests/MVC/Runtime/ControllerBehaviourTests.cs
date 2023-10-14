using MVC;
using NUnit.Framework;
using Tests.MVC.Editor;
using UnityEngine;

public class ControllerBehaviourTests
{

    [Test]
    public void InitModelAndViewBehaviour()
    {
        GameObject obj = new GameObject();
        IView view = obj.AddComponent<TestViewBehaviour>();
        IController controller = obj.AddComponent<TestControllerBehaviour>();

        Assert.NotNull(controller.Model);
        Assert.AreEqual(controller.Model,view.Model);
    }

    [Test]
    public void InitModelAndViewClass()
    {
        GameObject obj = new GameObject();
        // there a no view component on game object
        IController controller = obj.AddComponent<TestControllerBehaviour>();
        // Not behaviour view
        IView view = new TestView();

        view.SetModel(controller.Model);

        Assert.NotNull(controller.Model);
        Assert.AreEqual(controller.Model,view.Model);
    }
}