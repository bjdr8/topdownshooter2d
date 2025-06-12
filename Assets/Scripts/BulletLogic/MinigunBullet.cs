using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MinigunBullet : Bullet
{
    public MinigunBullet(GameManager gameManager, Vector2 direction) : base(gameManager, direction)
    {
        damage = 1;
        speed = 5f;
        size = 0.5f;

        this.gameManager.bulletList.Add(this);
    }
}
