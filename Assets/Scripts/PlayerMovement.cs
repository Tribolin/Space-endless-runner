using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public Transform Feed;
    public Transform RightArm;
    public Transform LeftArm;
    public Camera Camera;

    public float Speed = 12f;
    public float Gravity = -9.81f;
    public float JumpHeight = 3f;

    //Power Ups
    public int MaxELife = 2;
    public int ExtraLifes = 0;
    public bool SlowMo = false;

    public float WallDistance = 0.1f;
    public float GroundDistance = 0.1f;
    public LayerMask GroundMask;

    bool onRightWall;
    bool onLeftWall;
    bool isGrounded;
    bool isCrouching = false;
    Vector3 velocity;
    Management manager;

    const string KEY_CROUCH = "s";
    const string KEY_JUMP = "Jump";

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Management>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(Feed.position, GroundDistance, GroundMask);
        onLeftWall = Physics.CheckSphere(LeftArm.position, WallDistance, GroundMask);
        onRightWall = Physics.CheckSphere(RightArm.position, WallDistance, GroundMask);

        // Push player to floor
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        else if (onRightWall || onLeftWall)
        {
            velocity.y = -1.5f;
        }

        if (Input.GetKeyDown(KEY_CROUCH) && isGrounded)
        {
            isCrouching = true;
            controller.height = 1;
            gameObject.transform.position += new Vector3(0, -1, 0);
        }
        if (Input.GetKeyUp(KEY_CROUCH) || isCrouching && !isGrounded)
        {
            isCrouching = false;
            controller.height = 2;
            gameObject.transform.position += new Vector3(0, 1, 0);
        }
        if (Input.GetButtonDown(KEY_JUMP) && isGrounded)
        {
            velocity.y += Mathf.Sqrt(JumpHeight * -2f * Gravity);
        }

        // Adjust horizontal movement
        Vector3 move = transform.right * Input.GetAxis("Horizontal");
        controller.Move(move * Speed * Time.deltaTime);

        // Adjust vertical movement
        velocity.y += Gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    //Collisions
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");

        //Module Collision -> Spawn new module
        if (other.gameObject.tag == "trigger")
        {
            Debug.Log("Triggert");

            // Debug.Log(other.gameObject.tag);
            manager.SpawnModule();
            manager.DestroyLastModule();
        }

        //Powerup Collisions
        if (other.gameObject.tag == "PUExtra Life")
        {
            Debug.Log("power Up");
            if (ExtraLifes < MaxELife)
            {
                ExtraLifes += 1;
            }

            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Slowmo")
        {
            SlowMo = true;
            Destroy(other.gameObject);
        }

        //Death Collisions (Extra Live Powerup)
        if (other.gameObject.tag == "obstacle")
        {
            if (ExtraLifes > 0)
            {
                // TODO: Explode
                Destroy(other.gameObject);
                ExtraLifes -= 1;
            }
            else
            {
                Debug.Log("Lost");
                SceneManager.LoadScene(0);
            }

        }

        if (other.gameObject.tag == "Death")
        {
            Debug.Log("Lost");
            SceneManager.LoadScene(0);
        }
    }
}
