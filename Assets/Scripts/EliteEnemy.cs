using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEnemy : BaseEnemy
{
    public EliteEnemy(GameObject player, GameObject enemy) : base(player, enemy)
    {
        hp = 10;
        moveSpeed = 4f;
    }
}
