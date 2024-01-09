using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyMove : MonoBehaviour
{
    void Awake()
    {
        LeanTween.moveLocalX(gameObject, -290f, 25f);
    }    
}
