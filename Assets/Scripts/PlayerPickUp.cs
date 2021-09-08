using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    private Animator PlayerAnimator;
    private PlayerMovement playerMovement;
    public Transform PickUpLocation;
    public bool PickUp;
    private EnemyPick CurrentPickedEnemy;
    void Start()
    {
        PickUp = false;
        PlayerAnimator = GetComponent<Animator>();
        CurrentPickedEnemy = null;
    }
    private void Update()
    {
        //if(PickUp==true && Input.GetKeyDown(KeyCode.E))
        //{
        //    Throw();
        //}
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "EnemyPickUpTrigger")
        {
            if(Input.GetKey(KeyCode.E))
            {
                Debug.Log("PICKING");
                PickUp = true;
                CurrentPickedEnemy = other.gameObject.GetComponent<EnemyPick>();
                CurrentPickedEnemy.EnemyPickUp(PickUpLocation);
                PlayerAnimator.SetBool("Carry", true);
            }
        }
    }
    private void Throw()
    {
        CurrentPickedEnemy.EnemyThrow();
        PickUp = false;
        PlayerAnimator.SetBool("Throw", true);
    }
}
