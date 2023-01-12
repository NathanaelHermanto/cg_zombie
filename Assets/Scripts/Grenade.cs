using UnityEngine;

// explode grenade and get items affected tutorial: https://www.youtube.com/watch?v=BYL6JtUdEY0&list=WL&index=13&ab_channel=Brackeys
public class Grenade : MonoBehaviour
{
    public float delay = 2.5f;
    public float blastRadius = 2f;
    public float blastForce = 100f;

    public GameObject explosionEffect;

    float countdown;
    float damagePlayer = 5f;
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
            // add explosion force
            Rigidbody rb = blastedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(blastForce, transform.position, blastRadius);
            }

            // get zombie blasted and trigger animation
            Zombie zombie = blastedObject.GetComponent<Zombie>();
            if (zombie != null)
            {
                // check grenade position to determine zombie fall direction
                Vector3 relativePos = zombie.transform.position - transform.position;
                bool isInFront = Vector3.Dot(transform.forward, relativePos) > 0.0f;  
                
                zombie.Blasted(isInFront);
            }

            Player player = blastedObject.GetComponent<Player>();
            if (player != null)
            {
                player.IsAttacked(damagePlayer);
            }
        }

        // remove grenade
        Destroy(gameObject);
    }
}
