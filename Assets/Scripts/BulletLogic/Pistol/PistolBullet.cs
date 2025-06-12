using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : Bullet
{
    public PistolBullet(GameManager gameManager, Vector2 direction) : base(gameManager, direction)
    {
        damage = 3;
        speed = 5f;
        size = 2;

        this.gameManager.bulletList.Add(this);
    }
}
