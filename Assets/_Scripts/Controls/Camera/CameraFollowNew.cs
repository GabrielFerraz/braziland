using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowNew : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;

    public Vector3 offset;
    private Vector3 baseOffset;
    Vector3 desiredPosition;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
    }
    private void Start()
    {
        baseOffset = offset;
    }

    public void RestoreView()
    {
        offset = baseOffset;
    }
    private void Update()
    {
        desiredPosition = target.position + offset;
    }
    void LateUpdate()
    {
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
