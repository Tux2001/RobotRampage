using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Gun
{
    protected override void Update()
    {
        base.Update();

        //shotgun & pistol have semi-auto fire rate
        if (Input.GetMouseButtonDown(0) && (Time.time - lastFireTime) > fireRate)
        {
            lastFireTime = Time.time;
            Fire();
        }
    }
}
