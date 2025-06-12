using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class MiniGun : Weapon
{
    MinigunFactory minigunFactory = new MinigunFactory();
    public MiniGun(GameObject weaponObject)
    {
        Name = "MiniGun";
        fireRate = 0.25f;
        fireCooldown = 0.25f;
        weaponObj = weaponObject;
    }

    public override void Shoot(Vector2 direction, GameManager gameManager)
    {
        gameManager.bulletList.Add(minigunFactory.orderBullet(direction, gameManager));
    }
}
