using AsteroidGame;
using NUnit.Framework;
using UnityEngine;

public class CharacterTests
{
    private const float DELTA = 0.00001f;

    [Test]
    public void TestConstructor()
    {
        Vector2 position = Vector2.one;
        float rotation = 45f;
        Vector2 velocity = Vector2.one;
        float maxVelocity = 30f;
        Character ch = new Character(position, rotation, velocity, maxVelocity);


        Assert.IsTrue(Vector2.Distance(ch.Position, position) < DELTA);
        Assert.IsTrue(Mathf.Approximately(ch.Rotation, rotation));
        Assert.IsTrue(Vector2.Distance(ch.Velocity, velocity) < DELTA);
        Assert.IsTrue(Mathf.Approximately(ch.MaxVelocity, maxVelocity));
    }

    [Test]
    public void TestMovement()
    {
        Character ch = new Character(Vector2.zero, 0f, Vector2.one, 10f);

        ch.Tick(1f);

        Assert.IsTrue(Vector2.Distance(ch.Position, Vector2.one) < DELTA);
    }

    [Test]
    public void TestMaxVelocity()
    {
        float maxVelocity = 10f;
        Vector2 velocity = Vector2.right * 20f;
        Character ch = new Character(Vector2.zero, 0f, velocity, maxVelocity);

        Assert.IsTrue(Vector2.Distance(ch.Velocity, Vector2.right * maxVelocity) < DELTA);

        ch.Velocity = 100f * Vector2.right;
        Assert.IsTrue(Vector2.Distance(ch.Velocity, maxVelocity * Vector2.right) < DELTA);

        ch.Velocity = Vector2.up;
        Assert.IsTrue(Vector2.Distance(ch.Velocity, Vector2.up) < DELTA);

    }

    [Test]
    public void TestKill()
    {
        Character ch = new Character(velocity: Vector2.right);
        bool killEventFired = false;
        ch.OnKill += ch => killEventFired = true;

        ch.Kill();

        Assert.IsTrue(ch.IsKilled);
        Assert.IsTrue(killEventFired);

        killEventFired = false;
        ch.Kill();
        Assert.IsFalse(killEventFired);
    }

    [Test]
    public void TestPositionModifier()
    {
        Character ch = new Character(position: Vector2.zero, velocity: Vector2.zero);
        TestPositionModifier modifier = new ()
        {
            ModifiedPosition = Vector2.one
        };
        ch.AddPositionModifier(modifier);

        ch.Tick(1f);

        Assert.IsTrue(Vector2.Distance(ch.Position, modifier.ModifiedPosition) < DELTA);

        ch.RemovePositionModifier(modifier);
        Vector2 newPosition = Vector2.up;
        ch.Position = newPosition;
        ch.Tick(1f);
        Assert.IsTrue(Vector2.Distance(ch.Position, newPosition) < DELTA);

    }

}