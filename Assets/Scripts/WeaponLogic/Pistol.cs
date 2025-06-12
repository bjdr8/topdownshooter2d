using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Pistol : Weapon
{
    private PistolBulletFactory pistolBulletFactory = new PistolBulletFactory();
    public Pistol(GameObject weaponObject)
    {
        Name = "Pistol";
        fireRate = 1f;
        fireCooldown = 1f;
        weaponObj = weaponObject;
    }

    public override void Shoot(Vector2 direction, GameManager gameManager)
    {
        gameManager.bulletList.Add(pistolBulletFactory.orderBullet(direction, gameManager));
    }
}
