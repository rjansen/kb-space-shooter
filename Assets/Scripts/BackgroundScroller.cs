using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    public float speed = -0.25f;
    public float size = 30f;

    Vector3 startPosition;

    void Start () {
        startPosition = transform.position;
    }

    void Update () {
        float newPosition = Mathf.Repeat(Time.time * speed, size);
        transform.position = startPosition + Vector3.forward * newPosition;
    }
}
