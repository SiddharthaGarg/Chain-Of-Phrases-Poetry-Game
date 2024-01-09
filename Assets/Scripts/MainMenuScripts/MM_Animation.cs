using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_Animation : MonoBehaviour
{
    private Vector2 startPoint;
    private Vector3 endPoint = new Vector3(725, 0, 0);

    private Vector2 startScale;
    private Vector2 endScale = new Vector2(0.01f, 0.01f);
    private float tweenTime = 5f;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        LeanTween.moveLocal(gameObject, endPoint, tweenTime).setEaseInQuad().setLoopClamp();
        LeanTween.scale(gameObject, endScale, tweenTime).setEaseInExpo().setLoopClamp();
        LeanTween.rotateAround(gameObject, new Vector3(0, 0, 1), 360, 1f).setLoopClamp();


    }
}
