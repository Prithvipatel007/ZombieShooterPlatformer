using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class enemyDamage : MonoBehaviour
{
    public float damage;
    public float damageRate;
    public float pushBackForce;

    float nextDamage;       // time when the next damage is gonna occur
    bool playerInRange = false;     // still within our actual colloider
    GameObject thePlayer;
    playerHealth theplayerHealth;

    // Start is called before the first frame update
    void Start()
    {
        nextDamage = Time.time;
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        theplayerHealth = thePlayer.GetComponent<playerHealth>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerInRange)
        {
            Attack();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRange = false;
        }
    }

    private void Attack()
    {
        if(nextDamage <= Time.time)
        {
            theplayerHealth.addDamage(damage);
            nextDamage = Time.time + damageRate;        // only damage the player when it is suppose to...
            pushBack(thePlayer.transform);
        }
    }

    private void pushBack(Transform pushedObject)
    {
        Vector3 pushDirection = new Vector3(0, (pushedObject.position.y - transform.position.y), 0).normalized;
        pushDirection *= pushBackForce;

        Rigidbody pushedRB = pushedObject.GetComponent<Rigidbody>();
        pushedRB.velocity = Vector3.zero;
        pushedRB.AddForce(pushDirection, ForceMode.Impulse);

    }


}
