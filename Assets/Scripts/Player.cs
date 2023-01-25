using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Slider healthBar;
    public Transform targetCheat;
    public GameObject winUI;
    public GameObject loseUI;
    public GameObject attackedUI;
    public Transform target;
    public float health = 100f;

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
            if (target == null)
            {
                target = (GameManager.Cheat)? targetCheat: GameManager.GetRandomExit();
            }

            return new Vector2(target.position.x, target.position.z);
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
       
        if (GameManager.Cheat)
        {
            health = 10000f;
            target = targetCheat;
        }
        else
        {
            target = GameManager.GetRandomExit();
        }

        healthBar.minValue = 0f;
        healthBar.maxValue = health;

        Debug.Log("Paused " + PauseMenu.GameIsPaused);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead() && !dead)
        {
            dead = true;
            attackedUI.SetActive(false);
            EndGame(loseUI);
        }

        healthBar.value = health;

        if (ArrivedAtExit())
        {
            attackedUI.SetActive(false);
            EndGame(winUI);
        }
    }

    void OnApplicationQuit()
    {
        GameManager.Cheat = false;
    }

    public void IsAttacked(float damage)
    {
        health -= damage;
        attackedUI.SetActive(true);
        Debug.Log("player is attacked, health: " + health);
        Invoke("TurnOffAttackedPanel", 0.2f);
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

    private void TurnOffAttackedPanel()
    {
        attackedUI.SetActive(false);
    }
}
