using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CChangeCameraTarget_OneButton : MonoBehaviour
{

    public GameObject targetLeft;
    public GameObject targetRight;
    public SkateBoardJump skateScr;
    public CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(skateScr.currentDirection == SkateBoardJump.DirectionState.Left)
        {
            virtualCamera.LookAt=targetLeft.transform;
            virtualCamera.Follow=targetLeft.transform;
        }

        if (skateScr.currentDirection == SkateBoardJump.DirectionState.Right)
        {
            virtualCamera.LookAt = targetRight.transform;
            virtualCamera.Follow = targetRight.transform;
        }
    }
}
