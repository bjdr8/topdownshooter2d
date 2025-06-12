using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArBullet : Bullet
{
    public ArBullet(GameObject bulletObjectPrefab, GameManager gameManager) : base(bulletObjectPrefab, gameManager) 
    { 
        damage = 1;
        speed = 5f;
        size = 1;

        this.gameManager.bulletList.Add(this);
    }

    public override void Shoot(Vector2 direction)
    {
        bulletObject.transform.position = new Vector2(direction.x, direction.y).normalized * speed;
    }
}
