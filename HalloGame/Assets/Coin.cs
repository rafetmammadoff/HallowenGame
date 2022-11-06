using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    float time = 0.1f;

    void Start()
    {
        time = Random.Range(0.2f, 0.6f);
       var seq = DOTween.Sequence();
        seq.Append(transform.DORotate(new Vector3(0, 180, 0),time ).SetLoops(-1, LoopType.Incremental));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        var seq = DOTween.Sequence();

        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Invoke("Small", 1f);
        }
    }

    void Small()
    {
        gameObject.SetActive(true);
    }
}
