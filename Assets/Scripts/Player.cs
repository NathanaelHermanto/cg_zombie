using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Slider healthBar;
    float health = 100f;

    private void Start()
    {
        healthBar.minValue = 0f;
        healthBar.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead())
        {
            Debug.Log("you're dead lol");
        }

        healthBar.value = health;
    }

    public void isAttacked(float damage)
    {
        health -= damage;
        Debug.Log("player is attacked, health: " + health);
    }

    private bool isDead()
    {
        return health <= 0;
    }
}
