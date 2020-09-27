using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBullet : MonoBehaviour
{
    public float timeBetweenBullets = 0.15f;
    public GameObject projectile;       // the bullet prefab

    // bullet info
    public int maxRounds = 100;
    public int startingRounds = 100;
    int remainingRounds;
    public Slider playerAmmoSlider;

    // audio info
    AudioSource gunMuzzleAS;
    public AudioClip shootSound;
    public AudioClip reloadSound;


    float nextBullet;

    // Start is called before the first frame update
    void Awake()
    {
        nextBullet = 0f;
        remainingRounds = startingRounds;
        gunMuzzleAS = GetComponent<AudioSource>();
        playerAmmoSlider.maxValue = maxRounds;
        playerAmmoSlider.value = remainingRounds;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController myPlayer = transform.root.GetComponent<PlayerController>();

        if (Input.GetAxisRaw("Fire1") > 0 && nextBullet  < Time.time && remainingRounds > 0)
        {
            nextBullet = Time.time + timeBetweenBullets;
            Vector3 rot;

            if(myPlayer.GetFacing() == -1f)
            {
                rot = new Vector3(0, -90, 0);
            }
            else
            {
                rot = new Vector3(0, 90, 0);
            }

            PlayASound(shootSound);

            Instantiate(projectile, transform.position, Quaternion.Euler(rot));
            remainingRounds--;
            playerAmmoSlider.value = remainingRounds;

        }
    }

    public void Reload()
    {
        remainingRounds = maxRounds;
        playerAmmoSlider.value = remainingRounds;
        PlayASound(reloadSound);
    }

    void PlayASound(AudioClip playSound)
    {
        gunMuzzleAS.clip = playSound;
        gunMuzzleAS.Play();
    }


}
