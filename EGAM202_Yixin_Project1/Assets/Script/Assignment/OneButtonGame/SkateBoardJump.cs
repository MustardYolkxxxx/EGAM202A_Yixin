using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class SkateBoardJump : MonoBehaviour
{
    public GameOverCheck gameoverScr;
    public Rigidbody rb;
    public GameObject LeftRoad;
    public GameObject RightRoad;
    public float force;  //跳跃的初始力量
    public float slideForce;  //向前滑行的力量
    public float slideForceMin;
    public float slideThreshold;
    public float slideForceMax;
    public float inputForce;  //最后输出给滑板的力量
    public float addForceSpeed;  //按住空格每秒增加的力量
    public float frontForce;  //校准跳跃时产生的偏移量
    public float jumpForce;   //跳跃需要的最小力量
    public float jumpAngle;   //Olie产生的角度
    public float fullAngle;   //掉落时的角度
    public float posChange;
    private bool isJump;      //是否起跳
    public float jumpTime;    //滞空时间
    public float jumpChange;  //改变滑板角度的时间
    public float jumpTimeMax;  //结束跳跃
    public float rotateSpeed;  //旋转滑板的速度
    public float moveSpeed;    //自动向前滑行的速度
    public float changeDirectionForce;  //更改方向的力量
    private float typeTime;     //按下空格后开始计时
    public float changeDirectionSpeed;
    public float deadTime;
    public int typeCount;
    private bool timeCheck;    //是否开始计时
    public bool canJump;

    public float curPos;
    public float lastPos;
    public float curSpeed;
    public enum DirectionState
    {
        Left,
        Right,
    }

    public DirectionState currentDirection;
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(CalcuSpeed(1));
        StartCoroutine(TimeDuration(2));
        //StartCoroutine(DelayStart(5));
        inputForce = force;
        rb= GetComponent<Rigidbody>();
        gameoverScr = FindObjectOfType<GameOverCheck>();
    }
    void FixedUpdate()
    {
        if (!gameoverScr.gameOver)
        {
            //transform.Translate(Vector3.forward * moveSpeed);
            MatchPosition();
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{

        //}
        //if(curSpeed < 0.2)
        //{
            deadTime += Time.deltaTime;
        //}
        
        if (!gameoverScr.gameOver)
        {
            CheckJumpState();
            CheckPress();
            JumpAndSlide();
            CalculateSpeed();
        }
        MatchRotation();
        //Debug.Log(transform.rotation.y);
        if(timeCheck)
        {
            typeTime += Time.deltaTime;
        }

        if (typeTime < 0.2)
        {

        }
        else if(typeTime<0.4)
        {
            typeCount = 0;
            
            
        }
        else
        {
            timeCheck = false;
            typeTime = 0;
        }
        if (typeCount >= 2)
        {
            ChangeDirection();
            timeCheck= false;
            typeTime = 0;
            typeCount= 0;
        }
        //if(typeTime>=0.2)
        //{
        //    typeTime = 0;
        //    timeCheck = false;
        //}


        //if(isJump)
        //{
        //    jumpTime += Time.deltaTime;
        //}

        //if(jumpTime>jumpChange)
        //{
        //    gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(fullAngle,0,0)), rotateSpeed * Time.deltaTime);

        //}
        //if (jumpTime > jumpTimeMax)
        //{
        //    isJump = false;
        //    jumpTime = 0;
        //}
    }

    void MatchRotation()
    {
        if (transform.rotation.y > 0.1)
        {
            gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, -15, 0)), rotateSpeed * Time.deltaTime);
        }
        if (transform.rotation.y < -0.1)
        {
            gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 15, 0)), rotateSpeed * Time.deltaTime);
        }
    }

    void JumpAndSlide()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            slideForce += addForceSpeed;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //if (inputForce > jumpForce&&canJump)
            //{
            //    //gameObject.transform.Rotate(new Vector3(jumpAngle, 0, 0));
            //    //rb.AddForce(Vector3.up * inputForce);
            //    //rb.AddForce(Vector3.forward * frontForce);
            //    //isJump = true;
            //}
            //else if(typeTime>=0.15)
            //{
            //    rb.AddForce(Vector3.forward * slideForce);
            //}
            if (Mathf.Abs(slideForce) >Mathf.Abs(slideThreshold))
            {
                rb.AddForce(Vector3.forward * slideForce);
            }
            slideForce = force;
        }
    }

    void CheckPress()
    {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                typeCount++;
                timeCheck = true;
            }
        
        
    }
    void ChangeDirection()
    {
        if(currentDirection == DirectionState.Left)
        {
            rb.AddForce(Vector3.right*changeDirectionForce);
            
            currentDirection = DirectionState.Right;
        }
        else if(currentDirection == DirectionState.Right)
        {
            rb.AddForce(Vector3.left*changeDirectionForce);
           
            currentDirection = DirectionState.Left;
        }

    }

    void MatchPosition()
    {
        if (currentDirection == DirectionState.Left)
        {
            Vector3 targetPos = new Vector3(LeftRoad.transform.position.x, transform.position.y, transform.position.z);
            gameObject.transform.position = Vector3.Lerp(transform.position, targetPos, changeDirectionSpeed * Time.deltaTime);
            
        }
        else if (currentDirection == DirectionState.Right)
        {
            Vector3 targetPos = new Vector3(RightRoad.transform.position.x, transform.position.y, transform.position.z);
            gameObject.transform.position = Vector3.Lerp(transform.position, targetPos, changeDirectionSpeed * Time.deltaTime);
        }
    }

    void CheckJumpState()
    {
        Ray jumpDis = new Ray(gameObject.transform.position,Vector3.down);
        
            
        if(Physics.Raycast(jumpDis, 0.5f))
        {
            canJump= true;
        }
        else
        {
            canJump = false;
        }
    }
    IEnumerator DelayStart(int i)
    {
        yield return new WaitForSeconds(i);
        StartCoroutine(TimeDuration(2));
    }
    IEnumerator TimeDuration(int i)
    {
        
        float currentPosZ = transform.position.z;

        yield return new WaitForSeconds(i);
        
        float nextCurPosZ = transform.position.z;
        posChange = nextCurPosZ - currentPosZ;
        CheckPosChange();
        if (!gameoverScr.gameOver)
        {
            deadTime = 0;
        }
        

        StartCoroutine(TimeDuration(i));
        
    }
    void CheckPosChange()
    {
        if (Mathf.Abs(posChange) < 2/*deadTime>1.8f*/)
        {
            gameoverScr.gameOver= true;
        }
    }
    IEnumerator CalcuSpeed(int i)
    {
        curPos = transform.position.z;
        yield return new WaitForSeconds(i);
        curSpeed =Mathf.Abs( ((curPos - lastPos) / i));
        lastPos = curPos;
        Debug.Log(curSpeed);
        StartCoroutine(CalcuSpeed(i));
    }
    void CalculateSpeed()
    {
        
        
        
        
    }
}
