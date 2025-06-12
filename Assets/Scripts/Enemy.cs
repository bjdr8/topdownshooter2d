using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEnemy
{
    public Enemy(GameObject player, GameObject enemy) : base(player, enemy)
    {
        hp = 3;
        moveSpeed = 2f;
        damage = 1;
    }
}
