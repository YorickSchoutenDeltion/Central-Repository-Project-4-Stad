using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEV_Mechanics : MonoBehaviour {

    public bool packageOne;
    public bool packageTwo;
    public bool gotAllPackages;

    public int deaths;
    public int lives = 3;
    public int spawnPositionID;

    public GameObject objectPackageOne;
    public GameObject objectPackageTwo;

    public List<Vector3> packageOneSpawnPoints = new List<Vector3>();
    public List<Vector3> packageTwoSpawnPoints = new List<Vector3>();

    public List<Transform> packageLocations;

    public DEV_Score score;
    public GameObject camera;
    public GameObject deliverySpot;

    public string gotPackageOne;
    public string gotPackageTwo;
    public string dontGotPackageOne;
    public string dontGotPackageTwo;

    public Text packageOneUI;
    public Text packageTwoUI;

    void Start() {
        camera = GameObject.Find("Main Camera");
        score = camera.GetComponent<DEV_Score>();
    }

    void Update() {

    if(packageOne == true) {
            packageOneUI.text = gotPackageOne.ToString();
        }

        if (packageTwo == true) {
            packageTwoUI.text = gotPackageTwo.ToString();
        }

        if (packageOne == false) {
            packageOneUI.text = dontGotPackageOne.ToString();
        }

        if (packageTwo == false) {
            packageTwoUI.text = dontGotPackageTwo.ToString();
        }

        packageOneSpawnPoints[0] = new Vector3(packageLocations[0].position.x, 249.6f, packageLocations[0].position.z);
        packageOneSpawnPoints[1] = new Vector3(packageLocations[1].position.x, 249.6f, packageLocations[1].position.z);
        packageOneSpawnPoints[2] = new Vector3(packageLocations[2].position.x, 249.6f, packageLocations[2].position.z);
        packageOneSpawnPoints[3] = new Vector3(packageLocations[3].position.x, 249.6f, packageLocations[3].position.z);
        packageOneSpawnPoints[4] = new Vector3(packageLocations[4].position.x, 249.6f, packageLocations[4].position.z);
        packageTwoSpawnPoints[0] = new Vector3(packageLocations[5].position.x, 249.6f, packageLocations[5].position.z);
        packageTwoSpawnPoints[1] = new Vector3(packageLocations[6].position.x, 249.6f, packageLocations[6].position.z);
        packageTwoSpawnPoints[2] = new Vector3(packageLocations[7].position.x, 249.6f, packageLocations[7].position.z);
        packageTwoSpawnPoints[3] = new Vector3(packageLocations[8].position.x, 249.6f, packageLocations[8].position.z);
        packageTwoSpawnPoints[4] = new Vector3(packageLocations[9].position.x, 249.6f, packageLocations[9].position.z);

        if(deaths == lives) {
            print("Game Over");
            print("Your final score was: " + score.score);
            UnityEditor.EditorApplication.isPlaying = false;
        }

        if (packageOne && packageTwo == true) {
            gotAllPackages = true;
        }

        spawnPositionID = Random.Range(0, packageOneSpawnPoints.Count);
    }

    void OnCollisionEnter(Collision c) {
        if (c.gameObject.transform.tag == "PackageOne") {
            Destroy(c.gameObject);
            packageOne = true;
        }

        if (c.gameObject.transform.tag == "PackageTwo") {
            Destroy(c.gameObject);
            packageTwo = true;
        }

        if (c.gameObject.transform.tag == "Car") {
            deaths += 1;
            transform.position = new Vector3(deliverySpot.transform.position.x, transform.position.y, deliverySpot.transform.position.z);
        }

        if (c.gameObject.transform.tag == "DeliverySpot") {
            if (gotAllPackages == true) {
                packageOne = false;
                packageTwo = false;
                gotAllPackages = false;
                score.score += 100;
                Instantiate(objectPackageOne, packageOneSpawnPoints[spawnPositionID], Quaternion.identity);
                Instantiate(objectPackageTwo, packageTwoSpawnPoints[spawnPositionID], Quaternion.identity);
            }
        }
    }
}
