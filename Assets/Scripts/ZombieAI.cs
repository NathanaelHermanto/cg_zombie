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
        if (zombieMovement.IsArrivedAtTarget())
        {
            attackCooldown -= Time.deltaTime;
            AttackPlayer();
        }

        if (isBlasted)
        {
            Dying();
            isBlasted = false;
        }
    }

    private void AttackPlayer()
    {
        if (attackCooldown <= 0f ) 
        {
            animator.SetTrigger("attack");
            player.IsAttacked(attackDamage);
            attackCooldown = 1f / attackSpeed;
        }
    }

    void Dying()
    {
        Debug.Log("zombie is blasted to oblivion");
        animator.SetTrigger("dead");
        //Destroy(gameObject);
    }

    public void Blasted()
    {
        isBlasted = true;
    }
}
