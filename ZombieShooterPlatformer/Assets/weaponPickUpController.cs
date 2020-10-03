using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPickUpController : MonoBehaviour
{

    public int whichWeapon;
    public AudioClip pickUpClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<InventoryManager>().activateWeapons(whichWeapon);
            Destroy(transform.root.gameObject);
            AudioSource.PlayClipAtPoint(pickUpClip, transform.position);

        }
    }

}
