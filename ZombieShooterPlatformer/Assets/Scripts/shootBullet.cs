using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootBullet : MonoBehaviour
{
    public float range = 10f;
    public float damage = 5f;

    // a projectile or a ray of bullet
    Ray shootRay;
    // returns the information about ray hitting any object
    RaycastHit shootHit;
    // mask on which the bullet can shoot
    int shootableMask;
    // line to make it look like a bullet
    LineRenderer gunLine;


    // Start is called before the first frame update
    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        gunLine.SetPosition(0, transform.position);

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            // hit an enemy goes here

            // draws line from the gun position to whatever it hits
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
