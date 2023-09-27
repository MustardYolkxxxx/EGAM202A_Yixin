using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{

    public Renderer renderer1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickChangeColor()
    {
        if(renderer1.material.color == new Color(1, 1, 0, 1))
        {
            renderer1.material.color = Color.red;
        }
        else
        {
            renderer1.material.color = new Color(1, 1, 0, 1);
        }
    }
}
