using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Loading : MonoBehaviour
{
    private int Int;
    public void ClickonPlay()
    {
        SceneManager.LoadScene("Loading_Kirby");
    }

    private void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }
    IEnumerator ExampleCoroutine()
    {
        while (Int < 10)
        {
            //Debug.Log(Int++);
            yield return new WaitForSeconds(1);
        }


    }

    void Update()
    {

    }
}
