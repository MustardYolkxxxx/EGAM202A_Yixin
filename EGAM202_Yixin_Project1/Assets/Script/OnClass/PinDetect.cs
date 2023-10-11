using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinDetect : MonoBehaviour
{
    public Camera screenCamera;
    public float radius;
    public List<PinScr> pins;
    public Ray rayline;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            Ray mouseClickRay = screenCamera.ScreenPointToRay(mousePosition);
            rayline = mouseClickRay;
            RaycastHit[] hit = Physics.SphereCastAll(mouseClickRay, radius);
            foreach (RaycastHit c in hit)
            {
                PinScr pinScr = c.transform.GetComponent<PinScr>();
                if (pinScr)
                {
                    pinScr.DestroyMe();
                }
            }
        }
        

    }

    void OnDrawGizmos()
    { 
        Gizmos.DrawCube(rayline.origin,rayline.direction); 
    }
}
