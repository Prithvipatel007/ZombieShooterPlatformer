using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // to whom the camera follows
    public float smoothing = 5f;    // smoothness

    Vector3 offset;                 //  distance it maintains with the target when following

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;      // camera's position - target's position
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
