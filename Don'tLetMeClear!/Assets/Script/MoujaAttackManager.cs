using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoujaAttackManager : MonoBehaviour {
    public string fireKey = "e", missileKey = "r";
    public float offsetXFire = 1.2f, offsetYMissile = 2.0f, speedFire = 5.0f,  missilePowerX = 3.0f, missilePowerY = 10.0f, missileTorque = 0.4f; // プレハブのオフセット fireのスピード missileのパワー missileのトルク
    public float fireDamage = 10.0f, missileDamage = 30.0f, moujaDamage = 20.0f; // fire missile moujaのダメージ設定
    public float intervalSecondFire = 0.5f, intervalSecondMissile = 1.0f; // fire missileの連射間隔
    public GameObject firePrefab, misslePrefab;
    private bool leftFlag = false; // moujaが左向きかどうか
    private string rightKey, leftKey;
    private float intervalTimerFire, intervalTimerMissile; // fire missileのタイマー
    void Start() {
        rightKey = this.GetComponent<MoujaMoveManager>().rightKey; // MoujaMoveManagerからKeyを取得
        leftKey = this.GetComponent<MoujaMoveManager>().leftKey;
        intervalTimerFire = intervalSecondFire; // タイマー初期化
        intervalTimerMissile = intervalSecondMissile;
    }
	void Update() {
        intervalTimerFire += Time.deltaTime; // タイマー更新
        intervalTimerMissile += Time.deltaTime;
        if(Input.GetKey(rightKey)) {
            leftFlag = false;
        }
        if(Input.GetKey(leftKey)) {
            leftFlag = true;
        }
        if(Input.GetKey(fireKey) && (intervalTimerFire >= intervalSecondFire)) {
            intervalTimerFire = 0.0f; // タイマーリセット
            Vector3 firePos = this.transform.position;
            firePos.x =  leftFlag ? firePos.x - offsetXFire : firePos.x + offsetXFire; // オフセット反映
            firePos.z = -5.0f; // 手前に表示
            GameObject fireGameObject = Instantiate(firePrefab) as GameObject; // fireを生成
            fireGameObject.transform.position = firePos;
            fireGameObject.GetComponent<SpriteRenderer>().flipX = leftFlag; // fireを反転
            MoveXRotateZ fire = fireGameObject.GetComponent<MoveXRotateZ>();
            fire.speed = leftFlag ? -speedFire : speedFire; // fireの放射方向
        }
        if(Input.GetKey(missileKey) && (intervalTimerMissile >= intervalSecondMissile)) {
            intervalTimerMissile = 0.0f; // タイマーリセット
            Vector3 missilePos = this.transform.position;
            missilePos.y += offsetYMissile; // オフセット反映
            missilePos.z = -5.0f; // 手前に表示
            GameObject missileGameObject = Instantiate(misslePrefab) as GameObject; // missileを生成
            missileGameObject.transform.position = missilePos;
            Rigidbody2D missile = missileGameObject.GetComponent<Rigidbody2D>();
            if(leftFlag) { // missileの発射方向
                missile.AddForce(new Vector2(-missilePowerX, missilePowerY), ForceMode2D.Impulse);
                missile.AddTorque(missileTorque, ForceMode2D.Impulse); // トルクを加える
            } else {
                missile.AddForce(new Vector2(missilePowerX, missilePowerY), ForceMode2D.Impulse);
                missile.AddTorque(-missileTorque, ForceMode2D.Impulse);
            }
        }
        intervalTimerFire = (intervalTimerFire > intervalSecondFire) ? intervalSecondFire : intervalTimerFire; // オバーフロー対策
        intervalTimerMissile = (intervalTimerMissile > intervalSecondMissile) ? intervalSecondMissile : intervalTimerMissile;
    }
}
