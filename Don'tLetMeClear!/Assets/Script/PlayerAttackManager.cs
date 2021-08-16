using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour {
    public float offsetXBall = 0.0f, offsetYBall = 1.0f, speedBall = 7.0f; // プレハブのオフセット ballのスピード
    public float ballDamage = 20.0f, superuserDamage = float.MaxValue, playerDamage = 10.0f; // ball superuser playerのダメージ設定
    public GameObject ballPrefab;
    public bool stopAttackWhenPositive = false; // 進行方向が正なら攻撃無効
    public bool stopAttackWhenNegative = false; // 進行方向が負なら攻撃無効
    PlayerMoveManager playerMove;
    PlayerStatusManager playerStatus;
    void Start() {
        playerMove = this.GetComponent<PlayerMoveManager>(); // Status更新
        playerStatus = this.GetComponent<PlayerStatusManager>();
    }
	void Update() {
        if(playerStatus.status == PlayerStatusManager.PlayerStatus.Attack) { // 攻撃
            GameObject ballGameObject = Instantiate(ballPrefab) as GameObject; // ball生成
            ballGameObject.GetComponent<ChaceObject>().destroyIfVectorUnitXIsPositive = stopAttackWhenPositive; // 攻撃無効
            ballGameObject.GetComponent<ChaceObject>().destroyIfVectorUnitXIsNegative = stopAttackWhenNegative;
            ballGameObject.GetComponent<ChaceObject>().chaceSpeed = speedBall; // speed反映
            Vector3 ballPos = this.transform.position;
            ballPos.x += offsetXBall; // オフセット反映
            ballPos.y += offsetYBall;
            ballPos.z = -5.0f; // 手前に表示
            ballGameObject.transform.position = ballPos;
            ballGameObject.GetComponent<ChaceObject>().chaceSpeed = speedBall; // speed反映
            playerStatus.changeStatusToMove(); // StatusをMoveに変更
        }
    }
}
