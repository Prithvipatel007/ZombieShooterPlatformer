using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionController : MonoBehaviour
{
    public Light explosionLight;
    public float power;
    public float radius;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.gameObject.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 3.0f, ForceMode.Impulse);
            }

            if(hit.gameObject.CompareTag("Player"))
            {
                playerHealth playerHealth = hit.gameObject.GetComponent<playerHealth>();
                playerHealth.addDamage(damage);
            }
            else if(hit.gameObject.CompareTag("Enemy"))
            {
                enemyHealth enemyHealth = hit.gameObject.GetComponent<enemyHealth>();
                enemyHealth.addDamage(damage);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        explosionLight.intensity = Mathf.Lerp(explosionLight.intensity, 0f, 5f * Time.time);
    }

}
