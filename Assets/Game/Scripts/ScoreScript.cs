using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
    private int _score = -1;

    private string _sicretText = "";

    void Start() {
        _sicretText = "Cобирай стикеры!\nУправление: Space/tap - улететь с планеты.";
        UpdateScore();
    }

    void Print(string text) {
        _sicretText = text;
    }

    public void UpdateScore() {
        _score++;
        /*if (_score == 17) {
            Print("Наверное тут должно быть поздравление, но ты выбрала другое число :)");
        }
        if (_score == 19) {
            Print("Вторая попытка) В следующий раз точно повезет!");
        }
        if (_score == 59) {
            Print("Не, 59 - это слишком банально)");
        }
        if (_score == 63) {
            Print("Воооот, почтиииии...");
        }
        if (_score == 69) {
            Print("Любимая, ты у меня одна такая,\nТы лучшая на свете для меня,\nИ с Днем рождения сегодня поздравляя,\nЯ обнимаю и люблю тебя.");
        }
        if (_score == 125) {
            Print("Как же много у тебя свободного времени!)");
        }
        if (_score == 200) {
            Print("Игра - это хорошо, а задачки кто решать будет?)");
        }*/
        gameObject.GetComponent<Text>().text = "Score: " + _score.ToString() + "\n" + _sicretText;
    }
}
