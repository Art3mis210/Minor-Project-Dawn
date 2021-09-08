using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    private Animator PlayerAnimator;
    public int Sensitivity;
    private bool StartRotate;
    private Vector2 Turn;
    void Start()
    {
        PlayerAnimator = transform.parent.gameObject.GetComponent<Animator>();
        StartRotate = false;
    }
    // Update is called once per frame
    void Update()
    {

        if (!PlayerAnimator.GetBool("Front") && !PlayerAnimator.GetBool("Back") && !PlayerAnimator.GetBool("Left") && !PlayerAnimator.GetBool("Right"))
        {
            Turn.x += Input.GetAxis("Mouse X") * Sensitivity;
            Turn.y += Input.GetAxis("Mouse Y") * Sensitivity;
            Turn.y = Mathf.Clamp(Turn.y, -90f, 30f);
            transform.localRotation = Quaternion.Euler(-Turn.y, Turn.x, 0);
            StartRotate = false;
        }
        else if(StartRotate==false)
        {
            StartRotate = true;
            Turn = new Vector2(0, 0);
            StartCoroutine(BringCameraToCentre(transform.localRotation, Quaternion.Euler(0f, 0f, 0f), 1f));
        }
    }
    IEnumerator BringCameraToCentre(Quaternion start,Quaternion end,float Duration)
    {
        float t = 0f;
        while (t < Duration)
        {
            transform.localRotation = Quaternion.Slerp(start, end, t / Duration);
            yield return null;
            t += Time.deltaTime;
        }
        //transform.localRotation = end;
    }
}
