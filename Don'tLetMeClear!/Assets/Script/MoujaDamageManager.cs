using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoujaDamageManager : MonoBehaviour {
    public string playerName = "player"; // 攻撃力の参照元
    GameObject playerObject;
    PlayerAttackManager playerAttack;
    MoujaStatusManager moujaStatus;
    void Start() {
        playerObject = GameObject.Find(playerName);
        playerAttack = playerObject.GetComponent<PlayerAttackManager>(); // playerの攻撃力を取得
        moujaStatus = this.GetComponent<MoujaStatusManager>();
    }
    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.name.Contains(playerName)) { // playerと衝突
            PlayerStatusManager playerStatus = collision.gameObject.GetComponent<PlayerStatusManager>(); // 衝突したplayerのStatusを取得
            if(playerStatus.status == PlayerStatusManager.PlayerStatus.Superuser) { // playerが無敵状態
                moujaStatus.hp -= playerAttack.superuserDamage;
            } else {
                moujaStatus.hp -= playerAttack.playerDamage;
            }
        }
        if(collision.gameObject.name.Contains(playerAttack.ballPrefab.name)) { // ballと衝突
            moujaStatus.hp -= playerAttack.ballDamage;
        }
    }
}
