using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
using UnityEditor;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Slider healthBar;
    public Transform target;
    public GameObject winUI;
    public GameObject loseUI;

    float health = 100f;
    bool dead = false;

    Vector2 posV2
    {
        get
        {
            return new Vector2(transform.position.x, transform.position.z);
        }
    }

    Vector2 destination
    {
        get
        {
            return new Vector2(target.position.x, target.position.z);
        }
    }

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
            EndGame(loseUI);
        }

        healthBar.value = health;

        if (ArrivedAtExit())
        {
            Debug.Log("you win");
            EndGame(winUI);
        }
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

    private bool ArrivedAtExit()
    {
        return Vector2.Distance(posV2, destination) < 5f;
    }

    private void EndGame(GameObject UI)
    {
        Time.timeScale = 0f;
        PauseMenu.GameIsPaused = true;
        UI.SetActive(true);
    }
}
