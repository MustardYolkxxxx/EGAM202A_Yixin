using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AgentColor : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool isStop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isStop)
        {
            agent.isStopped= true;
        }
    }
}
