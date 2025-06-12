using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunFactory : BulletFactory
{
    public override Bullet CreateBullet(GameManager gameManager, Vector2 direction)
    {
        return new MinigunBullet(gameManager, direction);
    }
}
