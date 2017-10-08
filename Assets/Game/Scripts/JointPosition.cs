using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointPosition : MonoBehaviour {
    public float maxSpeed;
    public GameObject jointTo;
    public float speed;
    private Vector3 _velocity = Vector3.zero;

    void FixedUpdate() {
        var position = jointTo.transform.position;
        position.y = transform.position.y; // because it is 2.5D
        gameObject.transform.position = Vector3.SmoothDamp(transform.position, position, ref _velocity, 0.2f);
    }
}
