using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TimeBar : MonoBehaviour
{
    public GameOverCheck gameOverScr;
    public SkateBoardJump skateScr;
    public Image timeImg;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeImg.fillAmount = (2 - skateScr.deadTime) / 2;
    }
}
