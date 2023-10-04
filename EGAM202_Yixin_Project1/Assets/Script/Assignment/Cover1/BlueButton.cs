using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueButton : MonoBehaviour
{
    public GameObject floor;
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
        if(other.GetComponent<CharacterState>().currentColor == CharacterState.Colors.Blue)
        {
            floor.SetActive(true);
        }
    }
}
