using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 10;
    public float attackRange = 10f, attackRate = 1f;
    public bool canAttack = false;

    private float attackTimer = 0f;
    
    void LateUpdate()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= 1f /attackRate)
        {
            canAttack = true;
        }
    }

    public virtual void Attack()
    {
        attackTimer = 0f;
        canAttack = false;
    }
}
