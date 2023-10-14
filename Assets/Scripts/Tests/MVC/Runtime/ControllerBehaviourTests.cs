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
        IView<TestModel> view = obj.AddComponent<TestViewBehaviour>();
        IController<TestModel> controller = obj.AddComponent<TestControllerBehaviour>();

        Assert.NotNull(controller.Model);
        Assert.AreEqual(controller.Model,view.Model);
    }

    [Test]
    public void InitModelAndViewClass()
    {
        GameObject obj = new GameObject();
        // there a no view component on game object
        IController<TestModel> controller = obj.AddComponent<TestControllerBehaviour>();
        // Not behaviour view
        IView<TestModel> view = new TestView();

        view.SetModel(controller.Model);

        Assert.NotNull(controller.Model);
        Assert.AreEqual(controller.Model,view.Model);
    }
}