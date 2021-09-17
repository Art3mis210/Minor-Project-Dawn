using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExecution : MonoBehaviour
{
    private Animator EnemyAnimator;
    private Rigidbody EnemyRigidBody;
    public CapsuleCollider EnemyCollider;
    private Enemy EnemyState;
    void Start()
    {
        EnemyAnimator = gameObject.transform.parent.GetComponent<Animator>();
        EnemyRigidBody = gameObject.transform.parent.GetComponent<Rigidbody>();
        EnemyState = transform.parent.GetComponent<Enemy>();
    }
    public void FreezeEnemy()
    {
        EnemyState.Alive = false;
    }
    public void BrutalExecuteEnemy()
    {
        EnemyAnimator.SetBool("BrutalExecute", true);
        Invoke("BrutalChangeBoxColliderSize", 0.5f);
    }
    public void StealthExecuteEnemy()
    {
        EnemyState.Alive = false;
        EnemyAnimator.SetBool("StealthExecute", true);
        Invoke("StealthChangeBoxColliderSize", 0.5f);
    }
    public void BrutalChangeBoxColliderSize()
    {
        EnemyCollider.height = 0.22f;
        enabled = false;
    }
    public void StealthChangeBoxColliderSize()
    {
        EnemyCollider.height = 0.22f;
        EnemyCollider.radius = 0f;
        enabled = false;
    }

}
