using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SymbolsAnimation : MonoBehaviour
{

    [SerializeField] private Transform startPos;
    [SerializeField] private Transform intermediaryPos1;
    [SerializeField] private Transform intermediaryPos2;
    [SerializeField] private Transform endPos;
    [SerializeField] private float lerpSpeed;
    private float interpolateAmount = 0;

    private void Awake()
    {
        GetComponent<Image>().enabled = true;
    }
    private void Start()
    {
        lerpSpeed = Random.Range(0.1f, 0.7f);
    }
    void Update()
    {
        interpolateAmount = (interpolateAmount + Time.deltaTime * lerpSpeed) % 1f;
        if (lerpSpeed >= 0.5)
        {
            transform.position = QuadraticLerp(startPos.position, intermediaryPos1.position,
            endPos.position, interpolateAmount);
        }
        else
        {
            transform.position = CubicLerp(startPos.position, intermediaryPos1.position,
            intermediaryPos2.position,
            endPos.position, interpolateAmount);
        }
    }
    Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);
        return Vector3.Lerp(ab, bc, t);
    }
    Vector3 CubicLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);
        Vector3 cd = Vector3.Lerp(c, d, t);
        Vector3 abc = Vector3.Lerp(ab, bc, t);
        Vector3 bcd = Vector3.Lerp(bc, cd, t);
        return Vector3.Lerp(abc, bcd, t);


    }
}
