using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Camera gameCamera;
    public GameObject clickIndicator;
    public MoveCamera moveCameraScr;
    // Start is called before the first frame update
    void Start()
    {
        moveCameraScr = FindObjectOfType<MoveCamera>();
    }

    // Update is called once per frame
    void Update()
    {
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

                    Instantiate(clickIndicator, hitinfo.point, Quaternion.identity);
                }
            }
        }
    }
}