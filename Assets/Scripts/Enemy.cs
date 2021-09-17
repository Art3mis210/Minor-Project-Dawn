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
    public bool Alive;
    public GameObject PickUpPoint;
    public List<Transform> EnemyPath;
    public int CurrentPath;
    private Vector3 PathDirection;
    public bool Change;
    void Start()
    {
        PlayerFound = false;
        EnemyAnimator = GetComponent<Animator>();
        Alert = false;
        Alive = true;
        CurrentPath = 0;
        PathDirection = new Vector3(EnemyPath[CurrentPath].position.x, transform.position.y, EnemyPath[CurrentPath].position.z);
    }

    // Update is called once per frame
    void Update()
    {

        if (Alive == true)
        {
            if (PlayerFound == true)
            {
                Vector3 PlayerPosition = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
                transform.LookAt(PlayerPosition);
                DecideAction();
            }
            else
            {
                CheckForIntruders();
                FollowPath();
            }
        }
        else
        {
            FreezeEnemy();
            //PickUpPoint.SetActive(true);
            enabled = false;
        }
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
    private void FollowPath()
    {
        if (PlayerFound == false)
        {
            //StartCoroutine(RotateTowardsNextPath(EnemyPath[CurrentPath], 2f));
            PathDirection = new Vector3(EnemyPath[CurrentPath].position.x, transform.position.y, EnemyPath[CurrentPath].position.z);
            transform.LookAt(PathDirection);
            EnemyAnimator.SetBool("Walk", true);
            Debug.Log(Vector3.Distance(transform.localPosition, EnemyPath[CurrentPath].position));
            if (Vector3.Distance(transform.localPosition,EnemyPath[CurrentPath].position)<20f)
            {
                Debug.Log("Rotate");
                CurrentPath = (CurrentPath + 1) % EnemyPath.Count;
            }
        }
    }
    IEnumerator RotateTowardsNextPath(Transform Path, float Duration)
    {
        float t = 0f;
        while (t < Duration)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(Path.position - transform.position), t / Duration);
            yield return null;
            t += Time.deltaTime;
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
    public void FreezeEnemy()
    {
        EnemyAnimator.SetBool("Sprint", false);
        EnemyAnimator.SetBool("Walk", false);
        
    }
    private void PlayerLost()
    {
        PlayerFound = false;
        Player = null;
        Alert = true;
    }

}
