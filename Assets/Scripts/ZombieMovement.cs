using UnityEngine;

// zombie movement tutorial: https://www.youtube.com/watch?v=AGWZ4pM48Sk&ab_channel=b3agz
public class ZombieMovement : MonoBehaviour
{
    public bool isRunning = false;
    public float transitionSpeed = 10f;
    public Transform target;
    public bool hasCataract = false;
    public Zombie zombie;

    Animator animator;
    float currentSpeed = 0f;
    float targetSpeed = 0f;
    bool arrivedAtTarget = false;
    float sightDistance = 10f;
    Vector3 targetPosition;
    Quaternion lastRotation;

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
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!zombie.IsDead())
        {
            targetPosition = new Vector3(destination.x, transform.position.y, destination.y);
            lastRotation = transform.rotation;
            transform.LookAt(targetPosition);
        }
        else
        {
            transform.LookAt(targetPosition);
            transform.rotation = lastRotation;
        }

        if (hasCataract)
        {
            if (CanSeePlayer()) 
                Move();
        } else
        {
            Move();
        }

        currentSpeed = Mathf.SmoothStep(currentSpeed, targetSpeed, Time.deltaTime * transitionSpeed);
        animator.SetFloat("speed", currentSpeed);

    }

    private bool ShouldMove()
    {
        return Vector2.Distance(posV2, destination) > 1.8f;
    }

    private float GetSpeed()
    {
        return (isRunning) ? 2f : 1f;
    }

    private void Move()
    {
        if (ShouldMove())
        {
            targetSpeed = GetSpeed();
            arrivedAtTarget = false;
        }
        else
        {
            targetSpeed = 0f;
            arrivedAtTarget = true;
        }
    }

    private bool CanSeePlayer()
    {
        return Vector2.Distance(posV2, destination) < sightDistance;
    }

    public bool IsArrivedAtTarget()
    {
        return arrivedAtTarget;
    }
}
