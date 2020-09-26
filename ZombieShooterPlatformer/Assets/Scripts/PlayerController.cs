using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    ///  movement Variables
    /// </summary>
    private float runSpeed = 8f;
    private float walkSpeed = 1.7f;
    Rigidbody myRB;
    Animator myAnim;
    bool facingRight;
    bool running;

    /// <summary>
    /// variables for jumping
    /// </summary>
    bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        facingRight = true;
    }

    private void FixedUpdate()
    {
        running = false;

        // code for jump
        if(grounded && Input.GetAxis("Jump") > 0)
        {
            grounded = false;
            myAnim.SetBool("grounded", grounded);
            myRB.AddForce(new Vector3(0, jumpHeight, 0));
        }

        // this is specifically used for falling and landing: it requires the ground check position, it's radius and the intercepting layermask to which we want to react, for example , ground
        // returns any of the colloiders which are overlapped with the sphere
        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);     

        //if groundcollision's length > 0, means something is colloiding with it
        if(groundCollisions.Length > 0)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        myAnim.SetBool("grounded", grounded);

        float move = Input.GetAxis("Horizontal");
        myAnim.SetFloat("speed", Mathf.Abs(move));

        float sneaking = Input.GetAxisRaw("Fire3");     // Fire3 is associated by default with the Shift button
        myAnim.SetFloat("sneaking", sneaking);

        float firing = Input.GetAxisRaw("Fire1");
        myAnim.SetFloat("shooting", firing);

        if ((sneaking > 0.1 || firing > 0) && grounded)
        {
            // walking
            myRB.velocity = new Vector3(move * walkSpeed, myRB.velocity.y, 0);
        }
        else
        {
            // running
            myRB.velocity = new Vector3(move * runSpeed, myRB.velocity.y, 0);
            
            if(Mathf.Abs(move) > 0)
                running = true;
        }
        

        if(move > 0 && !facingRight)
        {
            Flip();
        }
        else if(move < 0 && facingRight)
        {
            Flip();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
    }

    public float GetFacing()
    {
        if (facingRight) return 1f;
        else return -1f;
    }

    public bool GetRunning()
    {
        return running;
    }

}
