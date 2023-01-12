using UnityEngine;

// attack speed tutorial: https://www.youtube.com/watch?v=FhAdkLC-mSg&list=PLPV2KyIb3jR4KLGCCAciWQ5qHudKtYeP7&index=12&ab_channel=Brackeys
public class Zombie : MonoBehaviour
{
    public Player player;
    public ZombieMovement zombieMovement;

    Animator animator;
    CharacterController cc;
    float attackSpeed = 1f;
    float attackCooldown = 0f;
    float attackDamage = 10f;
    bool isBlasted = false;
    bool dead = false;
    bool fallback;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        Physics.IgnoreLayerCollision(0, 7, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (zombieMovement.IsArrivedAtTarget() && dead==false)
        {
            attackCooldown -= Time.deltaTime;
            AttackPlayer();
        }

        if (isBlasted)
        {
            Dying();
            isBlasted = false;
            dead = true;
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
        if (fallback)
        {
            animator.SetTrigger("deadFallForward");
        }
        else
        {
            animator.SetTrigger("deadFallBack");
        }
       
        cc.GetComponent<Rigidbody>().isKinematic = true;
        cc.GetComponent<CapsuleCollider>().gameObject.layer = 7;
        Physics.IgnoreLayerCollision(0, 7);
    }

    public void Blasted(bool back)
    {
        isBlasted = true;
        fallback = back;
    }

    public bool IsDead()
    {
        return dead;
    }
}
