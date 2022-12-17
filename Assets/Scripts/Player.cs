using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Slider healthBar;
    float health = 100f;
    bool dead = false;

    private void Start()
    {
        healthBar.minValue = 0f;
        healthBar.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead() && !dead)
        {
            Debug.Log("you're dead lol");
            dead = true;
        }

        healthBar.value = health;
    }

    public void IsAttacked(float damage)
    {
        health -= damage;
        Debug.Log("player is attacked, health: " + health);
    }

    private bool IsDead()
    {
        return health <= 0;
    }
}
