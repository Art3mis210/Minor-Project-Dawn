using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPick : MonoBehaviour
{
    private Animator EnemyAnimator;
    private Rigidbody EnemyRigidBody;
    private BoxCollider TriggerCollider;
    private Transform EnemyTransform;
    void Start()
    {
        EnemyAnimator = gameObject.transform.parent.GetComponent<Animator>();
        EnemyTransform = gameObject.transform.parent.GetComponent<Transform>();
        EnemyRigidBody = gameObject.transform.parent.GetComponent<Rigidbody>();
        TriggerCollider = GetComponent<BoxCollider>();
    }
    public void EnemyPickUp(Transform PlayerTransform)
    {
        StartCoroutine(MoveToPlayer(PlayerTransform, 0.1f));
        TriggerCollider.enabled = false;
    }
    public void EnemyThrow()
    {
        //EnemyAnimator.SetBool("Throw", false);
        EnemyRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        transform.parent.gameObject.transform.parent = null;
        //TriggerCollider.enabled = true;
    }
    IEnumerator MoveToPlayer(Transform PlayerTransform,float Duration)
    {
        float t = 0f;
        while (t < Duration)
        {
            transform.parent.localPosition = Vector3.Lerp(transform.parent.localPosition, PlayerTransform.position, t / Duration);
            yield return null;
            t += Time.deltaTime;
        }
        StartCoroutine(RotateEnemy(PlayerTransform, 0.1f));

    }
    IEnumerator RotateEnemy(Transform PlayerTransform, float Duration)
    {
        float t = 0f;
        while (t < Duration)
        {
            transform.parent.localRotation = Quaternion.Slerp(transform.parent.localRotation, PlayerTransform.rotation, t / Duration);
            yield return null;
            t += Time.deltaTime;
        }
        transform.parent.transform.parent = PlayerTransform.gameObject.transform;
        EnemyRigidBody.constraints=RigidbodyConstraints.FreezeAll;
        EnemyAnimator.SetBool("Carry", true);
        
    }
}
