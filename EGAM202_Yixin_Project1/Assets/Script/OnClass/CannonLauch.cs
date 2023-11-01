using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonLauch : MonoBehaviour
{
    public Transform spawnHandle;
    public CannonBall cannonBallPre;

    public float force;
    public float frequence;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LauchBall());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LauchBall()
    {
        while (true)
        {
            yield return new WaitForSeconds(frequence); 
            CannonBall ball = Instantiate(cannonBallPre);
            ball.transform.position= spawnHandle.position;
            ball.Luach(spawnHandle.up, force);
        }
    }
}
