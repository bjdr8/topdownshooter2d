using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet
{
    public GameObject bulletObject;
    protected GameManager gameManager;
    public float damage;
    public float speed;
    public float size;

    public Bullet(GameObject bulletObjectPrefab, GameManager gameManager)
    {
        bulletObject = GameObject.Instantiate(bulletObjectPrefab);
        this.gameManager = gameManager;
    }
    public abstract void Shoot(Vector2 direction);
}
