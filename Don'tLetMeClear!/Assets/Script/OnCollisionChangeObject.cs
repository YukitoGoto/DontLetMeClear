using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionChangeObject : MonoBehaviour {
    public GameObject prefab;
    public float offsetX = 0.0f, offsetY = 0.0f;
    void OnCollisionEnter2D(Collision2D collision) {
        Vector3 newObjectPos = this.transform.position;
        newObjectPos.x += offsetX; // オフセット反映
        newObjectPos.y += offsetY;
        newObjectPos.z = -5; // 手前に表示
        GameObject newObject = Instantiate(prefab) as GameObject; // プレハブ生成
        newObject.transform.position = newObjectPos;
    }
}
