using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ar : Weapon
{
    ArBulletFactory ArBulletFactory = new ArBulletFactory();
    public Ar(GameObject weaponObject)
    {
        Name = "Ar";
        fireRate = 0.5f;
        fireCooldown = 0.5f;
        weaponObj = weaponObject;
    }

    public override void Shoot(Vector2 direction, GameManager gameManager)
    {
        gameManager.bulletList.Add(ArBulletFactory.orderBullet(direction, gameManager));
    }
}
