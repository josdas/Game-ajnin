using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Coin : SphereScript {
    public static float BaseRadius = 0.3f;
    public Transform finisher;
    public Texture[] textures;

    void Start() {
        Radius = BaseRadius;
        gameObject.GetComponent<Renderer>().material.mainTexture = textures[Random.Range(0, textures.Length)];
        transform.Rotate(Vector3.up, Random.Range(0, 360));
    }

    void OnTriggerEnter(Collider other)  {
        if (other.gameObject.tag == "Player") {
            GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreScript>().update();
            var instance = Instantiate(finisher, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, 1);
            Destroy(gameObject, 0.1f);
        }
    }
}
