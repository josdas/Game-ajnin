using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {
    public Transform planet;
    public Transform coin;

    public int neededObjectsCount;
    public float planetProbabilty;
    public float baseDistance;
    public float maxDistance;
    public float viewRange;
    public float minimalDistanceBetweenObjects;
    public float timeToUpdate;

    private GameObject _mainObject;
    private float _lastUpdate;

    void Start() {
        _mainObject = GameObject.FindWithTag("Player");
        Generate();
    }

    void Generate() {
        var gObjects = GameObject.FindGameObjectsWithTag("Planet")
            .Concat(GameObject.FindGameObjectsWithTag("Coin"));
        var posibleToToucheObjects = new List<GameObject>();
        var count = 0;
        foreach (var gObject in gObjects) {
            var distance = Vector3.Distance(gObject.transform.position, _mainObject.transform.position);
            if (distance > maxDistance) {
                Destroy(gObject);
            }
            if (distance < baseDistance) {
                count++;
            }
            if (distance < baseDistance + 2 * Planet.MaxRadius) {
                posibleToToucheObjects.Add(gObject);
            }
        }
        for (; count < neededObjectsCount; count++) {
            var isPlanet = Random.Range(0f, 1f) < planetProbabilty;
            float radius;
            if (isPlanet) {
                radius = Random.Range(Planet.MinRadius, Planet.MaxRadius);
            }
            else {
                radius = Coin.BaseRadius;
            }
            Vector3 position = Vector3.zero;
            bool genereted = false;
            for (int i = 0; i < 20; i++) {
                var deltaPosition = new Vector3(
                                                Random.Range(-baseDistance, baseDistance),
                                                0,
                                                Random.Range(-baseDistance, baseDistance)
                                               );
                position = deltaPosition + _mainObject.transform.position;
                position.y = 0;
                var distanceFromPositionToMain = Vector3.Distance(position, _mainObject.transform.position);
                if (distanceFromPositionToMain > baseDistance || distanceFromPositionToMain < viewRange) {
                    continue;
                }
                bool goodPosition =
                    posibleToToucheObjects.TrueForAll(
                                    x => Vector3.Distance(x.transform.position, position) >
                                         radius + minimalDistanceBetweenObjects + x.GetComponentInChildren<SphereScript>().Radius
                                   );
                if (goodPosition) {
                    genereted = true;
                    break;
                }
            }
            if (genereted) {
                var objectType = isPlanet ? planet : coin;
                var instance = Instantiate(objectType, position, Quaternion.identity);
                instance.gameObject.GetComponentInChildren<SphereScript>().Radius = radius;
                posibleToToucheObjects.Add(instance.gameObject);
            }
        }
    }

    void Update() {
        if (_lastUpdate < 0) {
            _lastUpdate = timeToUpdate;
            Generate();
        }
        _lastUpdate -= Time.deltaTime;
    }
}
