using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Monster : MonoBehaviour
{
    public GameObject target;
    public float chaseRange;
    public float attackRange;
    float distanceToTarget;
    bool withinRange;
    Vector3 lastKnownLocation;
    public NavMeshAgent agent;

    public SceneSwitcher switcher;
    private bool playerIsDead;
    //public AudioClip catchSound;

    // Start is called before the first frame update
    void Start()
    {
        withinRange = false;        
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = (agent.transform.position - target.transform.position).magnitude;

        if (distanceToTarget < chaseRange)
        {
            withinRange = true;
        }
        else
        {
            withinRange = false;
        }

        if (withinRange == true)
        {
            lastKnownLocation = target.transform.position;
            agent.SetDestination(lastKnownLocation);

            if (distanceToTarget < attackRange)
            {
                if (playerIsDead == false)
                {
                    playerIsDead = true;
                    KillPlayer();
                }
                
            }
        }

    }

    void KillPlayer()
    {
        //KILL PLAYER IN HERE

        switcher.ToggleSceneChange();

        //target.GetComponent<Player>().controlsLocked = true;

        //play catch sound
    }

}

