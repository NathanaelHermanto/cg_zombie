using UnityEngine;
using UnityEngine.UI;

// throw grenade tutorial: https://www.youtube.com/watch?v=BYL6JtUdEY0&list=WL&index=13&ab_channel=Brackeys
public class GrenadeThrower : MonoBehaviour
{
    public float throwForce = 20f;
    public GameObject grenadePrefab;
    public int ammo = 5;
    public Text ammoDisplay;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Cheat)
        {
            ammo = 100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ammoDisplay.text = ammo.ToString();

        if (Input.GetMouseButtonDown(0) && ammo>0f && !PauseMenu.GameIsPaused)
        {
            ThrowGrenade();
            decreaseAmmo();
        }
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }

    void decreaseAmmo()
    {
        ammo--;
    }
}
