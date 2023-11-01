using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class week6_Slot : MonoBehaviour
{
    public GameObject[] gameObjects;
    public int index;
    public bool turn;
    public bool isChanging;
    public Coroutine corout;
    public enum ChangeObject
    {
        cube,
        sphere,
        cylinder,
        capsule,
    }

    public ChangeObject changeobj;
    // Start is called before the first frame update
    void Start()
    {
       corout= StartCoroutine(ChangeSlot(1));
    }

    // Update is called once per frame
    void Update()
    {
        
        gameObjects[0].gameObject.SetActive(changeobj==ChangeObject.cube);
        gameObjects[1].gameObject.SetActive(changeobj == ChangeObject.sphere);
        gameObjects[2].gameObject.SetActive(changeobj == ChangeObject.cylinder);
        gameObjects[3].gameObject.SetActive(changeobj == ChangeObject.capsule);


        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (!isChanging)
            {         
                isChanging = true;
            }
            else
            {
                isChanging = false;
            }
            StartCoroutine(ChangeSlot(1));
        }
        //if(isChanging)
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        turn= false;
        //        isChanging= false;
        //    }
        //}

    }
    
    IEnumerator ChangeSlot(int i)
    {

        if (isChanging)
        {       
            changeobj = ChangeObject.cube;
            if (!isChanging)
            {
                yield break;
            }
            yield return new WaitForSeconds(i);
            changeobj = ChangeObject.sphere;
            if (!isChanging)
            {
                yield break;
            }
            yield return new WaitForSeconds(i);
            if (!isChanging)
            {
                yield break;
            }
            changeobj = ChangeObject.cylinder;
            yield return new WaitForSeconds(i);
            if (!isChanging)
            {
                yield break;
            }
            changeobj = ChangeObject.capsule;
            
            yield return new WaitForSeconds(i);
            StartCoroutine(ChangeSlot(i));
        }
        

    }
}
