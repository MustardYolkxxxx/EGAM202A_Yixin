using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedIndicate_UI : MonoBehaviour
{
    public GameOverCheck gameOverScr;
    public SkateBoardJump skateScr;
    public TextMeshProUGUI speedText;
   
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(CalcuSpeed(1));
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = skateScr.curSpeed.ToString("0");
    }

    IEnumerator CalcuSpeed(int i)
    {
        speedText.text = skateScr.curSpeed.ToString("0");
        yield return new WaitForSeconds(i);
        StartCoroutine(CalcuSpeed(i));
    }
}
