using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public int armor;
    public GameUI gameUI;
    private GunEquipper gunEquipper;
    private Ammo ammo;

    // Start is called before the first frame update
    void Start()
    {
        ammo = GetComponent<Ammo>();
        gunEquipper = GetComponent<GunEquipper>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    //Takes the incoming damage and reduces amount of health
    //based on how much armor the player has remaining
    public void TakeDamage(int amount)
    {
        int healthDamage = amount;

        if(armor > 0)
        {
            int effectiveArmor = armor * 2;
            effectiveArmor -= healthDamage;

            // If armor is still there, don't need to process
            // health damage
            if(effectiveArmor > 0)
            {
                armor = effectiveArmor / 2;
                gameUI.SetArmorText(armor);
                return;
            }
            armor = 0;
            gameUI.SetArmorText(armor);
        }
        health -= healthDamage;
        gameUI.SetHealthText(health);

        if(health <= 0)
        {
            Debug.Log("GameOver");
        }
    }

    //1
    private void pickupHealth()
    {
        health += 50;

        if (health > 200)
        {
            health = 200;
        }
        gameUI.SetPickUpText("Health picked up + 50 health");
        gameUI.SetHealthText(health);
    }

    private void pickupArmor()
    {
        armor += 15;

        gameUI.SetPickUpText("Armor picked up + 15 armor");
        gameUI.SetArmorText(armor);
    }

    //2
    private void pickupAssaultRifleAmmo()
    {
        ammo.AddAmmo(Constants.AssaultRifle, 50);
        
        //Checks to see if gun matches ammo type, then uptates ammo count if successful
        gameUI.SetPickUpText("Assault Rifle ammo picked up + 50 ammo");
        if(gunEquipper.GetActiveWeapon().tag == Constants.AssaultRifle)
        {
            gameUI.SetAmmoText(ammo.GetAmmo(Constants.AssaultRifle));
        }
    }

    private void pickupPistolAmmo()
    {
        ammo.AddAmmo(Constants.Pistol, 20);

        gameUI.SetPickUpText("Pistol ammo picked up + 20 ammo");
        if (gunEquipper.GetActiveWeapon().tag == Constants.Pistol)
        {
            gameUI.SetAmmoText(ammo.GetAmmo(Constants.Pistol));
        }
    }

    private void pickupShotgunAmmo()
    {
        ammo.AddAmmo(Constants.Shotgun, 10);

        gameUI.SetPickUpText("Shotgun ammo picked up + 10 ammo");
        if (gunEquipper.GetActiveWeapon().tag == Constants.Shotgun)
        {
            gameUI.SetAmmoText(ammo.GetAmmo(Constants.Shotgun));
        }
    }
    
    public void PickUpItem(int pickupType)
    {
        switch (pickupType)
        {
            case Constants.PickUpArmor:
                pickupArmor();
                break;
            case Constants.PickUpHealth:
                pickupHealth();
                break;
            case Constants.PickUpAssaultRifleAmmo:
                pickupAssaultRifleAmmo();
                break;
            case Constants.PickUpPistolAmmo:
                pickupPistolAmmo();
                break;
            case Constants.PickUpShotgunAmmo:
                pickupShotgunAmmo();
                break;
            default:
                Debug.LogError("Bad pickup type passed: " + pickupType);
                break;
        }
    }
}
