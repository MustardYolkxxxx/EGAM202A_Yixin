using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_TreasureNumber : MonoBehaviour
{
    public EndPoint endPointScr;
    public GameObject winGame;
    public TextMeshProUGUI treasureNumberText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        treasureNumberText.text = endPointScr.scorePoint.ToString();
        winGame.SetActive(endPointScr.scorePoint>=4);
    }
}
