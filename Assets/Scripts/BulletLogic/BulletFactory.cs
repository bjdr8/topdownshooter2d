using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletFactory
{
    public Bullet orderBullet(Vector2 direction, GameObject bulletObject, GameManager gameManager)
    {
        Bullet bullet = CreateBullet(bulletObject ,gameManager);
        bullet.Shoot(direction);
        return bullet;
    }
    public abstract Bullet CreateBullet(GameObject bulletObject ,GameManager gameManager);
}
