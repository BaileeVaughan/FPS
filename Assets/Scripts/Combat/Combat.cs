using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Weapon currentWeapon;

    // Update is called once per frame
    void Update()
    {
        //if there is a current weapon
        if (currentWeapon && currentWeapon.canAttack)
        {
            //if fire button is pressed
            if (Input.GetButton("Fire1"))
            {
                //attack
                currentWeapon.Attack();
            }
        }
    }
}
