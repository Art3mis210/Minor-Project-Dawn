using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDive : MonoBehaviour
{
    private Animator PlayerAnimator;
    private Rigidbody PlayerRigidBody;
    public bool ExitedPlane;
    private Vector3 Turn;
    void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
        PlayerRigidBody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            PlayerAnimator.SetBool("Move", true);
            if (ExitedPlane == true)
            {
                Physics.gravity = new Vector3(0, -10000f, 0);
                Turn.x += Input.GetAxis("Horizontal");
                transform.localRotation = Quaternion.Euler(0, Turn.x, 0);
            }
        }
        else
        {
            PlayerAnimator.SetBool("Move", false);
            if (ExitedPlane == true)
                Physics.gravity = new Vector3(0, -9000f, 0);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag=="Plane")
        {
            ExitedPlane = true;
        }
    }
}
