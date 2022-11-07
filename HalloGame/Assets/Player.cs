using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using TMPro;
using DG.Tweening;
using AmazingAssets.CurvedWorld.Example;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    bool isGround=true;
    public TMP_Text text;
    public TMP_Text Score;
    public int counter;
    Animator anim;
   public ChunkSpawner Spawner;
    [SerializeField] GameObject DeadMenu;
    int startMoney;
    public bool line1;
    public bool line2;
    public bool line3;
    [SerializeField] float JumpForce = 20;
    Vector3 vec;
    float time = 15f;
    void Start()
    {
        line1 = false;
        line3 = false;
        line2 = true;
        startMoney = PlayerPrefs.GetInt("Money");
        rb= GetComponent<Rigidbody>();
        anim= GetComponent<Animator>();
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", counter);
            PlayerPrefs.Save();
            counter=0;
        }
        else
        {
            counter = PlayerPrefs.GetInt("Money");
        }
        text.text =counter.ToString();
        vec = transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time>time)
        {
            Spawner.movingSpeed += 0.5f;
            time = Time.time+15f;
        }
        bool rightCondition = (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow));
        bool leftCondition = (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow));
        transform.position = Vector3.Lerp(transform.position, vec, 0.05f);
        if (line2 && rightCondition)
        {
            //Debug.Log("Saga basdi");
            //transform.position += new Vector3(0, 0, -16f);
            vec = new Vector3(transform.position.x, transform.position.y, -1.1f);
            line1 = false;
            line2 = false;
            line3 = true;
            
        }
        else if (line2 && leftCondition)
        {
            //transform.position += new Vector3(0, 0, 16f) ;
            vec = new Vector3(transform.position.x, transform.position.y, 1.1f);

            line1 = true;
            line3 = line2 = false;
        }
        else if (line1 && rightCondition)
        {
            //transform.position += new Vector3(0, 0, -16f) ;
            vec = new Vector3(transform.position.x, transform.position.y, 0);

            line2 = true;
            line1 = line3 = false;
        }

        else if (line3 && leftCondition)
        {
            //transform.position += new Vector3(0, 0, 16f) ;
            vec = new Vector3(transform.position.x, transform.position.y, 0);

            line2 = true;
            line1 = line3 = false;
        }
        //float Horizontal = Input.GetAxis("Horizontal");
        //transform.position += new Vector3(0, 0, -Horizontal) * 5f * Time.deltaTime;

        //transform.position=new Vector3(transform.position.x,transform.position.y,( Mathf.Clamp(transform.localPosition.z, -1.8f, 1.8f)));
        //if (!isGround)
        //{
        //    cs.movingSpeed = 37;
        //}
        //else
        //{
        //    cs.movingSpeed = 27;
        //}
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            anim.SetTrigger("Jump");
            isGround = false;
            rb.AddForce(Vector3.up*JumpForce, ForceMode.Impulse);


        }

       

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            anim.SetTrigger("Run");
            isGround = true;
        }

        if (collision.transform.CompareTag("Enemy"))
        {
            Spawner.movingSpeed = 0;
            anim.SetTrigger("Dead");
            GetComponent<CapsuleCollider>().enabled = false;
            DeadMenu.SetActive(true);
            Score.text=(counter-startMoney).ToString();

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Coin"))
        {
            counter++;
            PlayerPrefs.SetInt("Money", counter);
            text.text = PlayerPrefs.GetInt("Money").ToString();
           
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        DeadMenu.SetActive(false);
    }

}
