using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilHandler : MonoBehaviour
{

    void Update()
    {
        if (transform.position.x >= 500f) { Destroy(gameObject); }
    }

}
