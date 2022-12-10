using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// zombie movement tutorial: https://www.youtube.com/watch?v=AGWZ4pM48Sk&ab_channel=b3agz
public class ZombieAI : MonoBehaviour
{
    public bool isRunning = false;
    public float transitionSpeed = 10f;
    public Player player;
    public Transform target;

    CharacterController controller;
    Animator animator;
    float currentSpeed = 0f;
    float targetSpeed = 0f;
    float attackSpeed = 1f;
    float attackCooldown = 0f;

    Vector2 destination
    {
        get
        {
            return new Vector2(target.position.x, target.position.z);
        }
    }


    Vector2 posV2
    {
        get
        {
            return new Vector2(transform.position.x, transform.position.z);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(destination.x, transform.position.y, destination.y));

        if (shouldMove())
        {
            targetSpeed = getSpeed();
        } else
        {
            targetSpeed = 0f;
            attackCooldown -= Time.deltaTime;
            attackPlayer();
        }

        currentSpeed = Mathf.SmoothStep(currentSpeed, targetSpeed, Time.deltaTime * transitionSpeed);
        animator.SetFloat("speed", currentSpeed);    
        
    }

    private bool shouldMove()
    {
        return Vector2.Distance(posV2, destination) > 1.8f;
    }

    private float getSpeed()
    {
        return (isRunning) ? 2f : 1f;
    }

    private void attackPlayer()
    {
        if (attackCooldown <= 0f ) 
        {
            animator.SetTrigger("attack");
            player.isAttacked();
            attackCooldown = 1f / attackSpeed;
        }
    }
}
