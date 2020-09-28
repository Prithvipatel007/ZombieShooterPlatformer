using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieController : MonoBehaviour
{
    public GameObject flipModel;    // flip a part or body of zombie, not the whole zombie with slider and all
    public GameObject ragDollDead;

    // audio options
    public AudioClip[] idleSounds;
    public float idleSoundTime;     // how "often" the zombie can make idle sounds
    AudioSource enemyMovementAS;
    float nextIdleSound = 0f;      // when next idle sound occur

    public float detectionTime;     // player remains within the area of danger before it is actually detected
    float startRun;
    bool firstDetection;            // when gets detected, alwazs remain detected

    // movement options
    public float runSpeed;
    public float walkSpeed;
    public bool facingRight = true;

    float moveSpeed;
    bool running;

    Rigidbody myRB;
    Animator myAnim;
    Transform detectedPlayer;

    bool Detected;                  // once it'S true, it always remains true unless the player or zombie is dead

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponentInParent<Rigidbody>();
        myAnim = GetComponentInParent<Animator>();
        enemyMovementAS = GetComponent<AudioSource>();

        running = false;
        Detected = false;
        firstDetection = false;
        moveSpeed = walkSpeed;

        if(Random.Range(0, 10) > 5)     // flipping the zombie in random direction at the beginning
        {
            Flip();
        }

    }

    void FixedUpdate()
    {
        if (Detected)
        {
            facingCorrection();

            if (!firstDetection)
            {
                startRun = Time.time + detectionTime;
                firstDetection = true;
            }
        }
        if (Detected && !facingRight)
            myRB.velocity = new Vector3((moveSpeed * -1), myRB.velocity.y, 0);
        else if (Detected && facingRight)
            myRB.velocity = new Vector3(moveSpeed, myRB.velocity.y, 0);

        if(!running && Detected)
        {
            if(startRun < Time.time)
            {
                moveSpeed = runSpeed;
                myAnim.SetTrigger("run");
                running = true;
            }
        }

        // idle or walking sounds
        if (!running)
        {
            if(Random.Range(0,10) > 5 && nextIdleSound < Time.time)
            {
                AudioClip tempClip = idleSounds[Random.Range(0, idleSounds.Length)];
                enemyMovementAS.clip = tempClip;
                enemyMovementAS.Play();
                nextIdleSound = idleSoundTime + Time.time;
            }
        }


    }

    void facingCorrection()
    {
        if (detectedPlayer.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if (detectedPlayer.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = flipModel.transform.localScale;
        theScale.z *= -1f;
        flipModel.transform.localScale = theScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !Detected)
        {
            Detected = true;
            detectedPlayer = other.transform;
            myAnim.SetBool("detected", Detected);

            facingCorrection();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            firstDetection = false;
            if (running)
            {
                myAnim.SetTrigger("run");
                moveSpeed = walkSpeed;
                running = false;
            }
        }
    }

    public void RagdollDeath()
    {
        GameObject ragDoll = Instantiate(ragDollDead, transform.root.position, Quaternion.identity) as GameObject;

        Transform ragDollMaster = ragDoll.transform.Find("master");
        Transform zombieMaster = transform.root.Find("master");

        // to get the bone orientation like we want

        bool wasFacingRight = true;
        if (!facingRight)
        {
            wasFacingRight = false;
            Flip();
        }

        // gives all the transforms of all the joints in the ragdoll
        Transform[] ragdollJoints = ragDollMaster.GetComponentsInChildren<Transform>();
        Transform[] currentJoints = zombieMaster.GetComponentsInChildren<Transform>();

        for (int i = 0; i < ragdollJoints.Length; i++)
        {
            for(int q = 0; q < currentJoints.Length; q++)
            {
                if(currentJoints[q].name.CompareTo(ragdollJoints[i].name) == 0)
                {
                    ragdollJoints[i].position = currentJoints[q].position;
                    ragdollJoints[i].rotation = currentJoints[q].rotation;
                    break;
                }
            }
        }

        if (wasFacingRight)
        {
            Vector3 rotVector = new Vector3(0, 0, 0);
            ragDoll.transform.rotation = Quaternion.Euler(rotVector);
        }
        else
        {
            Vector3 rotVector = new Vector3(0, 90, 0);
            ragDoll.transform.rotation = Quaternion.Euler(rotVector);
        }

        Transform zombieMesh = transform.root.transform.Find("zombieSoldier");
        Transform ragDollMesh = ragDoll.transform.Find("zombieSoldier");

        ragDollMesh.GetComponent<Renderer>().material = zombieMesh.GetComponent<Renderer>().material;




    }

}
