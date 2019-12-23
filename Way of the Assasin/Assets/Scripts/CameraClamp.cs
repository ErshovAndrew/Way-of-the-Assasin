using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    [SerializeField]
    private Transform targetToFollow;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, -2.66f, 24.12f),
            Mathf.Clamp(targetToFollow.position.y, -1.47f, 0.02f),
            transform.position.z);
    }
}
