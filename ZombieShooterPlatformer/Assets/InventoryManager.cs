using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject[] Weapons;
    bool[] weaponsAvailable;
    public Image weaponImage;

    int currentWeapon;

    Animator weaponImageAnim;

    // Start is called before the first frame update
    void Start()
    {
        weaponsAvailable = new bool[Weapons.Length];
        for(int i = 0; i < Weapons.Length; i++)
        {
            weaponsAvailable[i] = false;
        }
        currentWeapon = 0;
        weaponsAvailable[currentWeapon] = true;
        /*for (int i = 0; i < Weapons.Length; i++)
        {
            weaponsAvailable[i] = true;
        }*/

        weaponImageAnim = weaponImage.GetComponent<Animator>();

        deactivateWeapons();

        setWeaponActive(currentWeapon);

    }

    // Update is called once per frame
    void Update()
    {
        // toggle weapons
        if (Input.GetKeyDown(KeyCode.Return))
        {
            int i;
            for(i=currentWeapon + 1; i < Weapons.Length; i++)
            {
                if(weaponsAvailable[i] == true)
                {
                    currentWeapon = i;
                    setWeaponActive(currentWeapon);
                    return;
                }
            }
            for(i=0; i< currentWeapon; i++)
            {
                if (weaponsAvailable[i] == true)
                {
                    currentWeapon = i;
                    setWeaponActive(currentWeapon);
                    return;
                }
            }
        }
    }

    public void setWeaponActive(int whichWeapon)
    {
        if (!weaponsAvailable[whichWeapon] == true) return;
        Debug.Log(whichWeapon);
        deactivateWeapons();
        Weapons[whichWeapon].SetActive(true);
        Weapons[whichWeapon].GetComponentInChildren<FireBullet>().InitializeWeapon();
        weaponImageAnim.SetTrigger("WeaponSwitch");
    }


    void deactivateWeapons()
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            Weapons[i].SetActive(false);
        }
    }

    public void activateWeapons(int whichWeapon)
    {
        weaponsAvailable[whichWeapon] = true;
    }

}
