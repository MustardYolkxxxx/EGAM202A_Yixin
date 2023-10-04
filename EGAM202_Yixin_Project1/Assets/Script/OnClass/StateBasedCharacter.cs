using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateBasedCharacter : MonoBehaviour
{
    public float randomTime;
    public float randomTimeMax;
    public float randomRangeMin;
    public float randomRangeMax;
    public int patrolIndex;
    public List<GameObject> patrolPosition;
    public NavMeshAgent agent;
    public enum State
    {
        Idle,
        Patrol,
        RandomPos,
    }

    public enum Colors
    {
        Red,
        Blue,
        Yellow,
    }
    public State currentState;
    public Colors currentColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                Idle();

                break;
            case State.Patrol:
                Patrol();
                break;
            case State.RandomPos:
                RandomPosition();
                break;
        }

        switch (currentColor)
        {
            case Colors.Red:
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                break;
            case Colors.Blue:
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case Colors.Yellow:
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                break;
        }
    }

    public void Idle()
    {
        agent.isStopped = true;
    }

    public void Patrol()
    {
        agent.isStopped = false;
        Vector3 targetPosition = patrolPosition[patrolIndex].transform.position;
        agent.SetDestination(targetPosition);

        Vector3 myPosition = agent.transform.position;
        Vector3 delta = targetPosition - myPosition;
        if (delta.magnitude < 1f)
        {
            patrolIndex++;
            if (patrolIndex >= patrolPosition.Count)
            {
                patrolIndex = 0;
            }
        }
    }
    public void RandomPosition()
    {
        agent.isStopped = false;
        randomTime += Time.deltaTime;
        if (randomTime >= randomTimeMax)
        {
            randomTime = 0;
            float randomX = Random.Range(randomRangeMin, randomRangeMax);
            float randomz = Random.Range(randomRangeMin, randomRangeMax);
            Vector3 newRandomPosition = new Vector3(transform.position.x+randomX, transform.position.y, transform.position.z + randomz);
            agent.SetDestination(newRandomPosition);
        }
    }
}
