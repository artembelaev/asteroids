using AsteroidGame;
using NUnit.Framework;
using UnityEngine;

public class WeaponTests
{

    [Test]
    public void TestFireCoolDown()
    {
        var weapon = new Weapon
        {
            AmmoCount = 2,
            FireCooldown = 0.5f,
        };

        Weapon firedWeapon = null;
        weapon.OnFire += w => firedWeapon = w;

        weapon.Fire();

        Assert.AreEqual(weapon, firedWeapon);
        Assert.AreEqual(1, weapon.AmmoCount);

        // test cooldown
        firedWeapon = null;

        weapon.Tick(weapon.FireCooldown * 0.5f);
        weapon.Fire();

        Assert.AreEqual(null, firedWeapon);
        Assert.AreEqual(1, weapon.AmmoCount);

        weapon.Tick(weapon.FireCooldown * 0.5f + 0.01f);
        weapon.Fire();

        Assert.AreEqual(weapon, firedWeapon);
        Assert.AreEqual(0, weapon.AmmoCount);
    }

    [Test]
    public void TestFireZeroAmmo()
    {
        var weapon = new Weapon
        {
            AmmoCount = 0,
        };

        Weapon firedWeapon = null;
        weapon.OnFire += w => firedWeapon = w;

        weapon.Fire();

        Assert.AreEqual(null, firedWeapon);
        Assert.AreEqual(0, weapon.AmmoCount);

    }

    [Test]
    [TestCase(5, 3.5f, 4, 1)]
    [TestCase(3, 3.5f, 3, 0)]
    public void TestFireAuto(int ammo, float fireTimeAmount, int fireCountExpected, int ammoCountExpected)
    {
        var weapon = new Weapon
        {
            AmmoCount = ammo,
            FireCooldown = 1f,
        };

        int fireCount = 0;
        weapon.OnFire += w => ++fireCount;

        weapon.FireTrigger = true;
        float time = fireTimeAmount;

        for (float dt = 0.05f; time > 0f; time -= dt)
        {
            weapon.Tick(dt);
        }

        weapon.FireTrigger = false;

        Assert.AreEqual(fireCountExpected, fireCount);
        Assert.AreEqual(ammoCountExpected, weapon.AmmoCount);
    }

    [Test]
    [TestCase(0, 1f, 5, 0.5f, 2.1f,
        1, 0)]
    [TestCase(1, 1f, 1, 1f, 10.1f,
        10, 0)]
    public void TestWeaponReloader(
        int ammo, float fireCooldown, int maxAmmo, float reloadSpeed, float timeAmount,
        int reloadedAmmoCountExpected, int ammoCountExpected)
    {
        var weapon = new Weapon
        {
            AmmoCount = ammo,
            FireCooldown = fireCooldown,
        };

        var reloader = new WeaponReloaderPerOneAmmo
        {
            Weapon = weapon,
            MaxAmmo = maxAmmo,
            ReloadSpeed = reloadSpeed,
        };

        int fireCount = 0;
        weapon.OnFire += w => ++fireCount; 
        
        int reloadAmmoCount = 0;
        reloader.OnReload += (w, count) =>
        {
            Assert.AreEqual(weapon, w);
            reloadAmmoCount += count;
        };

        weapon.FireTrigger = true;
        float time = timeAmount;

        for (float dt = 0.05f; time > 0f; time -= dt)
        {
            reloader.Tick(dt);
            weapon.Tick(dt);
        }

        weapon.FireTrigger = false;


        Assert.AreEqual(reloadedAmmoCountExpected, reloadAmmoCount);
        Assert.AreEqual(ammoCountExpected, weapon.AmmoCount);
    }

}