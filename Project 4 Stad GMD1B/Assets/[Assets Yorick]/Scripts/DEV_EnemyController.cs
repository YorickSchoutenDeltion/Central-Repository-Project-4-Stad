using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_EnemyController : MonoBehaviour {

    public float health;

    public void TakeDamage(float f)
    {
        health -= f;
        if(health < 0)
        {
            Destroy(gameObject);
        }
    }

}
