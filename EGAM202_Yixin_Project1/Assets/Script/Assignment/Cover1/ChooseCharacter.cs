using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ChooseCharacter : MonoBehaviour
{
    public GameObject cameraParent;
    public Camera gameCamera;
    public GameObject[] gameObjects;
    public List<NavMeshAgent> characters;
    public int characterIndex;
    public float x;
    public float y;
    public float z;
    public float smooth;
    public float moveTime;
    public bool cameraMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (characterIndex != 4)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FocusOnCharacter(gameObjects[characterIndex].transform.position);
            }
        }

        if(cameraMove&&characterIndex!=4)
        {
            
            moveTime += Time.deltaTime;
            cameraParent.transform.position = Vector3.Lerp(cameraParent.transform.position, new Vector3(gameObjects[characterIndex].transform.position.x + x, 0, gameObjects[characterIndex].transform.position.z + z), smooth * Time.deltaTime);
            if(math.abs(cameraParent.transform.position.x - gameObjects[characterIndex].transform.position.x)<61
                && math.abs(cameraParent.transform.position.x - gameObjects[characterIndex].transform.position.x) > 59
                && math.abs(cameraParent.transform.position.z - gameObjects[characterIndex].transform.position.z) < 31
                && math.abs(cameraParent.transform.position.z - gameObjects[characterIndex].transform.position.z) > 29)
            {
                cameraMove = false;
                
            }
        }
        
        ChangeACharacter();
        //ChosenCharacterMove();
    }
    void ChosenCharacterMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            Ray mouseClickRay = gameCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(mouseClickRay, out RaycastHit hitinfo, 1000))
            {
                Debug.DrawLine(mouseClickRay.origin, hitinfo.point, Color.red, 1f);
                if (hitinfo.collider.tag == "Ground")
                {
                    characters[characterIndex].destination= hitinfo.point;
                }
            }
        }
    }
    void ChangeACharacter()
    {
        if (Input.GetMouseButton(1))
        {
            characterIndex = 4;
            gameObjects[0].transform.GetChild(0).gameObject.SetActive(false);
            gameObjects[1].transform.GetChild(0).gameObject.SetActive(false);
            gameObjects[2].transform.GetChild(0).gameObject.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckGuiRaycastObjects()) return;
                Vector2 mousePosition = Input.mousePosition;
            Ray mouseClickRay = gameCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(mouseClickRay, out RaycastHit hitinfo, 1000))
            {
                if (hitinfo.collider.tag == "Ground")
                {
                    if(characterIndex != 4)
                    {
                        characters[characterIndex].destination = hitinfo.point;
                    }
                   
                }
                Debug.DrawLine(mouseClickRay.origin, hitinfo.point,Color.red,1f);
                if (hitinfo.collider.name == "CharacterFire")
                {
                    characterIndex = 0;
                    gameObjects[0].transform.GetChild(0).gameObject.SetActive(true);
                    gameObjects[1].transform.GetChild(0).gameObject.SetActive(false);
                    gameObjects[2].transform.GetChild(0).gameObject.SetActive(false);
                    FocusOnCharacter(hitinfo.transform.position);
                }
                else if (hitinfo.collider.name == "CharacterWater")
                {                  
                    characterIndex = 1;
                    gameObjects[1].transform.GetChild(0).gameObject.SetActive(true);
                    gameObjects[0].transform.GetChild(0).gameObject.SetActive(false);
                    gameObjects[2].transform.GetChild(0).gameObject.SetActive(false);
                    FocusOnCharacter(hitinfo.transform.position);
                }
                else if (hitinfo.collider.name == "CharacterElectric")
                {
                    
                    characterIndex = 2;
                    gameObjects[2].transform.GetChild(0).gameObject.SetActive(true);
                    gameObjects[1].transform.GetChild(0).gameObject.SetActive(false);
                    gameObjects[0].transform.GetChild(0).gameObject.SetActive(false);
                    FocusOnCharacter(hitinfo.transform.position);
                }
            }
        }
    }

    void FocusOnCharacter(Vector3 position)
    {
        if (characterIndex != 4)
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

    public void ClickFire()
    {
        characterIndex = 0;
        gameObjects[0].transform.GetChild(0).gameObject.SetActive(true);
        gameObjects[1].transform.GetChild(0).gameObject.SetActive(false);
        gameObjects[2].transform.GetChild(0).gameObject.SetActive(false);
        FocusOnCharacter(gameObjects[characterIndex].transform.position);
    }

    public void ClickWater()
    {
        characterIndex = 1;
        gameObjects[1].transform.GetChild(0).gameObject.SetActive(true);
        gameObjects[0].transform.GetChild(0).gameObject.SetActive(false);
        gameObjects[2].transform.GetChild(0).gameObject.SetActive(false);
        FocusOnCharacter(gameObjects[characterIndex].transform.position);
    }

    public void ClickElectric()
    {
        characterIndex = 2;
        gameObjects[2].transform.GetChild(0).gameObject.SetActive(true);
        gameObjects[1].transform.GetChild(0).gameObject.SetActive(false);
        gameObjects[0].transform.GetChild(0).gameObject.SetActive(false);
        FocusOnCharacter(gameObjects[characterIndex].transform.position);
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
}
