using UnityEngine;

public class EliteEnemy : BaseEnemy
{
    public EliteEnemy(GameObject player, GameObject enemy) : base(player, enemy)
    {
        hp = 10;
        moveSpeed = 3.5f;
        damage = 2;
        xpWorth = 5;
    }
}
