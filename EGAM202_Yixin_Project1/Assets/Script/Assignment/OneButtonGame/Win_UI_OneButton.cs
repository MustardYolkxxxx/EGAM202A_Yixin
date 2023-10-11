using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_UI_OneButton : MonoBehaviour
{
    public bool isWin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            isWin = true;
        }
        
    }
}
