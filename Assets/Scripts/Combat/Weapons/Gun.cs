using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public int maxReserve = 300, maxClip = 30;
    public float spread = 2f, recoil = 1f;
    public Transform shotOrigin;
    public GameObject projectilePrefab;

    private int currentReserve = 0, currentClip = 0;

    void Start()
    {
        Reload();
    }

    public void Reload()
    {
        //if there is ammo in reserve
        if (currentReserve > 0)
        {
            //if reserve is greater than max clip
            if (currentReserve > maxClip)
            {
                //remove difference from current reserve
                int difference = maxClip - currentClip;
                currentReserve -= difference;
                //replenish entire clip with max clip
                currentClip = maxClip;
            }
            //if clip < max clip
            if (currentReserve < maxClip)
            {
                //set entire clip to reserve
                currentClip += currentReserve;
                currentReserve -= currentReserve;
            }
        }
    }

    public override void Attack()
    {
        //reduce the clip
        currentClip--;
        //get origin + direction for bullet
        Camera attachedCamera = Camera.main;
        Transform camTransform = attachedCamera.transform;
        Vector3 lineOrigin = shotOrigin.position;
        Vector3 direction = camTransform.forward;
        //spawn bullet
        GameObject clone = Instantiate(projectilePrefab, camTransform.position, camTransform.rotation);
        Projectile projectile = clone.GetComponent<Projectile>();
        projectile.Fire(lineOrigin, direction);

        //reset ability to attack
        base.Attack();
    }
}
