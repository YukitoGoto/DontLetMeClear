using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaceObject : MonoBehaviour {
    public string targetObjectName;
    public float chaceSpeed = 3.0f, rotateTorque = 0.5f; // 追跡スピード 回転トルク
    public bool isRotate = false; // オブジェクトが回転する
    public bool destroyIfVectorUnitXIsPositive = false; // 進行方向が正なら消滅
    public bool destroyIfVectorUnitXIsNegative = false; // 進行方向が負なら消滅
    GameObject targetObject;
    Rigidbody2D thisObject;
    Vector3 vectorUnit; // オフジェクトの進行方向

    void Start() {
        targetObject = GameObject.Find(targetObjectName); // 目標を発見する
        thisObject = this.GetComponent<Rigidbody2D>();
        vectorUnit = (targetObject.transform.position - this.transform.position).normalized; // オブジェクト生成時に目標への方向ベクトルを取得
        this.GetComponent<SpriteRenderer>().flipX = (vectorUnit.x < 0); // 左右反転
        if((destroyIfVectorUnitXIsPositive && vectorUnit.x > 0.0f) || (destroyIfVectorUnitXIsNegative && vectorUnit.x < 0.0f)) {
            Destroy(this.gameObject);
        }
        if(isRotate) {
            if(vectorUnit.x < 0)
                thisObject.AddTorque(rotateTorque, ForceMode2D.Impulse); // トルクを加える
            else
                thisObject.AddTorque(-rotateTorque, ForceMode2D.Impulse);
        }
    }
    void FixedUpdate() {
        thisObject.velocity = new Vector2(vectorUnit.x * chaceSpeed, vectorUnit.y * chaceSpeed);
    }
}
