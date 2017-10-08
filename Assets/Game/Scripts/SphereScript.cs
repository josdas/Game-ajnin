using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour {
    public float _radius = 1f;

    public float Radius {
        get {
            return _radius;
        }
        set {
            _radius = value;
            gameObject.transform.localScale = Vector3.one * _radius;
        }
    }

	// Use this for initialization
	void Start () {
	    _radius = gameObject.transform.localScale.x;
	}
}
