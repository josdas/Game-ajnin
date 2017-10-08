using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : SphereScript {
    private const float CSpeedChange = 5f;
    private const float MinSlowDown = 0.75f;
    private const float RotateSlowDown = 80f;

    public float rotateInputSpeed = 15;
    public float maxSpeed = 15;

    private GameObject _currentPlanet;
    private GameObject _lastPlanet;

    private float _speed;
    private Vector3 _direction = Vector3.right;
    private float _singSpeed = 1;
    private float _rotateSpeed;

    private GameObject _mainCamera;

    private GameObject CurrentPlanet {
        get {
            return _currentPlanet;
        }
        set {
            _currentPlanet = value;
            if (_currentPlanet == null) {
                _mainCamera.GetComponent<JointPosition>().jointTo = gameObject;
            }
            else {
                _lastPlanet = _currentPlanet;
                var dV = gameObject.transform.position - _currentPlanet.transform.position;
                _mainCamera.GetComponent<JointPosition>().jointTo = _currentPlanet;
                var newDirection = Vector3.Cross(gameObject.transform.up, dV).normalized;
                var slowDown = Vector3.Dot(newDirection, _direction);
                _singSpeed *= Math.Sign(slowDown);
                if (slowDown < 0 && slowDown > -MinSlowDown) {
                    slowDown = -MinSlowDown;
                }
                if (slowDown >= 0 && slowDown < MinSlowDown) {
                    slowDown = MinSlowDown;
                }
                _speed *= slowDown;
                _direction = newDirection;
            }
        }
    }

    private Vector3 Position {
        get {
            return gameObject.transform.position;
        }
        set {
            gameObject.transform.position = value;
            if (_currentPlanet != null) {
                var dV = gameObject.transform.position - _currentPlanet.transform.position;
                dV = dV.normalized * (Radius + _currentPlanet.GetComponent<SphereScript>().Radius) / 2;
                gameObject.transform.position = dV + _currentPlanet.transform.position;
            }
        }
    }

    void Start() {
        _speed = maxSpeed / 4;
        _mainCamera = GameObject.FindWithTag("MainCamera");
    }

    void Update() {
        _speed += CSpeedChange * Time.deltaTime * _singSpeed; // * Input.GetAxis("Vertical")
        _speed = Math.Min(_speed, maxSpeed);
        _speed = Math.Max(_speed, -maxSpeed);

        //var angel = Input.GetAxis("Horizontal") * rotateInputSpeed * Time.deltaTime;
        //_direction = Quaternion.Euler(0, angel, 0) * _direction;

        if (Input.GetKeyDown(KeyCode.Space)) {
            CurrentPlanet = null;
        }
    }

    void FixedUpdate() {
        if (_currentPlanet != null) {
            var pRadius = _currentPlanet.GetComponent<SphereScript>().Radius;
            var dV = gameObject.transform.position - _currentPlanet.transform.position;
            _rotateSpeed = 30 * _speed;
            _direction = Vector3.Cross(gameObject.transform.up, dV).normalized;
        }
        else {
            var planets = GameObject.FindGameObjectsWithTag("Planet");
            foreach (var planet in planets) {
                if (planet != _lastPlanet) {
                    var pRadius = planet.GetComponent<SphereScript>().Radius;
                    var distance = Vector3.Distance(Position, planet.transform.position);
                    if (distance < (pRadius + Radius) / 2 * 1.05) {
                        CurrentPlanet = planet;
                        break;
                    }
                }
            }
            if (_rotateSpeed > 0) {
                _rotateSpeed -= RotateSlowDown * Time.fixedDeltaTime;
            }
            else {
                _rotateSpeed += RotateSlowDown * Time.fixedDeltaTime;
            }
        }
        Position += _direction * _speed * Time.fixedDeltaTime;
        transform.Rotate(Vector3.up, _rotateSpeed * Time.fixedDeltaTime);
    }
}
