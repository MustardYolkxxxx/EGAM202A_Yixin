using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountNumber : MonoBehaviour
{
    public TextMeshProUGUI textCountNumber;
    
    public Treasure treasureScr;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        textCountNumber.text = (treasureScr.height - treasureScr.currentCarryNumber).ToString();
        
    }
}
