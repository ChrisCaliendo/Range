using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform target;
    private Vector3 offset;
    [Range(-1, -20)]
    [SerializeField] private float zoomOut;
    [Range(1, 10)]
    [SerializeField] private float smoothFactor;

    private void Start()
    {
        offset = new Vector3(0, 0, zoomOut); 
    }
    void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor*Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
