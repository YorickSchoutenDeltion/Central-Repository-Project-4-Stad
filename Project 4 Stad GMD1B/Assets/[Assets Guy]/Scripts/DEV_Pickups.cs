﻿using UnityEngine;
using System.Collections.Generic;

public class DEV_Pickups : MonoBehaviour {

    public GameObject medKit;
    public GameObject ammoBox;
    public GameObject player;

    public List<GameObject> mechanicalPartsNeeded = new List<GameObject>();
    public List<GameObject> mechanicalPartsGotten = new List<GameObject>();
    public List<GameObject> weapons = new List<GameObject>();

    public List<string> tags = new List<tags>();

    public ResourceManager resources;

    public int healthIncrease;
    public int ammoIncrease;
    public int tagID;

    public bool allPartsScavenged;

    private void Awake() {
        player = GameObject.Find("Player");
        resources = player.GetComponent<ResourceManager>();
    }

    /* Checks if the player walks against a item thats qualified as pickup
    if so, destroys the object it was colliding with and adds an amount to one
    of the elements of the resource manager*/

    public void Update() {
        if(mechanicalPartsGotten == mechanicalPartsNeeded) {
            allPartsScavenged = true;
        }
    }

    /* NOTE: Tags still needs to be added to the string list */

    private void OnCollisionEnter(Collision c) {
        if (c.gameObject == tags[0]) {
            Destroy(c.gameObject);
            resources.health += healthIncrease;

            if (c.gameObject == tags[1]) {
                Destroy(c.gameObject);
                resources.ammo += ammoIncrease;
            }

            if (c.gameObject == tags[2]) {
                mechanicalPartsGotten.Add(c.gameObject);
                Destroy(c.gameObject);
            }

            if (c.gameObject == tags[3]) {
                weapons.Add(c.gameObject);
                Destroy(c.gameObject);
            }
        }
    }
}
