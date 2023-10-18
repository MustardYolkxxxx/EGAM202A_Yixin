using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreate : MonoBehaviour
{
    public GameObject ground;
    public GameObject location;
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
            Instantiate(ground,location.transform.position,Quaternion.identity);
        }
    }

}
