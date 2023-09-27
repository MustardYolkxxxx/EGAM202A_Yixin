using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Camera gameCamera;
    public float speed;
    public float mouseMoveRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCameraByKey();
        //MoveCameraByMouse();
    }

    void MoveCameraByKey()
    {
        if(Input.GetKey(KeyCode.W)) 
        {
            transform.Translate(new Vector3(-1,0,0)* Time.deltaTime*speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed);
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
