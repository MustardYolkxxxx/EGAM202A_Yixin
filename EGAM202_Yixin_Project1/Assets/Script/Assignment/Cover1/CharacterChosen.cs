using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class CharacterChosen : MonoBehaviour
{

    public GameObject cameraParent;
    public GameObject clickIndicator;
    public Camera gameCamera;
    public NavMeshAgent agent;
    public CharacterState characterState;

    public GameObject targetCarryObject;
    public int characterIndex;
    public float smooth;
    public float touchDistance;
    public float loseDistance;
    public float moveTime;
    public bool cameraMove;
    public bool isCarry;
    // Start is called before the first frame update
    void Start()
    {
        characterState= GetComponent<CharacterState>();
        gameCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(characterState.currentState == CharacterState.State.NotChosen)
        {
            targetCarryObject = null;
        }
        ChangeACharacter();
        CheckTargerDistance();
    }

    void ChangeACharacter()
    {
        if (Input.GetMouseButton(1))
        {
            if (characterState.currentState == CharacterState.State.Active)
            {
                characterState.currentState = CharacterState.State.Idle;
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
                    if (characterState.currentState == CharacterState.State.Active)
                    {
                        agent.SetDestination(hitinfo.point);
                        
                    }
                       
                }
                else if (hitinfo.collider.tag == "Treasure")
                {
                    
                    if (characterState.currentState == CharacterState.State.Active&& 
                        hitinfo.collider.GetComponent<Treasure>().isCarried==false)
                    {
                        agent.SetDestination(hitinfo.point);
                        targetCarryObject = hitinfo.collider.gameObject;
                    }
                }
                

               else if (hitinfo.collider.name == gameObject.name)
                {
                    if(targetCarryObject!= null)
                    {
                        ClearTarget();
                    }
                    
                    characterState.currentState = CharacterState.State.Active;
                    //FocusOnCharacter(hitinfo.transform.position);
                }

                else if (characterState.currentState != CharacterState.State.Carrying && characterState.currentState != CharacterState.State.TryToCarry)
                {
                    characterState.currentState = CharacterState.State.NotChosen;
                }

            }
        }
    }
    void FocusOnCharacter(Vector3 position)
    {
        if (characterState.currentState == CharacterState.State.Active)
        {
            if (transform.position != position)
            {
                moveTime = 0;
                cameraMove = true;
                //cameraParent.transform.position = new Vector3(position.x+x,0,position.z+z);
                //    cameraParent.transform.position = Vector3.Lerp(cameraParent.transform.position, new Vector3(position.x + x, 0, position.z + z), smooth*Time.deltaTime);
            }
        }
    }
    bool CheckGuiRaycastObjects()
    {
        // PointerEventData eventData = new PointerEventData(Main.Instance.eventSystem);

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.pressPosition = Input.mousePosition;
        eventData.position = Input.mousePosition;

        List<RaycastResult> list = new List<RaycastResult>();
        // Main.Instance.graphicRaycaster.Raycast(eventData, list);
        EventSystem.current.RaycastAll(eventData, list);
        //Debug.Log(list.Count);
        return list.Count > 0;
    }
    public void CheckCarryState()
    {
        if(targetCarryObject.GetComponent<Treasure>().currentCarryNumber>= targetCarryObject.GetComponent<Treasure>().height)
        {
            characterState.currentState = CharacterState.State.Carrying;
        }
    }

    public void ClearTarget()
    {
        targetCarryObject.GetComponent<Treasure>().RemoveGameObject(gameObject);
        //targetCarryObject.GetComponent<Treasure>().currentCarryNumber--;
        isCarry = false;
        targetCarryObject = null;
    }
    public void CheckTargerDistance()
    {

        if(targetCarryObject!= null)
        {
            
            if ((targetCarryObject.transform.position - transform.position).magnitude < touchDistance)
            {
                
                if (!isCarry)
                {
                    targetCarryObject.GetComponent<Treasure>().currentCarryNumber++;
                    targetCarryObject.GetComponent<Treasure>().AddGameObject(gameObject);
                    characterState.currentState = CharacterState.State.TryToCarry;
                    isCarry = true;                 
                }
                
            }

            else if ((targetCarryObject.transform.position - transform.position).magnitude > loseDistance&& isCarry)
            {
                characterState.currentState = CharacterState.State.NotChosen;
                isCarry = false;
                ClearTarget();

            }
            //else
            //{                                          
            //    if (isCarry == true)
            //    {
            //        targetCarryObject.GetComponent<Treasure>().RemoveGameObject(gameObject);
            //        targetCarryObject.GetComponent<Treasure>().currentCarryNumber--;
            //        isCarry = false;
            //    }
            //}
        }
       
    }
}
