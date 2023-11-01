using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedMyself : MonoBehaviour
{
    public float time;
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject,time);
    }
}
