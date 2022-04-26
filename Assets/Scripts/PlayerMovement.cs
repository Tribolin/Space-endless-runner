using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHight = 3f;
 
    public Transform feed;
    public Transform rightArm;
    public Transform leftArm;

    public float wallDistance = 0.1f;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    Vector3 velo;

    bool onRightWall;
    bool onLeftWall;
    bool isGrounded;
    Management manager;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Management>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(feed.position, groundDistance, groundMask);
        onLeftWall = Physics.CheckSphere(leftArm.position, wallDistance, groundMask);
        onRightWall = Physics.CheckSphere(rightArm.position, wallDistance, groundMask);
        if ((isGrounded && velo.y < 0)  )
        {
            velo.y = -2f;
        }
        else
        {
            if(onRightWall || onLeftWall)
            {
                velo.y = -1.5f;
            }
        }
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velo.y += Mathf.Sqrt(jumpHight * -2f * gravity );
        }
        float x = Input.GetAxis("Horizontal");
        //float y = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x);//+ (transform.forward * y);
        
        controller.Move(move * speed * Time.deltaTime);
        velo.y  += gravity * Time.deltaTime;
        controller.Move(velo * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "trigger")
        {

            //Debug.Log("collision");
           // Debug.Log(other.gameObject.tag);
            manager.SpawnModule();
            manager.DestroyLastModule();
        }
        if(other.gameObject.tag == "obstacle")
        {
            Debug.Log("Lost");
        }
    }
}
