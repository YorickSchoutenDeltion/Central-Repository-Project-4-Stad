using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_WeaponDamage : MonoBehaviour {

    public float wepDamage;

    public bool isSwinging;

    void OnCollisionEnter(Collision col)
    {
        if(isSwinging == true)
        {
            Debug.Log("Weapon Collided");
            if (col.transform.tag == "Enemy")
            {
                Debug.Log("Enemy Hit");
                col.gameObject.GetComponent<DEV_EnemyController>().TakeDamage(wepDamage);
            }
            isSwinging = false;
        }
    }

}
