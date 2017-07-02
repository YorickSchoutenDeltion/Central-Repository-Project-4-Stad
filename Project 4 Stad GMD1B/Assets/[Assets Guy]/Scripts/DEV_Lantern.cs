using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_Lantern : MonoBehaviour {

    public Light light;
    private int lightIntensity;

    void Start() {
        StartCoroutine("LightChange");
    }

    void Update() {
        lightIntensity = Random.Range(0, 100);
    }

    IEnumerator LightChange() {
        yield return new WaitForSeconds(0.05f);
        light.intensity = lightIntensity;
        StartCoroutine("LightChange");
    }
}
