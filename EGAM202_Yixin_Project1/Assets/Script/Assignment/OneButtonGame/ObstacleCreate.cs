using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCreate : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject location;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            index = Random.Range(0, obstacles.Length);
            Instantiate(obstacles[index],location.transform.position,Quaternion.identity);
        }
    }

}
