using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cleaner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerHealth playerDead = other.gameObject.GetComponent<playerHealth>();
            playerDead.makeDead();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
