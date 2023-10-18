using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Week5_LauchList : MonoBehaviour
{
    public List<GameObject> preList;
    public List<GameObject> lauchList;
    public GameObject position;
    public List<GameObject> positions;
    public int randomIndex;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lauchList.Count == 0)
        {
            foreach (GameObject go in preList)
            {
                lauchList.Add(go);
            }
            for (int i = 0; i < lauchList.Count; i++)
            {
                lauchList[i].transform.position = positions[i].transform.position;
            }
        }

            if (Input.GetKeyDown(KeyCode.Space))
            {

                randomIndex = Random.Range(0, lauchList.Count);
                lauchList[randomIndex].transform.position = position.transform.position;
                lauchList[randomIndex].GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 1) * force);
                lauchList.RemoveAt(randomIndex);
            }

        
       
    }

        
    }

