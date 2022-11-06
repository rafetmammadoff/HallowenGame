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
    void Start()
    {
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

    }

    // Update is called once per frame
    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        transform.position += new Vector3(0, 0, -Horizontal) * 5f * Time.deltaTime;

        transform.position=new Vector3(transform.position.x,transform.position.y,( Mathf.Clamp(transform.localPosition.z, -1.8f, 1.8f)));
        

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            anim.SetTrigger("Jump");
            isGround = false;
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            anim.SetTrigger("Run");
            isGround = true;
            Debug.Log("Deydi");
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
