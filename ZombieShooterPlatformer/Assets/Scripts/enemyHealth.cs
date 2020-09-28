using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour
{
    public float enemyMaxHealth;
    public float damageModifier;
    public GameObject damageParticles;
    public bool drops;
    public GameObject drop;
    public AudioClip deathSound;
    public bool canBurn;
    public float burnDamage;
    public float burnTime;
    public GameObject burnEffects;

    bool onFire;
    float nextBurn;
    float burnInterval = 1f;
    float endBurn;

    float currentHealth;
    public Slider enemyHealthIndicator;
    AudioSource enemyAS;


    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = enemyMaxHealth;
        enemyHealthIndicator.maxValue = enemyMaxHealth;
        enemyHealthIndicator.value = currentHealth;
        enemyAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(onFire && Time.time > nextBurn)
        {
            addDamage(burnDamage);
            nextBurn += burnInterval;       // taking damage once every second
        }

        if(onFire && Time.time > endBurn)   // if onfire and time to put fire out..
        {
            onFire = false;
            burnEffects.SetActive(false);
        }
    }

    public void addDamage(float damage)
    {
        enemyHealthIndicator.gameObject.SetActive(true);
        damage = damage * damageModifier;
        if(damage <= 0)
        {
            return;
        }
        else
        {
            currentHealth -= damage;
        }

        enemyHealthIndicator.value = currentHealth;
        enemyAS.Play();

        if(currentHealth <= 0)
        {
            makeDead();
        }

    }

    public void makeDead()
    {
        // turn off movement
        // create ragdoll

        AudioSource.PlayClipAtPoint(deathSound, transform.position, 0.15f);

        Destroy(gameObject.transform.root.gameObject);      // destroy the hierarchy
        if (drops)
        {
            Instantiate(drop, transform.position + new Vector3(0,1f,0), Quaternion.Euler(Vector3.zero));
        }
    }

    public void addFire()
    {
        if (!canBurn) return;
        onFire = true;
        burnEffects.SetActive(true);
        endBurn = Time.time + burnTime;
        nextBurn = Time.time + burnInterval;
    }

    public void damageFX(Vector3 point, Vector3 rotation)
    {
        Instantiate(damageParticles, point, Quaternion.Euler(rotation));
    }

}
