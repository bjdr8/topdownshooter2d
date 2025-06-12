using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBulletFactory : BulletFactory
{
    public override Bullet CreateBullet(GameManager gameManager, Vector2 direction)
    {
        return new PistolBullet(gameManager, direction);
    }
}
