using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Luach(Vector3 direction, float force)
    {
        rb.AddForce(direction*force,ForceMode.Impulse);
    }
}
