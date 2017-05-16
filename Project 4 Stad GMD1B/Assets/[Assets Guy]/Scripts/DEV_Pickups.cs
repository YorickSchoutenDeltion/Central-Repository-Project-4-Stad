using UnityEngine;

public class DEV_Pickups : MonoBehaviour {

    public GameObject medKit;
    public GameObject ammoBox;
    public GameObject player;

    public ResourceManager resources;

    public int medKitIncrease;
    public int ammoIncrease;

    private void Awake()
    {
        player = GameObject.Find("Player");
        resources = player.GetComponent<ResourceManager>();
    }

    /* Checks if the player walks against an item that's qualified as pickup
    if so, destroys the object it was colliding with and adds an amount to one
    of the elements of the resource manager.*/

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject == medKit)
        {
            Destroy(c.gameObject);
            resources.HealthIncrease(medKitIncrease);
        }

        if (c.gameObject == ammoBox)
        {
            Destroy(c.gameObject);
            resources.AmmoIncrease(ammoIncrease);
        }
    }
}
