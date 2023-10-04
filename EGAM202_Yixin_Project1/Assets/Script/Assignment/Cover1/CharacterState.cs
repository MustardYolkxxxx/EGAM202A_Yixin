using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class CharacterState : MonoBehaviour
{
    public float maxFollowDistance;
    public CharacterChosen characterChosenScript;
    public GameObject indicator;
    public GameObject tryToCarryIndicator;
    public GameObject carryingIndicator;
    public List<GameObject> patrolPosition;
    public NavMeshAgent agent;
    public enum State
    {
        Idle,
        NotChosen,
        Active,
        Carrying,
        TryToCarry,
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
        characterChosenScript = GetComponent<CharacterChosen>();
    }

    // Update is called once per frame
    void Update()
    {
        if(characterChosenScript.targetCarryObject != null&&currentState == State.Carrying) 
        {
            FollowTarget(characterChosenScript.targetCarryObject.transform.position);
        }
        indicator.SetActive(currentState == State.Active);
        tryToCarryIndicator.SetActive(currentState == State.TryToCarry);
        carryingIndicator.SetActive(currentState == State.Carrying);
        switch (currentState)
        {
            case State.Idle:
                Idle();

                break;

            case State.Active:
                Active();
                break;

            case State.Carrying:
                Carry();
                break;

            case State.TryToCarry:
                TryCarry();
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
    public void Active()
    {
        agent.isStopped = false;
        
    }
    public void Carry()
    {

    }
    public void TryCarry()
    {

    }

    public void FollowTarget(Vector3 target)
    {
        Vector3 delta = target - transform.position;
        if (delta.magnitude> maxFollowDistance) 
        {
            //transform.position = target - delta;
            transform.position = target + Vector3.ClampMagnitude(delta, maxFollowDistance);
            
        }
    }
}

