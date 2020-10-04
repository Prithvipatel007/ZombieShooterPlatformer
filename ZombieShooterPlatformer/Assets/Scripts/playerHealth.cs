using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public float fullHealth = 100;
    float currentHealth;

    public GameObject playerDeathFX;

    AudioSource playerAS;
    public restartGame gameController;

    public Text endGameText;

    // HUD
    public Slider playerHealthSlider;
    public Image damageScreen;
    Color flashColor = new Color(255f, 255f, 255f, 1);
    float flashSpeed = 5f;
    bool damaged = false;       // indicate if the player is damaged or not.

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = fullHealth;
        playerHealthSlider.maxValue = fullHealth;
        playerHealthSlider.value = currentHealth;
        playerAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // are we hurt?
        if (damaged)
        {
            damageScreen.color = flashColor;
        }
        else
        {
            damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void addDamage(float damage)
    {
        currentHealth -= damage;
        playerHealthSlider.value = currentHealth;
        damaged = true;
        playerAS.Play();

        if(currentHealth <= 0)
        {
            makeDead();
        }
    }

    public void addHealth(float healthAmount)
    {
        currentHealth += healthAmount;

        if (currentHealth >= fullHealth)
            currentHealth = fullHealth;

        playerHealthSlider.value = currentHealth;
    }


    public void makeDead()
    {
        
        gameObject.SetActive(false);
        Instantiate(playerDeathFX, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        damageScreen.color = flashColor;
        Animator endGameAnim = endGameText.GetComponent<Animator>();
        endGameAnim.SetTrigger("endGame");

        Destroy(gameObject, 5f);

        gameController.restartTheGame();
    }
}
