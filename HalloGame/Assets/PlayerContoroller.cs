using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck;
using Dreamteck.Splines;
using DG.Tweening;




public class PlayerContoroller : MonoBehaviour
{
    // Start is called before the first frame update
    float Horizontal;
    float Vertical;
    float speed = 7f;
    [SerializeField] private SplineComputer _left;
    [SerializeField] private SplineComputer _middle;
    [SerializeField] private SplineComputer _right;
    [SerializeField] private int _counter = 0;
    Sequence seq = DOTween.Sequence();
    Rigidbody rb;
    [SerializeField] float jumpForce = 5f;
    bool isGround=true;
    Animator anim;
    [SerializeField] float second=1;
    [SerializeField] float count=1;
    [SerializeField] float nextTime=0;
    SplineFollower splFollower;
    void Start()
    {
        _left = GameObject.FindGameObjectWithTag("Sol").GetComponent<SplineComputer>();
        _middle = GameObject.FindGameObjectWithTag("Orta").GetComponent<SplineComputer>();
        _right = GameObject.FindGameObjectWithTag("Sag").GetComponent<SplineComputer>();
        rb = transform.GetComponent<Rigidbody>();
        anim=GetComponent<Animator>();
        splFollower=transform.GetComponent<SplineFollower>();

    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");

        //transform.position+=new Vector3(Horizontal,0,1)*speed*Time.deltaTime;
        if (Time.time>=nextTime)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {

                SplineFollower splineFollower = transform.GetComponent<SplineFollower>();
                if (splineFollower.spline == _middle)
                {
                    var seq = DOTween.Sequence();
                    double startPoint = splineFollower.startPosition;
                    Vector3 leftDistance = new Vector3(-5.5f, 0, 4.1f);
                    seq.Append(transform.DOMove((transform.position + leftDistance), 0.5f)).AppendCallback(() =>
                    {
                        splineFollower.spline = _left;
                        splineFollower.startPosition = startPoint;
                    });



                }
                if (splineFollower.spline == _right)
                {
                    var seq = DOTween.Sequence();
                    double startPoint = splineFollower.startPosition;
                    Vector3 leftDistance = new Vector3(-5.5f, 0, 4.1f);
                    seq.Append(transform.DOMove((transform.position + leftDistance), 0.5f)).AppendCallback(() =>
                    {
                        splineFollower.spline = _middle;
                        splineFollower.startPosition = startPoint;
                    });


                }
                nextTime = Time.time + (second / count);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                SplineFollower splineFollower = transform.GetComponent<SplineFollower>();
                if (splineFollower.spline == _middle)
                {
                    var seq = DOTween.Sequence();
                    double startPoint = splineFollower.startPosition;
                    Vector3 rightDistance = new Vector3(5.5f, 0, 4.1f);
                    seq.Append(transform.DOMove((transform.position + rightDistance), 0.5f)).AppendCallback(() =>
                    {
                        splineFollower.spline = _right;
                        splineFollower.startPosition = startPoint;
                    });
                }
                if (splineFollower.spline == _left)
                {
                    var seq = DOTween.Sequence();
                    double startPoint = splineFollower.startPosition;
                    Vector3 rightDistance = new Vector3(5.5f, 0, 4.1f);
                    seq.Append(transform.DOMove((transform.position + rightDistance), 0.5f)).AppendCallback(() =>
                    {
                        splineFollower.spline = _middle;
                        splineFollower.startPosition = startPoint;
                    });


                }
                nextTime = Time.time + (second / count);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)&&isGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("Jump");
            isGround = false;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            anim.SetTrigger("Run");
            isGround = true;
        }
        if (collision.transform.CompareTag("Finish"))
        {
            splFollower.followSpeed = 0;
            anim.SetTrigger("Dance");
        }
        if (collision.transform.CompareTag("Enemy"))
        {
            splFollower.followSpeed = 0;
            anim.SetTrigger("Dead");
        }
    }
}




