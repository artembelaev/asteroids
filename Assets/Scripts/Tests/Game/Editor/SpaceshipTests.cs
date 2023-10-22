using AsteroidGame;
using NUnit.Framework;
using UnityEngine;

public class SpaceshipTests
{
    private const float DELTA = 0.00001f;

    [Test]
    public void TestConstructor()
    {
        float acceleration = 1f;
        var ship = new Spaceship {Acceleration = acceleration};

        Assert.IsTrue(Mathf.Approximately(ship.Acceleration, acceleration));
    }


    [Test]
    [TestCase(1f, 0f, 0f, 1f,
        0f, 1f, 0f, 1f)]
    [TestCase(1f, 0f, -90f, 1f,
        1f, 0f, 1f, 0f)]
    public void TestMovement(float acceleration, float velocity, float rotation, float dt,
        float resultVelX, float resultVelY, float resultPosX, float resultPosY)
    {
        Vector2 velocityVector = velocity * Vector2.up;
        var ship = new Spaceship
        {
            Acceleration = acceleration, Position = Vector2.zero,
            Rotation = rotation, Velocity = velocityVector
        };

        bool engineEnabled = false;
        ship.OnEngineEnabled += b => engineEnabled = b;

        // Engine off
        Assert.IsFalse(ship.EngineEnabled);
        ship.Tick(dt);
        Assert.IsTrue(Vector2.Distance(ship.Velocity, velocityVector) < DELTA);
        Assert.IsTrue(Vector2.Distance(ship.Position, Vector2.zero) < DELTA);

        // Engine on
        ship.EnableEngine(true);
        Assert.IsTrue(ship.EngineEnabled);
        Assert.IsTrue(engineEnabled);
        ship.Tick(dt);
        Vector2 resultVelocity = new Vector2(resultVelX, resultVelY);
        Assert.IsTrue(Vector2.Distance(ship.Velocity, resultVelocity) < DELTA);
        Vector2 resultPos = new Vector2(resultPosX, resultPosY);
        Assert.IsTrue(Vector2.Distance(ship.Position, resultPos) < DELTA);

        // Engine off
        ship.EnableEngine(false);
        Assert.IsFalse(engineEnabled);
        ship.Tick(dt);
        Assert.IsTrue(Vector2.Distance(ship.Velocity, resultVelocity) < DELTA);
        Assert.IsTrue(Vector2.Distance(ship.Position, resultPos + resultVelocity * dt) < DELTA);
    }
}