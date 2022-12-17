using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float blastRadius = 5f;
    public float blastForce = 1000f;

    public GameObject explosionEffect;

    float countdown;
    bool hasExploded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown<=0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        // show effect
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // get blasted object
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach(Collider blastedObject in colliders)
        {
            Rigidbody rb = blastedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log(rb.name);
                rb.AddExplosionForce(blastForce, transform.position, blastRadius);
            }

            ZombieAI zombie = blastedObject.GetComponent<ZombieAI>();
            if (zombie != null)
            {
                Debug.Log(zombie.name);
                zombie.blasted();
            }
        }

        // remove grenade
        Destroy(gameObject);
        Debug.Log("boom");
    }
}
