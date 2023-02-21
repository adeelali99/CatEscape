using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //hit by vase
        if(other.gameObject.layer == 9){
            animator.SetBool("dead", true);
            Debug.Log("dead");
        }
    }
}
