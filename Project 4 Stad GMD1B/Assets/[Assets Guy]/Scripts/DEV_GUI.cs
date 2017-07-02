using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEV_GUI : MonoBehaviour {

    [Range(0, 1f)]
    public float imageAlpha;

    public Image image;

    public GameObject imageObject;
    public GameObject constrSpawner;

    public DEV_ConstructionTypeSceneOne constrScene;

    public bool lerpStartVisual = true;

    public AudioSource rain;

    public void Start() {
        rain.volume = 0;
        constrScene = constrSpawner.GetComponent<DEV_ConstructionTypeSceneOne>();
        constrScene.canWalk = false;
        StartCoroutine("StartGame");
    }

    void Update() {

        image = imageObject.GetComponent<Image>();
        Color tempColor = image.color;
        tempColor.a = imageAlpha;
        image.color = tempColor;

        if(lerpStartVisual == true) {
            rain.volume = Mathf.Lerp(rain.volume, 1, Time.deltaTime / 2);
            imageAlpha = Mathf.Lerp(imageAlpha, 0, Time.deltaTime / 2);
        }
    }

    IEnumerator StartGame() {
        yield return new WaitForSeconds(5);
        lerpStartVisual = false;
        imageAlpha = 0;
        rain.volume = 1;
        constrScene.canWalk = true;
    }
}

