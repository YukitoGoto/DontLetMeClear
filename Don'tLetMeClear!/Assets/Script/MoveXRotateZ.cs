using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveXRotateZ : MonoBehaviour {
    public float speed = 3.5f, angle = 0.0f;
    void FixedUpdate() {
        this.transform.Translate(speed / 50.0f, 0, 0, Space.World); // ワールド座標で移動
        this.transform.Rotate(0, 0, angle/ 50.0f);
    }
}
