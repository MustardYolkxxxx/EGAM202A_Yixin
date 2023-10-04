using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCount : MonoBehaviour
{
    public GameObject bridge1;
    public GameObject bridge2;  
    public GameObject bridge3;
    public EndPoint endPointScr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bridge1.SetActive(endPointScr.scorePoint >= 1);
        bridge2.SetActive(endPointScr.scorePoint >= 2);
        bridge3.SetActive(endPointScr.scorePoint >= 3);
    }
}
