using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FishingDirectIndicator;


public class FishMove : MonoBehaviour
{
    public ThrowHook targetObj;
    public GameObject fish;
    public FishingDirectIndicator fishingScr;
    public FishBite fishBiteScr;

    public float randomTime;
    public float randomTimeMax;
    public float randomTimeMin;
    public bool isFished;

    public float rotateSpeed;
    public float moveSpeed;

    public Vector3 originPos;

    public enum FishState
    {
        idle,
        look,
        move,
    }
    public FishState currentFishState;
    // Start is called before the first frame update
    void Start()
    {
        fishingScr = FindObjectOfType<FishingDirectIndicator>();
        originPos= fish.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(fishingScr.currentFishingState!=FishingState.reelLine) 
        {
            if (targetObj != null)
            {
                LookToTarget();
                FishMovement();
            }
        }

        if(targetObj!= null)
        {
            
        }
        else
        {
            ResetThis();
        }
    }

    void LookToTarget()
    {
        if(targetObj != null)
        {
            if(isFished)
            {
                Vector3 pos = new Vector3(targetObj.transform.position.x, -0.05f, targetObj.transform.position.z);
                //fish.transform.forward = new Vector3(fish.transform.position.x - targetObj.transform.position.x, 0,
                //    fish.transform.position.y - targetObj.transform.position.y);

                //float angle = Vector3.Angle(fish.transform.forward,pos);
                //Quaternion target = Quaternion.Euler(0, angle, 0);

                Vector3 velocity = Quaternion.Inverse(fish.transform.rotation) * targetObj.transform.position;
                float angle = Mathf.Atan2(velocity.x,velocity.z)*Mathf.Rad2Deg;
                Debug.Log(angle);
                Quaternion target = Quaternion.Euler(0, angle, 0);
                //fish.transform.LookAt(pos);
                fish.transform.rotation = Quaternion.RotateTowards(fish.transform.rotation, target, rotateSpeed);
            }          
        }
    }
    void FishMovement()
    {
        Vector3 pos= new Vector3(targetObj.transform.position.x,-0.05f,targetObj.transform.position.z);
        if(currentFishState== FishState.move)
        {
            fish.transform.position = Vector3.Lerp(fish.transform.position, pos, Time.deltaTime*moveSpeed);
        }
    }
   IEnumerator DelayTime()
    {
        randomTime = Random.Range(randomTimeMin, randomTimeMax);
        yield return new WaitForSeconds(randomTime);
        isFished = true;
        yield return new WaitForSeconds(1);
        currentFishState= FishState.move;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(fishingScr.currentFishingState == FishingState.waitingFish)
        //{
            if (other.CompareTag("Hook"))
            {
                targetObj = other.GetComponent<ThrowHook>();
                
                StartCoroutine(DelayTime());
            }
        }
        
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hook"))
        {
            targetObj = other.GetComponent<ThrowHook>();
            
        }
    }

   public void ResetThis()
    {
        currentFishState = FishState.idle;
        //float angle = Vector3.Angle(fish.transform.forward, originPos);
        //Quaternion target = Quaternion.Euler(0, angle, 0);
        ////fish.transform.LookAt(pos);
        //fish.transform.rotation = Quaternion.RotateTowards(fish.transform.rotation, target, rotateSpeed);
        fish.transform.position = Vector3.Lerp(fish.transform.position, originPos, Time.deltaTime * moveSpeed);
        isFished= false;
        fishBiteScr.count= 0;
    }
}
