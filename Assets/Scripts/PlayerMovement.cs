using UnityEngine;

// player movement & jumping tutorial: https://www.youtube.com/watch?v=_QajrabyTJc&ab_channel=Brackeys
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundChecker;
    public LayerMask groundMask;
    public float speedWalk = 6f;
    public float speedRun = 10f;
    public float g = -20f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;
    public float stamina = 5f;

    Vector3 velocity;
    bool isGrounded;
    float staminaRecRate = 1f;
    float maxStamina = 5f;

    // Update is called once per frame
    void Update()
    {
        // ground check
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // run
        if (IsRunning())
        {
            controller.Move(move * speedRun * Time.deltaTime);
            stamina = stamina - staminaRecRate * Time.deltaTime;
        }
        else
        {
            controller.Move(move * speedWalk * Time.deltaTime);
            RecoverStamina();
        }

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * g);
        }

        velocity.y += g * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    bool IsRunning()
    {
        return Input.GetKey(KeyCode.LeftShift) && stamina > 0f;
    }

    void RecoverStamina()
    {
        if (stamina + staminaRecRate * Time.deltaTime < maxStamina)
        {
            stamina = stamina + staminaRecRate * Time.deltaTime;
        }
    }
}