using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon
{
    public string Name;
    public float fireRate;
    public float fireCooldown;
    public GameObject weaponObj;

    public abstract void Shoot(Vector2 direction, GameManager gameManager);
}
