using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpritesAnimation : MonoBehaviour
{
    [SerializeField] private GameObject[] image;
    [SerializeField] private Transform[] startPos;
    [SerializeField] private Transform[] intermediaryPos;
    [SerializeField] private Transform endPos;
    private float interpolateAmount = 0;
    private int i, j = 0;
    private float lerpTime;
    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        lerpTime = Random.Range(0f, 1f);
        interpolateAmount = (interpolateAmount + Time.deltaTime * lerpTime) % 1f;
        StartCoroutine(PLayAnimation());
    }
    private IEnumerator PLayAnimation()
    {

        for (i = 0; i < image.Length; i++)
        {
            if (j == 4)
            {
                j = 0;
            }
            image[i].transform.position = QuadraticLerp(startPos[j].position,
          intermediaryPos[j].position,
          endPos.position,
          interpolateAmount);
            j++;
            yield return new WaitForSeconds(2f);
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
        return QuadraticLerp(ab, bc, cd, t);

    }

}
