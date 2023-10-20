using AsteroidGame;
using NUnit.Framework;
using UnityEngine;

public class PlayerTests
{
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
        player.OnKill += ch => killEventFired = true;

        player.Kill();

        Assert.IsFalse(player.IsKilled);
        Assert.IsFalse(killEventFired);
        Assert.AreEqual(1, player.LivesCount);

        player.Kill();

        Assert.IsTrue(player.IsKilled);
        Assert.IsTrue(killEventFired);
        Assert.AreEqual(0, player.LivesCount);

        killEventFired = false;
        player.Kill();

        Assert.IsTrue(player.IsKilled);
        Assert.IsFalse(killEventFired);
        Assert.AreEqual(0, player.LivesCount);
    }

    [Test]
    public void TestRespawn()
    {
        int livesCount = 2;
        var player = new Player {LivesCount = livesCount};

        bool killEventFired = false;
        player.OnKill += ch => killEventFired = true;

        bool respawnEventFired = false;
        player.OnRespawn += (p, pos) => respawnEventFired = true;

        player.Kill();

        Assert.IsFalse(player.IsKilled);
        Assert.IsFalse(respawnEventFired);
        Assert.IsFalse(killEventFired);
        Assert.IsTrue(player.NeedRespawn);

        player.Tick(player.RespawnDelay + 0.01f); // Wait for respawn

        Assert.IsFalse(player.IsKilled);
        Assert.IsTrue(respawnEventFired);
        Assert.IsFalse(killEventFired);
        Assert.IsFalse(player.NeedRespawn);

        respawnEventFired = false;
        player.Kill();

        Assert.IsTrue(player.IsKilled);
        Assert.IsTrue(killEventFired);
        Assert.IsFalse(respawnEventFired);
        Assert.IsFalse(player.NeedRespawn);
    }

    [Test]
    public void TestBlinkAfterRespawn()
    {
        var player = new Player {BlinkDuration = 1f};

        player.Respawn();
        Assert.IsTrue(player.IsBlink);

        player.Tick(0.5f);
        Assert.IsTrue(player.IsBlink);

        player.Tick(0.55f);
        Assert.IsFalse(player.IsBlink);
    }

}