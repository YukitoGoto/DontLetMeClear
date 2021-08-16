using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaceCamera : MonoBehaviour {
    public string chaceTargetName = "mouja";
    GameObject targetObject;
    Vector3 baseCameraPos;
    void Start() {
        targetObject = GameObject.Find(chaceTargetName);
        baseCameraPos = Camera.main.gameObject.transform.position; // カメラの初期位置を保存
    }
    void LateUpdate() { // 処理の最後に行う
        Vector3 pos = targetObject.transform.position;
        pos.z = -10; // カメラを手前に引く
        pos.y =  baseCameraPos.y;
        Camera.main.gameObject.transform.position = pos;
    }
}
