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
    public float force;  //��Ծ�ĳ�ʼ����
    public float slideForce;  //��ǰ���е�����
    public float inputForce;  //�����������������
    public float addForceSpeed;  //��ס�ո�ÿ�����ӵ�����
    public float frontForce;  //У׼��Ծʱ������ƫ����
    public float jumpForce;   //��Ծ��Ҫ����С����
    public float jumpAngle;   //Olie�����ĽǶ�
    public float fullAngle;   //����ʱ�ĽǶ�
    public bool isJump;      //�Ƿ�����
    public float jumpTime;    //�Ϳ�ʱ��
    public float jumpChange;  //�ı们��Ƕȵ�ʱ��
    public float jumpTimeMax;  //������Ծ
    public float rotateSpeed;  //��ת������ٶ�
    public float moveSpeed;    //�Զ���ǰ���е��ٶ�
    public float changeDirectionForce;  //���ķ��������
    public float typeTime;     //���¿ո��ʼ��ʱ
    public float changeDirectionSpeed;
    public int typeCount;
    private bool timeCheck;    //�Ƿ�ʼ��ʱ
    public bool canJump;
    public enum DirectionState
    {
        Left,
        Right,
    }

    public DirectionState currentDirection;
    // Start is called before the first frame update
    void Start()
    {
        inputForce = force;
        rb= GetComponent<Rigidbody>();
        gameoverScr = FindObjectOfType<GameOverCheck>();
    }
    void FixedUpdate()
    {
        if (!gameoverScr.gameOver)
        {
            transform.Translate(Vector3.forward * moveSpeed);
            MatchPosition();
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{

        //}
        if (!gameoverScr.gameOver)
        {
            CheckJumpState();
            CheckPress();
            JumpAndSlide();
        }
        MatchRotation();
        Debug.Log(transform.rotation.y);
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


        if(isJump)
        {
            jumpTime += Time.deltaTime;
        }

        if(jumpTime>jumpChange)
        {
            gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(fullAngle,0,0)), rotateSpeed * Time.deltaTime);

        }
        if (jumpTime > jumpTimeMax)
        {
            isJump = false;
            jumpTime = 0;
        }
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
            inputForce += addForceSpeed;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (inputForce > jumpForce&&canJump)
            {
                gameObject.transform.Rotate(new Vector3(jumpAngle, 0, 0));
                rb.AddForce(Vector3.up * inputForce);
                rb.AddForce(Vector3.forward * frontForce);
                isJump = true;
            }
            else if(typeTime>=0.15)
            {
                rb.AddForce(Vector3.forward * slideForce);
            }
            inputForce = force;
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
}
