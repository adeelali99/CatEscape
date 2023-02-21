using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    Animator animator;
    Transform trans;

    private float speed = 20;

    [SerializeField] private CharacterController controller;
    [SerializeField] private FloatingJoystick joystick;
    public GameObject joyBackground;
    public GameObject invisibleWall;
    public GameObject lockedDoor;

    Vector2 startPos;
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        trans = GetComponent<Transform>();

        //bricks1Rigid.GetComponent<Rigidbody>().isKinematic = true;
        //bricks2Rigid.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            joyBackground.SetActive(true);
        }
        
        move();
    }

    private void move(){

        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        if(horizontal >= 0.2f || horizontal <= -0.2f || vertical >= 0.2f || vertical <= -0.2f){
            animator.SetBool("isRunning", true);

            Vector3 direction = new Vector3(horizontal , -9f, vertical).normalized;

            if(direction.magnitude >= 0.1f){

           float targetAngle = Mathf.Atan2(direction.x , direction.z) * Mathf.Rad2Deg;
           trans.rotation = Quaternion.Euler(0f , targetAngle , 0f); 

           controller.Move(direction * speed * Time.deltaTime);
            }
        }
        else{
                direction = new Vector3(0 , -9f, 0);
                animator.SetBool("isRunning", false);
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6){
            Destroy(other.gameObject);
            //bricks1Rigid.GetComponent<Rigidbody>().isKinematic = false;
            //bricks2Rigid.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
