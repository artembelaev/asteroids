using AsteroidGame;
using NUnit.Framework;
using UnityEngine;

public class AsteroidTests
{
    private const float DELTA = 0.00001f;

    [Test]
    public void TestConstructor()
    {
        int level = 1;
        var asteroid = new Asteroid(level);

        Assert.IsTrue(Mathf.Approximately(asteroid.Level, level));
    }

    [Test]
    public void TestCreateChilds()
    {
        int level = 0;
        var asteroid = new Asteroid(level) {LevelCount = 2, ChildCount = 3};

        int childCount = 0;
        asteroid.OnCreateChild += velocity => { ++childCount; };

        asteroid.Kill();

        Assert.AreEqual(asteroid.ChildCount, childCount);
    }

    [Test]
    public void TestLeafAsteroidNotCreateChilds()
    {
        int level = 1;
        var asteroid = new Asteroid(level) {LevelCount = 2, ChildCount = 3};

        int childCount = 0;
        asteroid.OnCreateChild += velocity => { ++childCount; };

        asteroid.Kill();

        Assert.AreEqual(0, childCount);
    }

}