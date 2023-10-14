using NUnit.Framework;
using Tests.MVC.Editor;
using UnityEngine;

public class ViewBehaviourTests
{
    [Test]
    public void SetModel()
    {
        GameObject obj = new GameObject();
        var view = obj.AddComponent<TestViewBehaviour>();

        TestModel model = new ();

        view.SetModel(model);
        Assert.AreEqual(view.Model,model);
    }
}