using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.AI;

public class Treasure : MonoBehaviour
{
    public GameObject cameraParent;
    public GameObject indicator;
    public Camera gameCamera;
    public int height;
    public int currentCarryNumber;
    public float catchRange;
    public CharacterChosen[] allCharacter;
    public CharacterState[] allCharacterState;

    public List<GameObject> carryThisTreasure;

    public enum CarryState
    {
        carry,
        notCarry,
        active,
    }
    public CarryState currentState;
    public bool isCarried;
    // Start is called before the first frame update
    void Start()
    {
        allCharacterState = FindObjectsOfType<CharacterState>();
        gameCamera = FindObjectOfType<Camera>();
    }


    // Update is called once per frame
    void Update()
    {
        currentCarryNumber = carryThisTreasure.Count;
        indicator.SetActive(currentState == CarryState.carry);
        if (carryThisTreasure.Count >= height)
        {
            isCarried = true;
            foreach (GameObject ob in carryThisTreasure)
            {
                ob.GetComponent<CharacterState>().currentState = CharacterState.State.Carrying;
            }
        }
        else
        {
            currentState = CarryState.notCarry;
            isCarried = false;
        }

        switch (currentState)
        {
            case CarryState.carry:
                UpdateCarry();
                break;
            case CarryState.notCarry:
                break;
            default:
                break;
        }

        if(isCarried)
        {
            transform.position = CalculateCenter(carryThisTreasure);
        }
        ChooseTreasure();
    }

    //public void CheckState()
    //{
    //    Debug.Log("1");
    //    foreach(CharacterState ch in allCharacterState)
    //    {
    //        Vector3 delta = transform.position - ch.gameObject.transform.position;
    //        if (delta.magnitude < catchRange)
    //        {
    //            Debug.Log("2");
    //            if (ch.currentState == CharacterState.State.TryToCarry)
    //            {
    //                Debug.Log("3");
    //                carryThisTreasure.Add(ch.gameObject);
    //            }
    //        }          
    //    }  
    //}
    public Vector3  CalculateCenter(List<GameObject> ob)
    {
        float totalX=0;
        float totalZ=0;
        foreach(GameObject obj in ob)
        {
            totalX += obj.transform.position.x;
            totalZ+= obj.transform.position.z;
        }
        float Xpos = totalX / ob.Count;
        float Zpos = totalZ / ob.Count;
        return new Vector3(Xpos, transform.position.y,Zpos);
    }
    public void AddGameObject(GameObject gameObject)
    {
        carryThisTreasure.Add(gameObject);
    }

    public void RemoveGameObject(GameObject gameObject)
    {
        if (carryThisTreasure.Count != 0)
        {
            carryThisTreasure.Remove(gameObject);
        }

    }

    public void ChooseTreasure()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePosition = Input.mousePosition;
            Ray mouseClickRay = gameCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(mouseClickRay, out RaycastHit hitinfo, 1000))
            {
                if (hitinfo.collider.name == gameObject.name)
                {
                        
                        foreach (GameObject ob in carryThisTreasure)
                        {
                            Vector3 delta = ob.transform.position - transform.position;
                            Vector3 targetPos = ob.transform.position+delta*5;
                            ob.GetComponent<CharacterChosen>().agent.SetDestination(targetPos);

                        }

                }
                else
                {
                    currentState = CarryState.notCarry;
                }
            }
        }

            if (Input.GetMouseButtonDown(0))
        {

            //if (CheckGuiRaycastObjects()) return;
            Vector2 mousePosition = Input.mousePosition;
            Ray mouseClickRay = gameCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(mouseClickRay, out RaycastHit hitinfo, 1000))
            {
                Debug.Log(hitinfo.collider.name);

                if (hitinfo.collider.tag == "Ground")
                {
                    if (currentState == CarryState.carry)
                    {
                        
                        foreach (GameObject ob in carryThisTreasure)
                        {
                            ob.GetComponent<CharacterChosen>().agent.SetDestination(hitinfo.point);
                        }
                    }

                }
                else if (hitinfo.collider.name == gameObject.name)
                {
                    if (isCarried)
                    {
                        currentState = CarryState.carry;

                    }
                }
                else
                {
                    currentState = CarryState.notCarry;
                }
            }
        }
    }
    public void UpdateCarry()
    {
        
    }

    public void ClearCarryObject()
    {
        foreach (GameObject ob in carryThisTreasure)
        {
            ob.GetComponent<CharacterState>().currentState = CharacterState.State.NotChosen;
        }
        Destroy(gameObject);
    }


    //public void CheckAgain()
    //{
    //    if (carryThisTreasure.Count > 0)
    //    {
    //        foreach (GameObject ob in carryThisTreasure)
    //        {
    //            if (ob.GetComponent<CharacterState>().currentState != CharacterState.State.TryToCarry)
    //            {
    //                carryThisTreasure.Remove(ob);
    //            }
    //        }
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Character"))
    //    {
    //        currentCarryNumber++;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Character"))
    //    {
    //        currentCarryNumber--;
    //    }
    //}
}
    
