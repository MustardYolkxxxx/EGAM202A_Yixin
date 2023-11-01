using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBite : MonoBehaviour
{
    public ThrowHook targetObj;
    public FishingDirectIndicator fishingScr;
    public FishMove fishMoveScr;
    public GameObject fish;
    public bool isFished;

    public float rotateSpeed;
    public float moveSpeed;
    public int count;
    
    // Start is called before the first frame update
    void Start()
    {
        fishingScr = FindObjectOfType<FishingDirectIndicator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(targetObj!= null)
        {
            if (fishMoveScr.currentFishState == FishMove.FishState.move && count == 0 && fishingScr.currentFish == null)
            {
                targetObj.CheckFishIsBite();
                fishingScr.SetFish(fish);
                count++;
            }
        }
        if(fishingScr.currentFish!= null&&fishingScr.currentFish!=fish) 
        {
            fishMoveScr.ResetThis();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hook"))
        {           
            targetObj = other.GetComponent<ThrowHook>();

            
        }
    }
}
