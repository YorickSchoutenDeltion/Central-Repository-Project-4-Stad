using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    static SceneManager instance;

    void Start() {
        if(instance != null) {
            GameObject.Destroy(gameObject);
        } else {
            GameObject.DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    public void LoadNextScene() {
        Application.LoadLevel("MainGameScene");
    }
}
