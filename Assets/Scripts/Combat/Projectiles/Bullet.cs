using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    public float speed = 10f;
    public Transform line;
    public GameObject effectPrefab;
    
    private Rigidbody rigid;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if velocity is not 0
        if (rigid.velocity.magnitude > 0f)
        {
            line.rotation = Quaternion.LookRotation(rigid.velocity);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (effectPrefab)
        {
            //get the contact point
            ContactPoint contact = col.contacts[0];
            //spawn the effect and rotate to contact normal
            Instantiate(effectPrefab, contact.point, Quaternion.LookRotation(contact.normal));
        }
        Destroy(gameObject);
    }

    public override void Fire(Vector3 lineOrigin, Vector3 direction)
    {
        rigid.AddForce(direction * speed, ForceMode.Impulse);
        line.position = lineOrigin;
    }
}
