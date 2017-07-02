using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_BookCase : MonoBehaviour {

    public bool moveToGoal;
    public GameObject goal;
	
	void Update () {
		if(moveToGoal == true) {
            transform.position = Vector3.MoveTowards(transform.position, goal.transform.position, Time.deltaTime * 2);
        }
	}
}
