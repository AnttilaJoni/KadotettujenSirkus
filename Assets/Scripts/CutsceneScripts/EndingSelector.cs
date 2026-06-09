using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndingSelector : MonoBehaviour
{
    public GameObject neutral;
    public GameObject good;
    
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().gameEnding == 2) //<-- Good ending saveState
        {
            good.SetActive(true);
        }
        else
        {
            neutral.SetActive(true);
        }
    }
}
