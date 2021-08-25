using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform playerTransform;

    private Vector3 position;

    void Start()
    {
        position = playerTransform.InverseTransformPoint(transform.position);
    }

    void Update()
    {
        var currentPosition = playerTransform.TransformPoint(position);
        transform.position = currentPosition;
        transform.LookAt(playerTransform);
    }
}
