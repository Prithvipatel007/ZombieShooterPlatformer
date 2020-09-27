using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFireBall : MonoBehaviour
{
    public float damage;        // damage it can do to others
    public float speed;         //how fast the fireball is going to move

    Rigidbody myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponentInParent<Rigidbody>();

        if (transform.rotation.y > 0)
            myRB.AddForce(Vector3.right * speed, ForceMode.Impulse);
        else
            myRB.AddForce(Vector3.right * -speed, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
        {
            myRB.velocity = Vector3.zero;
            Destroy(this.gameObject);
        }
    }

}
