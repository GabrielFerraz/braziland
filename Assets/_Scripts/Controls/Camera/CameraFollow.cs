using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;

    public Vector3 offset;
    private Vector3 baseOffset;
    Vector3 desiredPosition; 
    //[SerializeField] private Vector3 _eagleViewOffset;

    //private Vector3 _tutorialOffset = new Vector3(-8f, 0, 0f);

    //private bool toggle;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
    }
    private void Start()
    {
        baseOffset = offset;
    }

    //public void TutorialOffset()
    //{
    //    offset += _tutorialOffset;
    //}

    public void RestoreView()
    {
        offset = baseOffset;
    }

    //public void ToggleEagleView()
    //{
    //    toggle = !toggle;
    //    if (toggle)
    //        offset -= _eagleViewOffset;
    //    else
    //        offset = baseOffset;
    //}
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
