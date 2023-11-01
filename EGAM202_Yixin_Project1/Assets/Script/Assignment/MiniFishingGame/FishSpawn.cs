using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawn : MonoBehaviour
{
    public GameObject spawnFish;
    public GameObject currentFish;
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentFish==null&&count==0)
        {
            StartCoroutine(InsFish());
            count++;
        }
    }

    IEnumerator InsFish()
    {
        yield return new WaitForSeconds(20);
        currentFish = Instantiate(spawnFish,transform.position,Quaternion.identity);
        count = 0;
    }
}
