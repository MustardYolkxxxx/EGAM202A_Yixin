using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public int scorePoint;
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
        if (other.CompareTag("Treasure"))
        {
            print("trigger");
            scorePoint++;
            other.GetComponent<Treasure>().ClearCarryObject();
            //Destroy(other.gameObject);
        }
    }
}
