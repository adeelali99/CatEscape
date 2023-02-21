using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    Animator animator;
    Transform trans;

    private float speed = 20;
    private bool gameWon = false;

    [SerializeField] private CharacterController controller;
    [SerializeField] private FloatingJoystick joystick;
    public GameObject joyBackground;
    public GameObject invisibleWall;
    public GameObject vase;
    public GameObject switchButton;

    Vector2 startPos;
    Vector2 direction;

    void Start()
    {
        vase.SetActive(false);
        switchButton.GetComponent<Transform>().position = new Vector3(1.5f , 0.08f , 2.8f);

        animator = GetComponent<Animator>();
        trans = GetComponent<Transform>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            joyBackground.SetActive(true);
        }
        
        if(!gameWon){
            move();
        }
        
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
        //touching consumable
        if(other.gameObject.layer == 6){
            Destroy(other.gameObject);
            invisibleWall.SetActive(false);
        }

        //touching switch
        if(other.gameObject.layer == 7){
            switchButton.GetComponent<Transform>().position = new Vector3(1.5f , 0f , 2.8f);
            vase.SetActive(true);
        }

        //Game won
        if(other.gameObject.layer == 8){
            animator.SetBool("sound", true);
            gameWon = true;
        }
    }
}
