using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for slider

public class PlayerStatusManager : MonoBehaviour { // Status(行動パターン)管理 HP管理
    public float superuserRate = 0.1f, superuserSecond = 10.0f, attackRate = 0.3f, jumpRate = 0.1f, attackPeriod = 3.0f, jumpPeriod = 3.0f; // 無敵状態になる確率 無敵状態の継続時間[s] 攻撃・ジャンプする確率 攻撃・ジャンプする周期[s]
    public enum PlayerStatus {
        /* 無敵状態 */
        Superuser,
        /* 通常状態 */
        Move, // 移動
        KnockBack, // ノックバック
        Jump, // ジャンプ
        Attack, // 攻撃
        Dead, // HP0
    }
    public PlayerStatus status; // Status管理
    public float hp = 100.0f; // HP管理
    public Slider hpBar; // HPバー
    private float firstHp; // HP初期値
    private float superuserTimer = 0.0f;
    private bool invokeRepeatingIsNotColed = true; // InvokeRepeatingを一回のみ呼ぶ為のフラグ
    void Start() {
        firstHp = hp;
        hpBar.value = 1.0f; // HPバーMax
        if(superuserRate >= Random.value) {
            changeStatusToSuperuser(); // 生成時に一定の割合で無敵状態となる
        } else {
            changeStatusToMove();
        }
    }
    void Update() {
        hpBar.value = hp / firstHp; // HPバー更新
        if(status == PlayerStatus.Superuser) { // 無敵状態
            superuserTimer += Time.deltaTime; // タイマー更新
            if(superuserTimer > superuserSecond)
                status = PlayerStatus.Move; // 無敵状態解除
        } else { // 通常状態
            if(invokeRepeatingIsNotColed && (status != PlayerStatus.KnockBack)) { // ノックバック中は呼び出さない
                InvokeRepeating("changeStatusToAttack",1.0f, attackPeriod); // 攻撃周期ごとに一定確率で攻撃
                InvokeRepeating("changeStatusToJump", 2.0f, jumpPeriod); // ジャンプ周期ごとに一定確率でジャンプ
                invokeRepeatingIsNotColed = false;
            }
            if(hp <= 0.0f) {
                changeStatusToDead(); // HP0未満で消滅
                Destroy(this.gameObject);
            }
        }
    }
    void changeStatusToSuperuser() {
        status = PlayerStatus.Superuser;
    }
    public void changeStatusToMove() {
        status = PlayerStatus.Move;
    }
    public void changeStatusToKnockBack() {
        status = PlayerStatus.KnockBack;
        CancelInvoke();
        invokeRepeatingIsNotColed = true;
    }
    void changeStatusToJump() {
        if(jumpRate >= Random.value) {
            status = PlayerStatus.Jump;
        }
    }
    void changeStatusToAttack() {
        if(attackRate >= Random.value) {
            status = PlayerStatus.Attack;
        }
    }
    void changeStatusToDead() {
        status = PlayerStatus.Dead;
    }
}
