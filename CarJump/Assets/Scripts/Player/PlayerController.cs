using System.Collections;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlayerInputManager controller;

    [Header("Car Seting")]
    public float carSpeed = 10f;
    public float bostSpeed;
    public float turnFactor = 3.5f;
    public float driftFactor = 0.95f;
    public float steeringInput = 0f;
    public bool isRunCar;
    public bool isRotateCar;
    public bool isBostCar;
    public float lateralSpeed;
    public float rotationAngle;

    Vector3 baseScale;

    [Header("Jump")]
    public float jumpForce; // Zıplama kuvveti
    public float scaleIncrement; // Scale artış miktarı
    public float scaleDecrement; // Scale azalış miktarı
    public float maxScale = 2f; // Maksimum scale değeri
    public float jumpDelay = 0.3f;
    public bool isJumping = false;
    [SerializeField]float jumpTimer = -1;

    public bool everythingİsOkey;

    Vector2 velocityChange;

    void OnEnable()
    {
        PlayerInputManager.bostCarHandler += BostCar;
    }
    void OnDisable()
    {
        PlayerInputManager.bostCarHandler -= BostCar;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
    }

    private void Update()
    {
        InputValue();
    }

    void FixedUpdate()
    {
        if(everythingİsOkey)
        {
            KillOrthogonalVelocity();

            if (controller.jumpCar && !isJumping && isRunCar)
            {
                if(jumpTimer <= 0) 
                {
                    Jump();
                    jumpTimer = jumpDelay;
                }
                else if (jumpTimer > 0)
                {
                    jumpTimer -= Time.fixedDeltaTime;
                }
            }

            if (isRunCar)
            {
                EngineForce();
            }
            if (isRotateCar)
            {
                Steering();
            }
        }

    }
    void KillOrthogonalVelocity()
    {
        Vector2 forwarVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

        rb.velocity = forwarVelocity + rightVelocity * driftFactor;

    }

    void BostCar()
    {
        if(isBostCar)
        {
            Vector2 desiredVelocity = transform.up * bostSpeed;
            Vector2 currentVelocity = rb.velocity;
            velocityChange = desiredVelocity - currentVelocity;

            rb.AddForce(velocityChange, ForceMode2D.Impulse);

        }
    }
    void EngineForce()
    {
        Vector2 desiredVelocity = transform.up * carSpeed;
        Vector2 currentVelocity = rb.velocity;
        velocityChange = desiredVelocity - currentVelocity;

        rb.AddForce(velocityChange, ForceMode2D.Force);

        if (rb.velocity.magnitude > carSpeed)
        {
            rb.velocity = rb.velocity.normalized * carSpeed;
        }
    }
    void Steering()
    {

        float swapTurnFactor = turnFactor;
        if (isJumping)
            swapTurnFactor = 1f;


        rotationAngle -= steeringInput * turnFactor;
        float turnSpeed = Mathf.Abs(steeringInput) * turnFactor;
        lateralSpeed = rb.velocity.magnitude * Mathf.Deg2Rad * turnSpeed;
        rb.MoveRotation(rotationAngle);
    }

    void Jump()
    {

        rb.AddForce(velocityChange * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
            
            StartCoroutine(ScalingCoroutine());
    }
    IEnumerator ScalingCoroutine()
    {
        float currentScale = transform.localScale.x;

        while (currentScale < maxScale)
        {
            currentScale += scaleIncrement * Time.deltaTime;
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            yield return null;
        }

        while (currentScale > baseScale.x)
        {
            currentScale -= scaleDecrement * Time.deltaTime;
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            yield return null;
        }

        transform.localScale = baseScale;
        
        isJumping = false;
    }
    void InputValue()
    {
        steeringInput = controller.move.x;
    }


}

