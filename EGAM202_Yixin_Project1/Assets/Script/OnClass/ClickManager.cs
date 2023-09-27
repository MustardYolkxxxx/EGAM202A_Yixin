using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickManager : MonoBehaviour
{

    public Camera screenCamera;
    public CameraSwitcher cameraScript;
    public bool isclick;
    // Start is called before the first frame update
    void Start()
    {
        cameraScript=GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraSwitcher>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            Ray mouseClickRay = screenCamera.ScreenPointToRay(mousePosition);
            if(Physics.Raycast(mouseClickRay, out RaycastHit hitinfo, 100))
            {
                if (hitinfo.collider.name == "Cube1"|| hitinfo.collider.name == "Cube2")
                {
                    ChangeColor changeColorScript = hitinfo.collider.gameObject.GetComponent<ChangeColor>();
                    changeColorScript.ClickChangeColor();
                }

                if (hitinfo.collider.name == "PlanePink" )
                {
                    Debug.Log(hitinfo.collider.name);
                    if (cameraScript.activeCameraIndex == 0)
                    {
                        Debug.Log("11");
                        cameraScript.DifferentRoomChange(4);
                        return;
                    }
                    cameraScript.DifferentRoomChange(0);

                }
                if (hitinfo.collider.name == "PlaneBlue" )
                {
                    if (cameraScript.activeCameraIndex == 1)
                    {
                        cameraScript.DifferentRoomChange(4);
                        return;
                    }
                    cameraScript.DifferentRoomChange(1);     
                }          

                if (hitinfo.collider.name == "PlaneGreen")
                {
                    if (cameraScript.activeCameraIndex == 2)
                    {
                        cameraScript.DifferentRoomChange(4);
                        return;
                    }
                    cameraScript.DifferentRoomChange(2);
                }
                
                }
                if (hitinfo.collider.name == "PlanePurple")
                {
                    if (cameraScript.activeCameraIndex == 3)
                    {
                        cameraScript.DifferentRoomChange(4);
                        return;
                    }
                cameraScript.DifferentRoomChange(3);
                }
              
            }
        }
        
        
    }

