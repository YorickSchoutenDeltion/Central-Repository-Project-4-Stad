using UnityEngine;
using System.Collections.Generic;

public class DEV_Pickups : MonoBehaviour {

    public GameObject medKit;
    public GameObject ammoBox;
    public GameObject player;

    public DEV_ResourceManager resources;

    public List<GameObject> mechanicalPiecesNeeded = new List<GameObject>();
    public List<GameObject> mechanicalPiecesGotten = new List<GameObject>();

    public List<GameObject> weapons = new List<GameObject>();

    public int healthIncrease;
    public int ammoIncrease;

    public bool gottenMechanicalPieces;

    private void Awake() {

        player = GameObject.Find("Player");
        resources = player.GetComponent<DEV_ResourceManager>();
    }

    /* Checks if the player walks against a item thats qualified as pickup
    if so, destroys the object it was colliding with and adds an amount to one
    of the elements of the resource manager*/

    private void OnCollisionEnter(Collision c) {

        if (c.gameObject.tag == "MedKit") {
            Destroy(c.gameObject);
            resources.health += healthIncrease;

            if (c.gameObject.tag == "AmmoBox") {
                Destroy(c.gameObject);
                resources.ammo += ammoIncrease;
            }

            if (c.gameObject.tag == "MechanicalPiece") {
                Destroy(c.gameObject);
                mechanicalPiecesGotten.Add(c.gameObject);
            }

            if(mechanicalPiecesNeeded == mechanicalPiecesGotten) {
                gottenMechanicalPieces = true;
            }

            if (c.gameObject.tag == "Weapon") {
                Destroy(c.gameObject);
                weapons.Add(c.gameObject);
            }
        }
    }
}
