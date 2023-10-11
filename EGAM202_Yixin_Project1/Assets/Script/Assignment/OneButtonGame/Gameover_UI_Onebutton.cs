using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Gameover_UI_Onebutton : MonoBehaviour
{
    public GameObject gameoverText;
    public GameObject gamewin;
    public GameOverCheck gameoverScr;
    public Win_UI_OneButton winScr;
    // Start is called before the first frame update
    void Start()
    {
        gameoverScr = FindObjectOfType<GameOverCheck>();
        winScr = FindObjectOfType<Win_UI_OneButton>();
    }

    // Update is called once per frame
    void Update()
    {
        gameoverText.SetActive(gameoverScr.gameOver);
        gamewin.SetActive(winScr.isWin);
    }
}
