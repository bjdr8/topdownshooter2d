using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArBulletFactory : BulletFactory
{
    public override Bullet CreateBullet(GameObject bulletObject, GameManager gameManager)
    {
        return new ArBullet(bulletObject, gameManager);
    }
}
