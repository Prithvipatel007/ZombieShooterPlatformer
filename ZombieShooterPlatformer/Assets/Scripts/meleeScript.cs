using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeScript : MonoBehaviour
{
    public float damage;
    public float knockBack;
    public float knockBackRadius;
    public float meleeRate;

    float nextMelee;

    int shootableMask;

    Animator myAnim;
    PlayerController myPC;

    // Start is called before the first frame update
    void Start()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        myAnim = transform.root.GetComponent<Animator>();
        myPC = transform.root.GetComponent<PlayerController>();
        nextMelee = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float melee = Input.GetAxis("Fire2");

        if(melee > 0f && nextMelee < Time.time && !myPC.GetRunning())
        {
            myAnim.SetTrigger("gunMeele");
            nextMelee = Time.time + meleeRate;

            // do damage
            Collider[] attacked = Physics.OverlapSphere(transform.position, knockBackRadius, shootableMask);

            int i = 0;
            while( i < attacked.Length)
            {
                if(attacked[i].tag == "Enemy")
                {
                    enemyHealth doDamage = attacked[i].GetComponent<enemyHealth>();
                    doDamage.addDamage(damage*5);
                    doDamage.damageFX(transform.position, transform.localEulerAngles);
                }
                i++;
            }

        }
    }
}
