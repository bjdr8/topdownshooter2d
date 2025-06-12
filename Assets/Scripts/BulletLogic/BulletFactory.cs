using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletFactory
{
    public Bullet orderBullet(Vector2 direction, GameManager gameManager)
    {
        Bullet bullet = CreateBullet(gameManager, direction);
        return bullet;
    }
    public abstract Bullet CreateBullet(GameManager gameManager, Vector2 direction);
}
