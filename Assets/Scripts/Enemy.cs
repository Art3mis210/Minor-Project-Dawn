using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject Player;
    public bool PlayerFound;
    private RaycastHit hit;
    private Animator EnemyAnimator;
    public int CloseEnoughDistance;
    public int AimDistance;
    public int FarEnoughDistance;
    private bool Alert;
    void Start()
    {
        PlayerFound = false;
        EnemyAnimator = GetComponent<Animator>();
        Alert = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerFound == true)
        {
            Vector3 PlayerPosition = new Vector3(Player.transform.position.x,transform.position.y,Player.transform.position.z);
            transform.LookAt(PlayerPosition);
            DecideAction();
        }
        else
            CheckForIntruders();
    }
    private void CheckForIntruders()
    {
        Vector3 StartVector = new Vector3(0, 10, 0) + transform.position;
        Vector3 FrontDirectionVector = (transform.forward) * 100;
        Vector3 RightDirectionVector = (transform.forward + transform.right) * 100;
        Vector3 LeftDirectionVector = (transform.forward - transform.right) * 100;
        Debug.DrawRay(StartVector, FrontDirectionVector, Color.red, 0.5f);
        Debug.DrawRay(StartVector,RightDirectionVector, Color.red, 0.5f);
        Debug.DrawRay(StartVector, LeftDirectionVector, Color.red, 0.5f);
        if (Physics.Raycast(StartVector,FrontDirectionVector,out hit,50f) || Physics.Raycast(StartVector, RightDirectionVector, out hit, 50f) || Physics.Raycast(StartVector, LeftDirectionVector, out hit, 50f))
        {
            if(hit.transform.gameObject.tag=="Player")
            {
                PlayerFound = true;
                Player = hit.transform.gameObject;
            }
        }
    }
    private void DecideAction()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) > FarEnoughDistance)
        {
            if (Alert == false)
            {
                EnemyAnimator.SetBool("Walk", false);
                EnemyAnimator.SetBool("Sprint", false);
                PlayerLost();
            }
        }
        else if (Vector3.Distance(Player.transform.position, transform.position) < CloseEnoughDistance)
        {
            EnemyAnimator.SetBool("Sprint", false);
            EnemyAnimator.SetBool("Walk", true);
        }
        else if (Vector3.Distance(Player.transform.position, transform.position) > CloseEnoughDistance)
        {
            EnemyAnimator.SetBool("Walk", true);
            EnemyAnimator.SetBool("Sprint", true);
        }

    }
    private void PlayerLost()
    {
        PlayerFound = false;
        Player = null;
        Alert = true;
    }

}
