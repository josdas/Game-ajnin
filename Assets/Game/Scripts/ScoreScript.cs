using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
    private int _score = -1;

    void Start() {
        update();
    }

    public void update() {
        _score++;
        gameObject.GetComponent<Text>().text = "Score: " + _score.ToString();
    }
}
