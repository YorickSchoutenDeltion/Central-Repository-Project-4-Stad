using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_ResourceManager : MonoBehaviour {

    public int health;
    public int medkits;
    public int maximumHealth;

    public int ammo;

    public void HealthDecrease(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            //gameover
        }
    }

    public void HealthIncrease(int amount)
    {
        health += amount;
        if (health > maximumHealth)
        {
            health = maximumHealth;
        }
    }

    public void AmmoIncrease(int amount)
    {
        ammo += amount;
    }
}
