using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] GameUI gameUI;
    [SerializeField] private int pistolAmmo = 20;
    [SerializeField] private int shotgunAmmo = 10;
    [SerializeField] private int assaultRifleAmmo = 50;
    [SerializeField] private int laserGunAmmo = 5;
    
    public Dictionary<string, int> tagToAmmo;

    //Initializes the dictionary
    void Awake()
    {
        tagToAmmo = new Dictionary<string, int>
        {
             { Constants.Pistol , pistolAmmo },
             { Constants.Shotgun , shotgunAmmo },
             { Constants.AssaultRifle , assaultRifleAmmo },
             {Constants.LaserGun, laserGunAmmo },
        };
    }

    public void AddAmmo(string tag, int ammo)
    {
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("Unrecognized gun type passed: " + tag);
        }
        tagToAmmo[tag] += ammo;
    }

    //returns true if gun has ammo
    public bool HasAmmo(string tag)
    {
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("Unrecognized gun type passed: " + tag);
        }
        return tagToAmmo[tag] > 0;
    }

    public int GetAmmo(string tag)
    {
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("Unrecognized gun type passed:" + tag);
        }
        return tagToAmmo[tag];
    }

    public void ConsumeAmmo(string tag)
    {
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("Unrecognized gun type passed:" + tag);
        }
        tagToAmmo[tag]--;

        gameUI.SetAmmoText(tagToAmmo[tag]);
    }
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
