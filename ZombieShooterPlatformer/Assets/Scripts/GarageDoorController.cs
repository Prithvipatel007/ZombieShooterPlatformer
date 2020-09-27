using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageDoorController : MonoBehaviour
{
    public bool resetable;
    public GameObject door;
    public GameObject gear;

    public bool startOpen;

    bool firstTrigger = false;
    bool open = true;
    Animator doorAnim;
    Animator gearAnim;

    AudioSource doorAudio;

    // Start is called before the first frame update
    void Start()
    {
        doorAnim = door.GetComponent<Animator>();
        gearAnim = gear.GetComponent<Animator>();
        doorAudio = door.GetComponent<AudioSource>();

        if (!startOpen)
        {
            open = false;
            doorAnim.SetTrigger("doorTrigger");
            doorAudio.Play();
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !firstTrigger)
        {
            if (!resetable)
            {
                firstTrigger = true;
            }
            doorAnim.SetTrigger("doorTrigger");
            open = !open;
            gearAnim.SetTrigger("gearTrigger");
            doorAudio.Play();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
