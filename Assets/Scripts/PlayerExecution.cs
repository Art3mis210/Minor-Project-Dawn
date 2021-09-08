using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExecution : MonoBehaviour
{
    private Animator PlayerAnimator;
    private PlayerMovement playerMovement;
    private CapsuleCollider PlayerCollider;
    private Rigidbody PlayerRigidBody;
    void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        PlayerRigidBody = GetComponent<Rigidbody>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag=="EnemyExecutionTrigger")
        {
            if(Input.GetKey(KeyCode.E))
            {
                if(playerMovement.Stealth==false)
                    StartCoroutine(BrutalMoveToExecutionPosition(other.gameObject.transform, 1f));
                else
                    StartCoroutine(StealthMoveToExecutionPosition(other.gameObject.transform, 1f));
                other.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
    IEnumerator BrutalMoveToExecutionPosition(Transform Enemy,float Duration)
    {
        float t = 0f;
        while (t < Duration)
        {
            transform.position = Vector3.Lerp(transform.position, Enemy.position, t / Duration);
            yield return null;
            t += Time.deltaTime;
        }
        StartCoroutine(MoveToExecutionRotation(Enemy, 1f));
        //transform.localRotation = end;
    }
    IEnumerator StealthMoveToExecutionPosition(Transform Enemy, float Duration)
    {
        float t = 0f;
        while (t < Duration)
        {
            transform.position = Vector3.Lerp(transform.position, Enemy.position+0.25f*Enemy.transform.right, t / Duration);
            yield return null;
            t += Time.deltaTime;
        }
        StartCoroutine(MoveToExecutionRotation(Enemy, 1f));
        //transform.localRotation = end;
    }
    IEnumerator MoveToExecutionRotation(Transform Enemy,float Duration)
    {
        float t = 0f;
        while (t < Duration)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Enemy.rotation, t / Duration);
            yield return null;
            t += Time.deltaTime;
        }
        EnemyExecution enemyExecution = Enemy.gameObject.GetComponent<EnemyExecution>();
        enemyExecution.FreezeEnemy();
        if(playerMovement.Stealth==false)
        {
            enemyExecution.BrutalExecuteEnemy();
            PlayerAnimator.SetBool("BrutalExecution", true);
        }
        else
        {
            enemyExecution.StealthExecuteEnemy();
            PlayerAnimator.SetBool("StealthExecution", true);
        }
    }

}
