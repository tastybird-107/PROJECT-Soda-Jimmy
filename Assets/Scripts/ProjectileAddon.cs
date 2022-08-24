using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAddon : MonoBehaviour
{
    public int damage;

    public int timeToDestroy;

    private Rigidbody rb;

    private bool targetHit;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Invoke(nameof(DestroyProjectile), timeToDestroy);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Make sure only stick to the first target you hit
        if (targetHit)
        {
            return;
        } else
        {
            targetHit = true;

            //check if you hit an enemy
            if(collision.gameObject.GetComponent<EnemyAI>() != null)
            {
                EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();

                enemy.TakeDamage(damage);

                Destroy(gameObject);
            }

            //make sure projectile sticks to surface
            rb.isKinematic = false;

            //make sure projectile moves with target
            transform.SetParent(collision.transform);
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
