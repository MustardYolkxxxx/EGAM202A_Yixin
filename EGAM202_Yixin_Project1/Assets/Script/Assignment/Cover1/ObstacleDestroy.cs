using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroy : MonoBehaviour
{
    public EndPoint endPointScr;
    public int targetPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (endPointScr.scorePoint >= targetPoint)
        {
            Destroy(gameObject); 
        }
    }
}
