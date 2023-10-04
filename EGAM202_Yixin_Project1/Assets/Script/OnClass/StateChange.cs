using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
public class StateChange : MonoBehaviour
{
    public TextMeshProUGUI textState;
    public TextMeshProUGUI textColor;
    public StateBasedCharacter stateBasedCharacter;
    public StateBasedCharacter[] characterScripts;

   
    
    public List<GameObject>[] waitingObject;
    public List<NavMeshAgent> navMeshAgents;
    public List<GameObject> agentGameobject;
    
    public NavMeshAgent agent;

    //public enum State
    //{
    //    Idle,
    //    Patrol,
    //    RandomPos,
    //}

    //public enum Colors
    //{
    //    Red,
    //    Blue,
    //    Yellow,
    //}
    public StateBasedCharacter.Colors currentColor;
    public StateBasedCharacter.Colors lastColor;
    public StateBasedCharacter.State currentState;
    public StateBasedCharacter.State lastState;
    // Start is called before the first frame update
    void Start()
    {
        characterScripts = FindObjectsOfType<StateBasedCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAllColorState();
        //switch(currentState)
        //{
        //    case State.Idle:


        //         break; 
        //    case State.Patrol:

        //        break; 
        //    case State.RandomPos:

        //        break;
        //}

        //switch(currentColor)
        //{
        //    case Colors.Red:
        //        break; 
        //    case Colors.Blue:
        //        break; 
        //    case Colors.Yellow: 
        //        break;
        //}

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            lastState = StateBasedCharacter.State.Idle;
            textState.text = "Idle";
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            lastState = StateBasedCharacter.State.Patrol;
            textState.text = "Patrol";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            lastState = StateBasedCharacter.State.RandomPos;
            textState.text = "Random";
        }


        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            lastColor = StateBasedCharacter.Colors.Red;
            textColor.text = "Red";
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            lastColor = StateBasedCharacter.Colors.Blue;
            textColor.text = "Blue";
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            lastColor = StateBasedCharacter.Colors.Yellow;
            textColor.text = "Yellow";
        }
    }
   

    public void ChangeAllColorState()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
           foreach(StateBasedCharacter t in characterScripts)
            {
                if(t.currentColor == lastColor)
                {
                    t.currentState = lastState;
                }
            }
        }


    }
}
