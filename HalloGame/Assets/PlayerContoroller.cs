using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck;
using Dreamteck.Splines;




public class PlayerContoroller : MonoBehaviour
{
    // Start is called before the first frame update
    float Horizontal;
    float Vertical;
    float speed = 7f;
    [SerializeField] private SplineComputer _left;
    [SerializeField] private SplineComputer _middle;
    [SerializeField] private SplineComputer _right;



    void Start()
    {
        _left = GameObject.FindGameObjectWithTag("Sol").GetComponent<SplineComputer>();
        _middle = GameObject.FindGameObjectWithTag("Orta").GetComponent<SplineComputer>();
        _right = GameObject.FindGameObjectWithTag("Sag").GetComponent<SplineComputer>();

    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
       
        transform.position+=new Vector3(Horizontal,0,1)*speed*Time.deltaTime;
        if (Horizontal < 0)
        {
            SplineFollower splineFollower = transform.GetComponent<SplineFollower>();
            double startPoint = splineFollower.startPosition;
            splineFollower.spline = _left;
            splineFollower.startPosition = startPoint;
            
        }
        if (Horizontal > 0)
        {
            SplineFollower splineFollower = transform.GetComponent<SplineFollower>();
            double startPoint = splineFollower.startPosition;
            splineFollower.spline = _right;
            splineFollower.startPosition = startPoint;

        }

    }
}
