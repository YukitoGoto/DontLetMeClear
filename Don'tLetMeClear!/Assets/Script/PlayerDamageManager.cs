using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageManager : MonoBehaviour { // ダメージ管理 Status(KnockBack)変更
    public string moujaName = "mouja";
    GameObject moujaObject;
    MoujaAttackManager moujaAttack;
    PlayerStatusManager playerStatus;
    void Start() {
        moujaObject = GameObject.Find(moujaName); // moujaオブジェクト発見
        moujaAttack = moujaObject.GetComponent<MoujaAttackManager>(); // MoujaAttackManagerコンポーネント取得
        playerStatus = this.GetComponent<PlayerStatusManager>(); // Status更新
    }
    void OnCollisionEnter2D(Collision2D collision) {
        if(playerStatus.status != PlayerStatusManager.PlayerStatus.Superuser) { // 無敵状態でないとき
            string collisionObjectName = collision.gameObject.name;
            if(collisionObjectName.Contains(moujaName)) { // moujaと衝突
                playerStatus.hp -= moujaAttack.moujaDamage; // ダメージを受ける
                playerStatus.changeStatusToKnockBack(); // StatusをKnockBackに変更
            }
            if(collisionObjectName.Contains(moujaAttack.firePrefab.name)) { // fireと衝突
                playerStatus.hp -= moujaAttack.fireDamage;
                playerStatus.changeStatusToKnockBack();
            }
            if(collisionObjectName.Contains(moujaAttack.misslePrefab.name)) { // missileと衝突
                playerStatus.hp -= moujaAttack.missileDamage;
                playerStatus.changeStatusToKnockBack();
            }
        }
    }
}
