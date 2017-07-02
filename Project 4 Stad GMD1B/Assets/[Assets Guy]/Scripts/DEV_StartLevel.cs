using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEV_StartLevel : MonoBehaviour {

    public float imageAlpha;

    public Image blackScreenReference;

    public Button button;

    public GameObject imageBlackScreen;

    public AudioSource ambientMusic;
    public SceneManager scene;

    public bool transitioningNewScene = false;
    public bool startingCurrentScene = true;

    public void Awake() {
        button = button.GetComponent<Button>();
    }

    public void StartGame() {
        transitioningNewScene = true;
        startingCurrentScene = false;
        print("Loading...");
        StartCoroutine("LoadingNewScene");
    }

    IEnumerator LoadingNewScene() {
        yield return new WaitForSeconds(15);
        scene.LoadNextScene();
    }

    IEnumerator LoadingStart() {
        yield return new WaitForSeconds(10);
        startingCurrentScene = false;
    }

    public void Start() {
        StartCoroutine("LoadingStart");
    }

    void Update () {
        if (startingCurrentScene == false && transitioningNewScene == false) { 
            button.interactable = true;
        } else {
            button.interactable = false;
        }

        blackScreenReference = imageBlackScreen.GetComponent<Image>();
        Color tempColor = blackScreenReference.color;
        tempColor.a = imageAlpha;
        blackScreenReference.color = tempColor;

        if (startingCurrentScene == true) {
            ambientMusic.volume = Mathf.Lerp(ambientMusic.volume, 1, Time.deltaTime / 2);
            imageAlpha = Mathf.Lerp(imageAlpha, 0, Time.deltaTime / 2);
        }

        if (transitioningNewScene == true) {
            ambientMusic.volume = Mathf.Lerp(ambientMusic.volume, 0, Time.deltaTime / 2);
            imageAlpha = Mathf.Lerp(imageAlpha, 1, Time.deltaTime / 2);
        }
	}
}
