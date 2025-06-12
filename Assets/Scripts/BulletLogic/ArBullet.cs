using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArBullet : Bullet
{
    public ArBullet(GameManager gameManager, Vector2 direction) : base(gameManager, direction) 
    { 
        damage = 1;
        speed = 5f;
        size = 1;

        this.gameManager.bulletList.Add(this);
    }
}
