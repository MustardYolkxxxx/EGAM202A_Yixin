using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerIndicate_UI : MonoBehaviour
{
    public GameOverCheck gameOverScr;
    public SkateBoardJump skateScr;
    public Image powerImg;
    public Image threshold;
    private float power;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       powerImg.fillAmount =Mathf.Abs((skateScr.slideForce-skateScr.slideForceMin))/ Mathf.Abs((skateScr.slideForceMax-skateScr.slideForceMin));
    }
}
