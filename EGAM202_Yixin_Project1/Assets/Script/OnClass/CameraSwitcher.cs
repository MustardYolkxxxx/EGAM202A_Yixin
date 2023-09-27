using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cameralist;
    public int activeCameraIndex;
    public int index;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //ClickToChangeCamera();
    }
    public void DifferentRoomChange(int activeIndex)
    {
        for (int i = 0; i < cameralist.Count; i++)
        {
            cameralist[i].Priority = 0;
        }
        int c = activeIndex;
        cameralist[c].Priority = 100;
        activeCameraIndex = activeIndex;
    }

    void ClickToChangeCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            activeCameraIndex++;
            if (activeCameraIndex >= cameralist.Count)
            {
                activeCameraIndex = 0;
            }
            for (int i = 0; i < cameralist.Count; i++)
            {
                int newPriority = 0;
                if (i == activeCameraIndex)
                {
                    newPriority = 100;

                }
                cameralist[i].Priority = newPriority;
            }
        }
    }
}
