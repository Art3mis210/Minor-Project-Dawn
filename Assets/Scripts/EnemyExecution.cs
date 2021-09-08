using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExecution : MonoBehaviour
{
    private Animator EnemyAnimator;
    private Rigidbody EnemyRigidBody;
    public CapsuleCollider EnemyCollider;
    void Start()
    {
        EnemyAnimator = gameObject.transform.parent.GetComponent<Animator>();
        EnemyRigidBody = gameObject.transform.parent.GetComponent<Rigidbody>();
    }
    public void FreezeEnemy()
    {
        //EnemyRigidBody.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void BrutalExecuteEnemy()
    {
        EnemyAnimator.SetBool("BrutalExecute", true);
        Invoke("BrutalChangeBoxColliderSize", 0.5f);
    }
    public void StealthExecuteEnemy()
    {
        EnemyAnimator.SetBool("StealthExecute", true);
        Invoke("StealthChangeBoxColliderSize", 0.5f);
    }
    public void BrutalChangeBoxColliderSize()
    {
        EnemyCollider.height = 0.22f;
    }
    public void StealthChangeBoxColliderSize()
    {
        EnemyCollider.height = 0.22f;
        EnemyCollider.radius = 0f;
    }

}
