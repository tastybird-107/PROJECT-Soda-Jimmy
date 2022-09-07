using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAddonEnemy : MonoBehaviour
{
    public int damage;

    public float timeToDestroy;

    private Rigidbody rb;

    private bool targetHit;

    [Header("Explosive Projectile")]
    public bool isExplosive;
    public float explosionRadius;
    public float explosionForce;
    public int explosionDamage;
    public GameObject explosionEffect;

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
            if(collision.gameObject.GetComponent<PlayerMovement>() != null)
            {
                PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

                player.TakeDamage(damage);

                Destroy(gameObject);
            }

            // explode projectile if it's explosive
            if (isExplosive)
            {
                Explode();
                return;
            }

            //make sure projectile DOESNT sticks to surface
            rb.isKinematic = false;

            //make sure projectile moves with target (PROLLY UNNECESSARY NOW)
            //transform.SetParent(collision.transform);
        }
    }

    private void Explode()
    {
        // spawn explosion effect (if assigned)
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // find all the objects that are inside the explosion range
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, explosionRadius);

        // loop through all of the found objects and apply damage and explosion force
        for (int i = 0; i < objectsInRange.Length; i++)
        {
            if (objectsInRange[i].gameObject == gameObject)
            {
                // don't break or return please, thanks
            }
            else
            {
                // check if object is enemy, if so deal explosionDamage
                if (objectsInRange[i].GetComponent<PlayerMovement>() != null)
                    objectsInRange[i].GetComponent<PlayerMovement>().TakeDamage(explosionDamage);

                // check if object has a rigidbody
                if (objectsInRange[i].GetComponent<Rigidbody>() != null)
                {
                    // custom explosionForce
                    Vector3 objectPos = objectsInRange[i].transform.position;

                    // calculate force direction
                    Vector3 forceDirection = (objectPos - transform.position).normalized;

                    // apply force to object in range
                    objectsInRange[i].GetComponent<Rigidbody>().AddForceAtPosition(forceDirection * explosionForce + Vector3.up * explosionForce, transform.position + new Vector3(0, -0.5f, 0), ForceMode.Impulse);

                    Debug.Log("Kabooom " + objectsInRange[i].name);
                }
            }
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
