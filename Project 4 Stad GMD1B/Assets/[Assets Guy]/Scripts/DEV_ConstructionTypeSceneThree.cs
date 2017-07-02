using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_ConstructionTypeSceneThree : MonoBehaviour {

    public bool cutSceneActivated = false;
    public bool hasSpawned;
    public GameObject constructionTypeSpawner;
    public GameObject constructionTypeFinalPos;
    public GameObject constructionType;
    private GameObject cType;
    public float spawnSpeedMultiplier;
    public DEV_ConstructionTypeSceneOne firstScene;
    public GameObject cam;
    public GameObject human;
    public GameObject player;
    public DEV_CameraEffects cameraSettings;
    public DEV_CameraEffects cameraSettingsTwo;
    public DEV_HumanTypeAI humanSettings;
    public DEV_CharacterController controller;

    public void Awake() {
        cameraSettings = cam.GetComponent<DEV_CameraEffects>();
        humanSettings = human.GetComponent<DEV_HumanTypeAI>();
        controller = player.GetComponent<DEV_CharacterController>();
    }

    void Update() {
        if (cutSceneActivated == true && hasSpawned == false) {
            Debug.Log("Attempting to instantiate object");
            cType = (GameObject)Instantiate(constructionType, new Vector3(constructionTypeSpawner.transform.position.x, 0, constructionTypeSpawner.transform.position.z), constructionType.transform.rotation);
            hasSpawned = true;
            firstScene.canWalk = false;
            cameraSettings.isInDanger = true;
            cameraSettingsTwo.isInDanger = true;
            controller.forcedFlashLightFalse = true;

            if (firstScene.canWalk == false) {
                controller.ver = 0;
                controller.hor = 0;
            }
        }
        if (cType != null) {
            Vector3 transformGoal = Vector3.MoveTowards(cType.transform.position, player.transform.position, Time.deltaTime * spawnSpeedMultiplier);
            cType.transform.position = transformGoal;
            if (cType.transform.position == constructionTypeFinalPos.transform.position) {
                Destroy(cType);
                cameraSettings.isInDanger = false;
                cameraSettingsTwo.isInDanger = false;
                humanSettings.canStartWalking = true;
                controller.forcedFlashLightFalse = true;
            }
        }
    }
}
