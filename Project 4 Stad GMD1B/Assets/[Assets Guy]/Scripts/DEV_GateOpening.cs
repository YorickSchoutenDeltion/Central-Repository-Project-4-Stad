using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_GateOpening : MonoBehaviour {

    public bool gateOpening;

    public GameObject gate;
    public GameObject gateGoal;

    public Color color;
    public Light light;

    public void Awake() {
        light = this.GetComponent<Light>();
    }

	void Update () {
        if (gateOpening == true) {
            gate.transform.position = Vector3.MoveTowards(gate.transform.position, gateGoal.transform.position, Time.deltaTime * 2.5f);
            light.color = color;
        }
    }
}
