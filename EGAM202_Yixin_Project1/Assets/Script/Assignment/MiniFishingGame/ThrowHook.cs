using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ThrowHook : MonoBehaviour
{
    public FishingDirectIndicator fishingScr;
    public Rigidbody rb;

    public float verticalSpeed;
    public float verticalPos;
    public float horizontalSpeed;
    public float gravity;
    public float shockSpeed;
    public float directionX;
    public float directionZ;

    public float control;
    public Vector3 direction;
    public Vector3 originPos;
    public Vector3 targetPos;
    public Vector3 fallPos;

    public GameObject targetObj;
    public GameObject waterEffect;
    public GameObject splash;

    public bool up;
    public bool isBite;
    public bool savePos;
    public enum HookState
    {
        thrown,
        inWater,
    }

    public HookState currentState;
    // Start is called before the first frame update
    void Start()
    {
        targetObj = GameObject.Find("hookTargetPos");
        fishingScr = FindObjectOfType<FishingDirectIndicator>();
        //rb = GetComponent<Rigidbody>();
        originPos = new Vector3 (fishingScr.transform.position.x,0, fishingScr.transform.position.z);
        targetPos = targetObj.transform.position;
        StartCoroutine(HookShock());
        StartCoroutine(SavePosInfo());
        StartCoroutine(DelayShowEffect());
    }

    // Update is called once per frame
    void Update()
    {
        verticalSpeed -= gravity*Time.deltaTime;
        if(currentState == HookState.thrown)
        {
            if (transform.position.y > 0)
            {
                transform.position += fishingScr.transform.right * (fishingScr.currentPower / 10) * Time.deltaTime;
                transform.position += fishingScr.transform.up * verticalSpeed * Time.deltaTime;
            }
        }
       
        if(transform.position.y < 0)
        {
            currentState = HookState.inWater;
        }

        
        if (fishingScr.currentFishingState == FishingDirectIndicator.FishingState.reel)
        {
            if(up)
            {
                verticalPos +=  Time.deltaTime*shockSpeed;
            }
            else
            {
                verticalPos -= Time.deltaTime * shockSpeed;
            }
            //transform.Translate(fishingScr.holdLineIndicator / 500, 0, 0);
            //transform.position = originPos + direction * ((fishingScr.reelPowerMax-fishingScr.currentReelPower) / fishingScr.reelPowerMax);
            //Vector3 fishPos = new Vector3(transform.position.x /*+ fishingScr.holdLineIndicator / 500*/, 0, fallPos.z);
            Vector3 fishPos = new Vector3(fallPos.x + fishingScr.holdLineIndicator / 5, 0, fallPos.z);
            transform.position = Vector3.Lerp(fishPos, targetPos, fishingScr.currentReelPower / fishingScr.reelPowerMax);
            transform.position += new Vector3(0, verticalPos, 0);
        }

        //rb.AddForce((fishingScr.transform.right+new Vector3(0,1,0)) * fishingScr.currentPower);
        if (fishingScr.currentFishingState == FishingDirectIndicator.FishingState.fishGo||
            fishingScr.currentFishingState == FishingDirectIndicator.FishingState.catchFish)
        {
            Destroy(gameObject);
        }

        //splash.SetActive(fishingScr.currentFishingState == FishingDirectIndicator.FishingState.reel);
    }
    IEnumerator SavePosInfo()
    {
        yield return new WaitForSeconds(1.5f);

        directionX = transform.position.x - fishingScr.transform.position.x;
        directionZ = transform.position.z - fishingScr.transform.position.z;
        direction = new Vector3(directionX, 0, directionZ);
        verticalPos = transform.position.y;
        fallPos= new Vector3 (transform.position.x,0, transform.position.z);
        savePos = true;
        
    }
    public IEnumerator DelayShowEffect()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(WaterEffectControl());
    }
    IEnumerator HookShock()
    {
        if (up)
        {
            up= false;
        }

        yield return null;

        if(!up)
        {
            up = true;
        }

        yield return null;
        StartCoroutine(HookShock());
    } 

    public IEnumerator WaterEffectControl()
    {
        float randomTime = Random.Range(0.05f, 0.15f);
        GameObject ripples = Instantiate(waterEffect, gameObject.transform.position,Quaternion.Euler(90,0,0));
        yield return new WaitForSeconds(randomTime);
        StartCoroutine(WaterEffectControl());
    }

    public void InstantiateSplash()
    {
        Instantiate(splash,gameObject.transform.position,Quaternion.Euler(-90, 0, 0));
    }

    public void CheckFishIsBite()
    {
        StartCoroutine(ChangeStateToBite());
    }

    IEnumerator ChangeStateToBite()
    {
        yield return null;
        fishingScr.currentFishingState = FishingDirectIndicator.FishingState.bite;
    }
}
