using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class FishingDirectIndicator : MonoBehaviour
{
    public GameObject hookOriginPos;
    public GameObject hook;
    public GameObject fishCreatePos;
    public GameObject currentHook;
    public GameObject currentFish;

    public GameObject[] fish;


    public CinemachineVirtualCamera fishCamera;
    public CinemachineVirtualCamera catchFishCamera;
    public CinemachineVirtualCamera idleCamera;

    /*Change Direction*/
    public float originAngle;
    public float thiAngle = 0;
    public float rotateSpeed = 1;
    public float angleTrans;
    public float limitAngle = 45;    //the indicator can change during -45 - 45 degree

    /*Change Power*/
    public float currentPower;    //choose a cast power
    public float maxPower;          //the max cast power
    public float minPower;
    public float powerUpSpeed;

    /*Waiting Fish*/
    public float fishShowRandomTime;

    /*Reeling Back*/
    public float holdLineIndicator;
    public float holdMin;
    public float holdMax;
    public float holdSpeed;
    public float holdLeftThreshold;
    public float holdRightThreshold;

    public float reelPowerMax;
    public float reelPowerMin;
    public float currentReelPower;
    public float powerDownSpeed;
    public float reelSpeed;

    public float checkCollideTime;
    public float checkCollideDuration;

    public float holdLineRandomMove;
    private float holdLineRandomTime;

    public int count;
    public int collideCount;
    public int fishSize;
    public int fishIndex;
    public bool reelBack;

    public bool directionChoose;
    public bool hookSet;
    public enum indicatorDirect
    {
        turnRight,
        turnLeft,
    }
    public PowerState currentPowerState;
    public enum PowerState
    {
        poweron,
        powerdown,
    }

    public enum FishingState
    {
        idle,
        chooseDirect,
        choosePower,
        waitingFish,
        reelLine,
        bite,
        fishShowUp,
        fishGo,
        reel,        
        catchFish,       
        question,
    }
    public FishingState currentFishingState;
    public indicatorDirect direction;
    // Start is called before the first frame update
    void Start()
    {
        angleTrans = Mathf.Deg2Rad;
        originAngle = transform.rotation.eulerAngles.y;
        holdLeftThreshold = Random.Range(1.5f, holdMax-1.5f);
        //holdRightThreshold = Random.Range(0, holdMax / 2);
        holdRightThreshold = holdLeftThreshold;
        StartCoroutine(ReelBarAutoMove());
    }

    // Update is called once per frame
    void Update()
    {
        if(currentFishingState == FishingState.idle)
        {
           if( Input.GetKeyDown(KeyCode.Space))             
           {
                StartCoroutine(ControlSequence());
           }
        }
        if (currentFishingState == FishingState.chooseDirect)
        {
            CheckDirection();
            PointerRotate();
            StopDirectIndicator();
            Recast();
        }

        if(currentFishingState == FishingState.choosePower)
        {
            PowerUp();
            StopChoosePower();
            Recast();
        }
        if (currentFishingState == FishingState.waitingFish)
        {
            StopCast();
            Recast();
        }

        if(currentFishingState == FishingState.bite)
        {
            StartCoroutine(WaitingFish());
        }

        if (currentFishingState == FishingState.fishShowUp)
        {
            FishShowUp();
        }

        if (currentFishingState == FishingState.reel)
        {
            ReelingBack();
            ReelingBarChange();
            ReelingBackPress();
            CheckReelPower();
        }
        
        if (currentFishingState == FishingState.question)
        {
            BackToIdle();
        }
        checkCollideTime += Time.deltaTime;
        thiAngle = transform.rotation.eulerAngles.y;
        
        
        //Debug.Log("target"+-45 * angleTrans);
        
    }

    void StopCast()
    {
        if(currentHook.GetComponent<ThrowHook>().savePos)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                StartCoroutine(ReelLine());
            }
        }
        
    }

    IEnumerator ReelLine()
    {
        float time = 0;
        float duration = 1;
        currentFishingState= FishingState.reelLine;
        while (time < duration )
        {
            currentHook.transform.position = Vector3.Lerp(currentHook.GetComponent<ThrowHook>().fallPos,
                currentHook.GetComponent<ThrowHook>().targetPos, time/duration);
            yield return null;
            time += Time.deltaTime;
        }
        StartCoroutine(RestartGame());
    }
    void Recast()
    {
        if(Input.GetKeyDown(KeyCode.Q)) 
        {
            StartCoroutine(RestartGame());
        }
    }
    void ResetValue()
    {
        fishCamera.Priority = 20;
        catchFishCamera.Priority = 10;
        idleCamera.Priority = 10;
        currentPower = minPower;
        thiAngle = originAngle;
        currentReelPower = reelPowerMin;
        //holdLeftThreshold = Random.Range(0, holdMax / 2);
        //holdRightThreshold = Random.Range(0, holdMax / 2);
        holdLeftThreshold = Random.Range(1.5f, holdMax - 1.5f);

        holdRightThreshold = holdLeftThreshold;
        holdLineIndicator = 0;

        collideCount= 0;

        if (currentHook != null)
        {
            Destroy(currentHook);
        }
        if (currentFish != null)
        {
            currentFish = null;
        }
    }
    IEnumerator ControlSequence()
    {
        yield return new WaitForSeconds(0.1f);
        if (currentFishingState == FishingState.choosePower)
        {
            currentFishingState = FishingState.waitingFish;
        }
        if (currentFishingState == FishingState.chooseDirect)
        {
            currentFishingState = FishingState.choosePower;
        }
        

        if (currentFishingState == FishingState.idle)
        {
            currentFishingState = FishingState.chooseDirect;
        }
        
    }
    void CheckDirection()
    {

        float targetAngleLeft = originAngle - limitAngle;
        float targetAngleRight = limitAngle + originAngle;
        if(targetAngleLeft < 0)
        {
            targetAngleLeft = 360 - targetAngleLeft;
        }
        if(targetAngleRight >360)
        {
            targetAngleRight = targetAngleRight - 360;
        }
        if (thiAngle > targetAngleLeft  && thiAngle <= targetAngleLeft+1)
        {
            direction = indicatorDirect.turnRight;
        }

        if (thiAngle >= targetAngleRight-1 && thiAngle < targetAngleRight)
        {
            direction = indicatorDirect.turnLeft;
        }

    }
    void PointerRotate()
    {

        if (direction == indicatorDirect.turnRight)
        {
            Quaternion target = Quaternion.Euler(0, limitAngle + originAngle, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotateSpeed);
        }
        else if (direction == indicatorDirect.turnLeft)
        {
            Quaternion target = Quaternion.Euler(0, originAngle - limitAngle, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotateSpeed);
        }
    }

    void StopDirectIndicator()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ControlSequence());
        }
    }
    void PowerUp()
    {
        if (currentPower < minPower)
        {
            currentPowerState = PowerState.poweron;
        }
        if(currentPower > maxPower) 
        {
            currentPowerState = PowerState.powerdown;
        }
        if(currentPowerState == PowerState.poweron)
        {
            currentPower += Time.deltaTime * powerUpSpeed;
        }
        if (currentPowerState == PowerState.powerdown)
        {
            currentPower -= Time.deltaTime * powerUpSpeed;
        }
    }
    void StopChoosePower()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetHookPos();
            //StartCoroutine(WaitingFish());
            StartCoroutine(ControlSequence());
        }
    }
    public void SetFish(GameObject fish)
    {
        currentFish = fish;
    }
    IEnumerator WaitingFish()
    {
        //fishShowRandomTime = Random.Range(2f, 4f);
        //yield return new WaitForSeconds(fishShowRandomTime);
        //currentFishingState = FishingState.fishShowUp;
        //float waitTime = 2f;
        //StartCoroutine(HookDown());
        //yield return new WaitForSeconds(waitTime);
        //StartCoroutine(currentFish.GetComponent<ThrowHook>().WaterEffectControl());
        yield return HookDown();
        if (currentFishingState == FishingState.fishShowUp)
        {
            StartCoroutine(QuitToIdle());
            currentFishingState = FishingState.fishGo;
        }
    }
    IEnumerator HookDown()
    {
        float duration = 2f;
        float currentTime=0;
        ThrowHook hook = FindObjectOfType<ThrowHook>();
        Vector3 hookPos = hook.transform.position;
        currentFishingState = FishingState.fishShowUp;
        currentHook.GetComponent<ThrowHook>().InstantiateSplash();
        while (currentTime < duration&& currentFishingState == FishingState.fishShowUp)
        {
            hook.transform.position = hookPos - new Vector3(0,0.03f,0);
            
            yield return null;
            currentTime += Time.deltaTime;
        }
        hook.transform.position = hookPos + new Vector3(0, 0.03f, 0);

    }
    void FishShowUp()
    {
        if(currentFishingState == FishingState.fishShowUp)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(IfReelingOn());
                StartCoroutine(ReelingBarChangeDirect());
                currentFishingState = FishingState.reel;          /*change to the reel state*/
            }
        }
    }

    IEnumerator IfReelingOn()
    {
        yield return new WaitForSeconds(10);           //time that players can trying to reel
        if(currentFishingState == FishingState.reel)
        {
            StartCoroutine(QuitToIdle());
            currentFishingState = FishingState.fishGo;
        }
        
    }
    void SetHookPos()
    {
        Vector3 pos = hookOriginPos.transform.position + hookOriginPos.transform.forward * currentPower;
        GameObject fishHook = Instantiate(hook, hookOriginPos.transform.position, Quaternion.identity);
        currentHook = fishHook;
        hookSet = true;
    }

    void TurnIntoReelingBack()
    {

    }

    IEnumerator ReelBarAutoMove()
    {
        float time = 0;
        float duration = 1;
        count++;
        while (time < duration&&count%2 == 0)
        {
            holdLeftThreshold += 0.005f;
            holdRightThreshold = holdLeftThreshold;
            yield return null;
            time += Time.deltaTime;
        }
        while (time < duration && count % 2 == 1)
        {
            holdLeftThreshold -= 0.005f;
            holdRightThreshold = holdLeftThreshold;
            yield return null;
            time += Time.deltaTime;
        }
        StartCoroutine(ReelBarAutoMove());
    }
    IEnumerator ReelingBarChangeDirect()   //a ienumerator to determine the hold bar moving direction and speed
    {
        float randomTime = Random.Range(1, 2);
        holdLineRandomMove = Random.Range(-5, 5);
        if (holdLineRandomMove == 0)
        {
            holdLineRandomMove = Random.Range(-5, 5);
        }
        yield return new WaitForSeconds(randomTime);
        
            StartCoroutine(ReelingBarChangeDirect()); 
    }
    void ReelingBarChange()    // change the hold indicator in random direction and speed, and indicate the reel power bar
    { 
        if(holdLineIndicator < holdMin)
        {
            holdLineIndicator = holdMin;
        }
        if(holdLineIndicator > holdMax)
        {
            holdLineIndicator = holdMax;
        }
        holdLineIndicator += Time.deltaTime* holdLineRandomMove;
        currentReelPower -=Time.deltaTime*powerDownSpeed;
    }
    void ReelingBack()     //input arrow key down and control the hold indicator in a particular area
    {
        float x = Input.GetAxis("Horizontal");//对应键盘上的A键和D键 或←键和→键
        holdLineIndicator += x * holdSpeed/20;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //    holdLineIndicator -= holdSpeed;
            currentHook.GetComponent<ThrowHook>().InstantiateSplash();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //    holdLineIndicator += holdSpeed;
            currentHook.GetComponent<ThrowHook>().InstantiateSplash();
        }

        if (holdLineIndicator < holdMax - holdRightThreshold && holdLineIndicator > holdMin + holdLeftThreshold)   //determine the area
        {
            reelBack= true;
        }
        else
        {
            reelBack= false;
        }
        if(holdLineIndicator<holdMin+0.3||holdLineIndicator > holdMax - 0.3)
        {
            if(checkCollideTime>checkCollideDuration)
            {
                StartCoroutine(CheckReelBarCollide());
                
            }
            
        }
    }

    IEnumerator CheckReelBarCollide()
    {
        checkCollideTime = 0;
        collideCount++;
        if (collideCount == 2)
        {
            StartCoroutine(QuitToIdle());
            currentFishingState = FishingState.fishGo;
        }
        yield return new WaitForSeconds(1);

    }

    void ReelingBackPress()   // press space key to let the reel power up
    {
        if (reelBack)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentReelPower += reelSpeed;
                currentHook.GetComponent<ThrowHook>().InstantiateSplash();
            }
        }
    }

    void CheckReelPower()
    {
        if (currentReelPower < reelPowerMin)
        {
            currentReelPower = reelPowerMin;
        }
            if (currentReelPower>reelPowerMax)
        {
            StartCoroutine(CatchFish());
            currentFishingState = FishingState.catchFish;
        }
    }
    IEnumerator RandomFish()
    {

        fishIndex = currentFish.GetComponent<FishIndex>().index;
        fishSize = Random.Range(0, 3);
        yield return null;
        GameObject thisfish = Instantiate(fish[fishIndex], fishCreatePos.transform.position, Quaternion.Euler(0, 0, 90));
        yield return new WaitForSeconds(2);
        Destroy(currentFish);
        Destroy(thisfish);
    }
    IEnumerator CatchFish()
    {
        
        fishCamera.Priority = 10;
        catchFishCamera.Priority = 20;
        idleCamera.Priority = 10;
        //StartCoroutine(RandomFish());

        yield return RandomFish();
        StartCoroutine(QuestionTime());
    }

    IEnumerator QuestionTime()
    {
        currentFishingState= FishingState.question;
        fishCamera.Priority = 10;
        catchFishCamera.Priority = 10;
        idleCamera.Priority = 20;
        yield return new WaitForSeconds(1);
        //StartCoroutine(QuitToIdle());
    }
    void BackToIdle()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RestartGame());
        }
    }
    IEnumerator QuitToIdle()
    {
        
        yield return new WaitForSeconds(2);
        currentFishingState = FishingState.idle;
        ResetValue();
        
    }

    IEnumerator RestartGame()
    {
        
        yield return null;
        currentFishingState = FishingState.idle;
        ResetValue();
        
    }
    void GameEnd()
    {

    }
}
