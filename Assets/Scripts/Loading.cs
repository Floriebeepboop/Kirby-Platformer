using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Loading : MonoBehaviour
{
    public void ClickonPlay()
    {
        SceneManager.LoadScene("Loading_Kirby");
    }

    void Update()
    {

    }
}
