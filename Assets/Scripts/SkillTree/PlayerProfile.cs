using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile
{
    public event Action OnXpChanged;
    public int xp { get; private set; } = 500;
    private int ProfileLevel;

    public PlayerProfile()
    {

    }
    public void AddXp(int xp)
    {
        this.xp += xp;
        OnXpChanged.Invoke();
        Debug.Log(this.xp);
    }

    public void RemoveXp(int xp)
    {
        this.xp -= xp;
        OnXpChanged.Invoke();
        Debug.Log(this.xp);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
