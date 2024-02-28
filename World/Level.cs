using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    public const int DAY_TIME = 72000;
    public const int HOUR_TIME = 3000;
    private const float LIGHT_FRAME_ROTATION = 0.05f;

    private int time;

    void Start() {
        time = 0;
    }

    void FixedUpdate() {
        time++;
    }
}
