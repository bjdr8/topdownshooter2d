using UnityEngine;

public abstract class Bullet
{
    public GameObject bulletObject;
    protected GameManager gameManager;
    protected Vector2 direction;
    protected Rigidbody2D rb;
    public float lifeSpan = 30;
    public float damage;
    public float speed;
    public float size;

    public Bullet(GameManager gameManager, Vector2 direction)
    {
        this.gameManager = gameManager;
        bulletObject = GameObject.Instantiate(this.gameManager.bulletObjectPrefab, gameManager.player.transform.position, Quaternion.identity);
        rb = bulletObject.GetComponent<Rigidbody2D>();
        ShootDirection(direction);
    }
    public void ShootDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    public void MoveBullet()
    {
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        lifeSpan -= Time.deltaTime;
    }
}
