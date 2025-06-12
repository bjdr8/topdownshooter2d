using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemy
{
    public GameObject enemy;
    protected GameObject target;
    public CircleCollider2D collider;

    protected Vector2 moveDirection;
    protected float moveSpeed;
    public float hp;
    public int damage;

    protected Rigidbody2D rb;
    public BaseEnemy(GameObject player, GameObject enemy)
    {
        target = player;
        this.enemy = enemy;

        rb = this.enemy.GetComponent<Rigidbody2D>();
        collider = this.enemy.GetComponent<CircleCollider2D>();
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
