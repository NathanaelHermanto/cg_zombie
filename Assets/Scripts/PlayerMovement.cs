using UnityEngine;
using UnityEngine.UI;

// player movement & jumping tutorial: https://www.youtube.com/watch?v=_QajrabyTJc&ab_channel=Brackeys
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundChecker;
    public LayerMask groundMask;
    public LayerMask trapGroundMask;
    public Slider staminaBar;
    public float speedWalk = 5f;
    public float speedRun = 10f;
    public float g = -30f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 1f;
    public float stamina = 10f;
    public bool isInTheTrap = false;

    Vector3 velocity;
    bool isGrounded;
    float staminaRecRate = 1f;
    float maxStamina = 10f;

    void Start()
    {
        staminaBar.minValue = 0f;
        staminaBar.maxValue = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        // ground check
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

        if(!isGrounded)
        {
            isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, trapGroundMask);
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // trap check
        isInTheTrap = Physics.CheckSphere(groundChecker.position, groundDistance, trapGroundMask);

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

        staminaBar.value = stamina;
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