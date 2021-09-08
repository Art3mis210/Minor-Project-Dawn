using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator PlayerAnimator;
    private int Sensitivity;
    private Vector2 Turn;
    public bool Stealth;
    void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
        Sensitivity = 4;
        Stealth = false;
    }

    void FixedUpdate()
    {
        PlayerAnimator.SetBool("Front", Input.GetKey(KeyCode.W));
        PlayerAnimator.SetBool("Back", Input.GetKey(KeyCode.S));
        PlayerAnimator.SetBool("Left", Input.GetKey(KeyCode.A));
        PlayerAnimator.SetBool("Right", Input.GetKey(KeyCode.D));
        PlayerAnimator.SetBool("Sprint", Input.GetKey(KeyCode.LeftShift));
        PlayerAnimator.SetBool("Slide", Input.GetKey(KeyCode.Z));
        PlayerAnimator.SetBool("Jump", Input.GetKey(KeyCode.Space));
        PlayerAnimator.SetBool("ChangeStance", Input.GetKey(KeyCode.LeftControl));
        if (PlayerAnimator.GetBool("Front") || PlayerAnimator.GetBool("Back") || PlayerAnimator.GetBool("Left") || PlayerAnimator.GetBool("Right"))
        {
            Turn.x += Input.GetAxis("Mouse X") * Sensitivity;
            transform.localRotation = Quaternion.Euler(0, Turn.x, 0);
        }
    }
    public void StopAnimation(string AnimationName)
    {
        PlayerAnimator.SetBool(AnimationName, false);
        if (AnimationName == "ChangeStance")
            Stealth = !Stealth;
    }
}
