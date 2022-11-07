using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightscript : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] Light isik;
    float time;
  public bool isActive=true;
    float intensity;
    void Start()
    {
        //StartCoroutine(Change());
    }

    // Update is called once per frame
    void Update()
    {
        time = Random.Range(0.1f, 0.5f);
        intensity = Random.Range(0, 5);
        isik.intensity = intensity;
        if (isActive)
        {
            //isik.enabled = true;
            //material.SetColor("_EmissionColor", Color.white);
            //material.EnableKeyword("_EMISSION");
            
        }
        if (!isActive)
        {
            //isik.enabled = false;
            //material.SetColor("_EmissionColor", Color.black);
            //material.DisableKeyword("_EMISSION");

        }
    }

    public IEnumerator Change()
    {
        isActive=!isActive;
        yield return new WaitForSeconds(time);
        isActive = !isActive;
        yield return new WaitForSeconds(time);

        Yeniden();
    }
    void Yeniden()
    {
        StartCoroutine(Change());
    }


}
