using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy
{
    protected GameObject enemy;
    protected GameObject target;

    protected Vector2 moveDirection;
    protected float moveSpeed;
    protected int hp;

    protected Rigidbody2D rb;
    public BaseEnemy(GameObject player, GameObject enemy)
    {
        target = player;
        this.enemy = enemy;

        rb = this.enemy.GetComponent<Rigidbody2D>();
    }

    public virtual void DirectionCalc()
    {
        Vector2 direction = (target.transform.position - enemy.transform.position).normalized;
        moveDirection = direction;
    }

    public void Move()
    {
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
    }

    public void MoveEnemy()
    {
        DirectionCalc();
        Move();
    }
}
