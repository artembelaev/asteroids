using AsteroidGame;
using NUnit.Framework;
using UnityEngine;

public class PlayerTests
{
    private const float DELTA = 0.00001f;

    [Test]
    public void TestConstructor()
    {
        int livesCount = 2;
        var player = new Player {LivesCount = livesCount};

        Assert.AreEqual(livesCount, player.LivesCount);
    }

    [Test]
    public void TestKill()
    {
        int livesCount = 2;
        var player = new Player {LivesCount = livesCount};

        bool killEventFired = false;
        player.OnKill += entity => killEventFired = true;

        player.Kill();

        Assert.IsFalse(player.IsKilled);
        Assert.IsFalse(killEventFired);
        Assert.AreEqual(1, player.LivesCount);

        player.Kill();

        // before respawn
        Assert.IsTrue(player.IsBlink);
        Assert.IsTrue(player.NeedRespawn);
        Assert.AreEqual(1, player.LivesCount);
        Assert.IsFalse(killEventFired);
        Assert.IsFalse(player.IsKilled);

        // after respawn
        player.Tick(player.RespawnDelay + 0.01f);
        player.Kill();

        Assert.IsTrue(player.IsBlink);
        Assert.IsFalse(player.NeedRespawn);
        Assert.AreEqual(1, player.LivesCount);
        Assert.IsFalse(killEventFired);
        Assert.IsFalse(player.IsKilled);

        // after blink period
        player.Tick(player.BlinkDuration + 0.01f);
        player.Kill();

        Assert.IsFalse(player.IsBlink);
        Assert.IsFalse(player.NeedRespawn);
        Assert.IsTrue(player.IsKilled);
        Assert.IsTrue(killEventFired);
        Assert.AreEqual(0, player.LivesCount);
    }
}