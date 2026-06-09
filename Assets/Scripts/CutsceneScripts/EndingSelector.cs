using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndingSelector : MonoBehaviour
{
    public GameObject neutral;
    public GameObject good;
    void Start()
    {
        /* if() //<-- Good ending saveState
        {
            good.setActive(true);
        } */
        //else
        //{
            neutral.SetActive(true);
        //}
    }
}
