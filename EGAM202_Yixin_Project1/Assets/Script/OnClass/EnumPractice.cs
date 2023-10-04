using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnumPractice : MonoBehaviour
{

    public GameObject target;
    public GameObject pikmanSizeLarge;
    public GameObject pikmanSizeSmall;
    public GameObject pikmanSizeMedium;
    public enum PikmanColor
    {
        Red,
        Blue,
        Yellow,
    }

    public enum PikmanSize
    {
        Small,
        Medium,
        Large,
    }

    public PikmanColor currentColor;
    public PikmanSize currentSize;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentSize)
        {
            case PikmanSize.Large:
                currentSize = PikmanSize.Large;
                ChangeSize(); 
                break;
            case PikmanSize.Small:
                currentSize = PikmanSize.Small;
                ChangeSize();
                break;
            case PikmanSize.Medium:
                currentSize = PikmanSize.Medium;
                ChangeSize();
                break;
            default: 
                break;
        }

        switch(currentColor)
        {
            case PikmanColor.Red:
                ChangeColor(PikmanColor.Red); break;
            case PikmanColor.Blue:
                ChangeColor(PikmanColor.Blue);
                break;
            case PikmanColor.Yellow:
                ChangeColor(PikmanColor.Yellow);
                break;
            default: 
                break;
        }
    }

    public void ChangeSize()
    {

            pikmanSizeSmall.SetActive(currentSize == PikmanSize.Small);
            pikmanSizeLarge.SetActive(currentSize == PikmanSize.Large);
            pikmanSizeMedium.SetActive(currentSize == PikmanSize.Medium);

    }

    public void ChangeColor(PikmanColor color)
    {
        if(color == PikmanColor.Red)
        {
            pikmanSizeSmall.gameObject.GetComponent<Renderer>().material.color = Color.red;
            pikmanSizeLarge.gameObject.GetComponent<Renderer>().material.color = Color.red;
            pikmanSizeMedium.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else if(color == PikmanColor.Blue)
        {
            pikmanSizeSmall.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            pikmanSizeLarge.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            pikmanSizeMedium.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
       else if(color == PikmanColor.Yellow)
        {
            pikmanSizeSmall.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            pikmanSizeLarge.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            pikmanSizeMedium.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }
}
