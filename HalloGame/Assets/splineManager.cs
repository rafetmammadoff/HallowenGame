using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splineManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool line1;
    public bool line2;
    public bool line3;
    
    void Start()
    {
        line1 = false;
        line3 = false;
        line2 = true;


    }
    // 0 0 0
    // Update is called once per frame
    void Update()
    {
        bool rightCondition = (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow));
        bool leftCondition = (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow));

        if (line2 && rightCondition)
        {
            Debug.Log("Saga basdi");
            transform.position += new Vector3(0, 0, -1.1f);
            line1 = false;
            line2 = false;
            line3 = true;
            Debug.Log("Tru oldum2" + line2);
            Debug.Log("Tru oldum1" + line1);
            Debug.Log("Tru oldum3" + line3);
        }
        else if (line2 && leftCondition)
        {
            transform.position += new Vector3(0, 0, 1.1f);
            line1 = true;
            line3 = line2 = false;
        }
        else if (line1 && rightCondition)
        {
            transform.position += new Vector3(0, 0, -1.1f);
            line2 = true;
            line1 = line3 = false;
        }

        else if ( line3 && leftCondition)
        {
            transform.position += new Vector3(0, 0, 1.1f);
            line2 = true;
            line1 = line3 = false;
        }

    }
}
