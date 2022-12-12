using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    float health = 100f;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void isAttacked(float damage)
    {
        health -= damage;
        Debug.Log("player is attacked, health: " + health);
    }
}
