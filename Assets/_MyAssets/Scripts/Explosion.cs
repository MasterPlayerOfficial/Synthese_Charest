using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 0.5f);
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 2.0f);
    }
}
