using UnityEngine;

// attack speed tutorial: https://www.youtube.com/watch?v=FhAdkLC-mSg&list=PLPV2KyIb3jR4KLGCCAciWQ5qHudKtYeP7&index=12&ab_channel=Brackeys
public class ZombieAI : MonoBehaviour
{
    public Player player;
    public ZombieMovement zombieMovement;

    Animator animator;
    float attackSpeed = 1f;
    float attackCooldown = 0f;
    float attackDamage = 5f;
    bool isBlasted = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (zombieMovement.isArrivedAtTarget())
        {
            attackCooldown -= Time.deltaTime;
            attackPlayer();
        }

        if (isBlasted)
        {
            dying();
            isBlasted = false;
        }
    }

    private void attackPlayer()
    {
        if (attackCooldown <= 0f ) 
        {
            animator.SetTrigger("attack");
            player.isAttacked(attackDamage);
            attackCooldown = 1f / attackSpeed;
        }
    }

    void dying()
    {
        Debug.Log("zombie is blasted to oblivion");
        animator.SetTrigger("dead");
    }

    public void blasted()
    {
        isBlasted = true;
    }
}
