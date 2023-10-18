using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AddScore : MonoBehaviour
{
    public GameOverCheck gameOverScr;
    public SkateBoardJump skateScr;
    public TextMeshProUGUI scoreText;
    private float score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOverScr.gameOver)
        {
            if (!skateScr.canJump)
            {
                score += Time.deltaTime*100;
            }
        }
        scoreText.text = score.ToString("0");
    }
}
