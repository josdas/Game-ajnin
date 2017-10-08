using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainColorChanger : MonoBehaviour {
    // Use this for initialization
    void Start() {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color",
                                                              new Color(
                                                                        1 - Random.Range(0, 0.6f),
                                                                        1 - Random.Range(0, 0.6f),
                                                                        1 - Random.Range(0, 0.8f)
                                                                       )
                                                             );
    }
}
