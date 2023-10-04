using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class MoveCamera : MonoBehaviour
{
    public Camera gameCamera;
    public float smooth;
    public float speed;
    public float mouseMoveRange;
    public float clickTime;
    public float fieldView;
    public Vector3 targetMovePos;
    public GameObject focusObject;
    public CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        fieldView = virtualCamera.m_Lens.FieldOfView;
    }

    // Update is called once per frame
    void Update()
    {

        fieldView -= Input.GetAxis("Mouse ScrollWheel")*10;
        fieldView = Mathf.Clamp(fieldView, 23f, 55);
        virtualCamera.m_Lens.FieldOfView = fieldView;


        if (Input.GetMouseButtonDown(0))
        {

            //if (CheckGuiRaycastObjects()) return;
            Vector2 mousePosition = Input.mousePosition;
            Ray mouseClickRay = gameCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(mouseClickRay, out RaycastHit hitinfo, 1000))
            {
                if (hitinfo.collider.CompareTag("Ground"))
                {
                    targetMovePos = hitinfo.transform.position;
                    clickTime = 0;
                }

                //focusObject.transform.position = hitinfo.transform.position;
               
            }
        }
        if (clickTime < 1)
        {
            clickTime += Time.deltaTime;
            focusObject.transform.position = Vector3.Lerp(focusObject.transform.position, targetMovePos, smooth * Time.deltaTime);
        }

        MoveCameraByKey();
        //MoveCameraByMouse();
    }

    void MoveCameraByKey()
    {
        if(Input.GetKey(KeyCode.W)) 
        {
            focusObject.transform.Translate(new Vector3(-1,0,0)* Time.deltaTime*speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            focusObject.transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            focusObject.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            focusObject.transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed);
        }
    }

    void MoveCameraByMouse()
    {
        Debug.Log(Input.mousePosition);
        if(Input.mousePosition.x < mouseMoveRange) 
        {
            transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * speed);
        }
        if (Input.mousePosition.x > 1920- mouseMoveRange)
        {
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
        }
        if (Input.mousePosition.y < mouseMoveRange)
        {
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed);
        }
        if (Input.mousePosition.y > 1080- mouseMoveRange)
        {
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * speed);
        }
    }


}
