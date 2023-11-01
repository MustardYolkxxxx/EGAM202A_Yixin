using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishingUIReelBalance : MonoBehaviour
{
    public Scrollbar holdBalance;
    public GameObject startGameUI;
    public GameObject castPowerUI;
    public GameObject directionHintUI;
    public GameObject powerHintUI;
    public GameObject waitUI;
    public GameObject holdUI;
    public GameObject confirmUI;
    public GameObject reelPowerUI;
    public GameObject fishShowUpUI;
    public GameObject fishGoUI;
    public GameObject catchFish;
    public GameObject balanceHandle;
    public GameObject pullButton;
    public GameObject[] fishSizeUI;
    public GameObject[] collideTimeUI;
    public GameObject fishSizeUIGroup;
    public GameObject bubbleUI;
    public GameObject bubble2UI;
    public GameObject recastUI;

    public Image castPower;
    public Image powerLevel;
    public Image leftReelThreshold;
    public Image rightReelThreshold;
    public FishingDirectIndicator fishingScr;

    public TextMeshProUGUI caughtText;

    public Vector3 handlePos;
    // Start is called before the first frame update
    void Start()
    {
       handlePos = balanceHandle.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        startGameUI.SetActive(fishingScr.currentFishingState == FishingDirectIndicator.FishingState.idle);
        castPowerUI.SetActive(fishingScr.currentFishingState == FishingDirectIndicator.FishingState.choosePower);
        //powerHintUI.SetActive(fishingScr.currentFishingState == FishingDirectIndicator.FishingState.choosePower);
        directionHintUI.SetActive(fishingScr.currentFishingState == FishingDirectIndicator.FishingState.chooseDirect||
            fishingScr.currentFishingState == FishingDirectIndicator.FishingState.choosePower);
        confirmUI.SetActive(fishingScr.currentFishingState == FishingDirectIndicator.FishingState.chooseDirect ||
            fishingScr.currentFishingState == FishingDirectIndicator.FishingState.choosePower);
        waitUI.SetActive(fishingScr.currentFishingState == FishingDirectIndicator.FishingState.waitingFish);
        recastUI.SetActive(fishingScr.currentFishingState == FishingDirectIndicator.FishingState.waitingFish);
        reelPowerUI.SetActive(fishingScr.currentFishingState == FishingDirectIndicator.FishingState.reel);

        pullButton.SetActive(fishingScr.reelBack);

        holdUI.SetActive(fishingScr.currentFishingState == FishingDirectIndicator.FishingState.reel);
        fishShowUpUI.SetActive(fishingScr.currentFishingState == FishingDirectIndicator.FishingState.fishShowUp);
        fishGoUI.SetActive(fishingScr.currentFishingState == FishingDirectIndicator.FishingState.fishGo);
        //catchFish.SetActive(fishingScr.currentFishingState == FishingDirectIndicator.FishingState.catchFish);
        fishSizeUIGroup.SetActive(fishingScr.currentFishingState == FishingDirectIndicator.FishingState.catchFish);
        bubbleUI.SetActive(fishingScr.currentFishingState == FishingDirectIndicator.FishingState.catchFish);
        bubble2UI.SetActive(fishingScr.currentFishingState == FishingDirectIndicator.FishingState.question);

        holdBalance.value = (fishingScr.holdLineIndicator - fishingScr.holdMin)/(fishingScr.holdMax-fishingScr.holdMin);

        powerLevel.fillAmount = (fishingScr.currentReelPower-fishingScr.reelPowerMin)/(fishingScr.reelPowerMax-fishingScr.reelPowerMin);

        leftReelThreshold.fillAmount = fishingScr.holdLeftThreshold / (fishingScr.holdMax - fishingScr.holdMin);

        rightReelThreshold.fillAmount = fishingScr.holdRightThreshold / (fishingScr.holdMax - fishingScr.holdMin);

        castPower.fillAmount = (fishingScr.currentPower-fishingScr.minPower) / (fishingScr.maxPower-fishingScr.minPower);

        if(Input.GetKeyDown(KeyCode.Space)&& fishingScr.currentFishingState == FishingDirectIndicator.FishingState.reel)
        {
            balanceHandle.transform.position += new Vector3(0, 10, 0);
        }
        if (Input.GetKeyUp(KeyCode.Space) && fishingScr.currentFishingState == FishingDirectIndicator.FishingState.reel)
        {
            balanceHandle.transform.position -= new Vector3(0, 10, 0);
        }
        
        if (fishingScr.currentFishingState == FishingDirectIndicator.FishingState.catchFish)
        {
            for(int i =0; i<fishSizeUI.Length; i++)
            {
                if(i == fishingScr.fishSize)
                {
                    fishSizeUI[i].SetActive(true);
                }
                else
                {
                    fishSizeUI[i].SetActive(false);
                }
            }
        }

        collideTimeUI[0].SetActive(fishingScr.collideCount == 2);
        collideTimeUI[1].SetActive(fishingScr.collideCount == 1);
        collideTimeUI[2].SetActive(fishingScr.collideCount == 0);

        caughtText.text = "You caught the" + fishingScr.fish[fishingScr.fishIndex].name + "!";
    }
}
