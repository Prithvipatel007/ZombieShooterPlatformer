using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    ///  movement Variables
    /// </summary>
    private float runSpeed = 8f;
    Rigidbody myRB;
    Animator myAnim;
    bool facingRight;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        facingRight = true;
    }

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        myAnim.SetFloat("speed", Mathf.Abs(move));

        myRB.velocity = new Vector3(move * runSpeed, myRB.velocity.y, 0);

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

}
