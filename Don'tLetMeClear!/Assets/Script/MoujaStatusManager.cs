using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for slider

public class MoujaStatusManager : MonoBehaviour { // Status(行動パターン)管理 HP管理
    public enum MoujaStatus {
        Normal, // 通常状態
        Dead, // HP0
    }
    public MoujaStatus status; // Status管理
    public float hp = 100.0f; // HP管理
    public Slider hpBar; // HPバー
    private float firstHp; // HP初期値
    void Start() {
        firstHp = hp;
        hpBar.value = 1.0f; // HPバーMax
        changeStatusToNormal();
    }
    void Update() {
        hpBar.value = hp / firstHp; // HPバー更新
        if(hp <= 0.0f) {
            changeStatusToDead(); // HP0未満でゲームオーバー
        }
    }
    void changeStatusToNormal() {
        status = MoujaStatus.Normal;
    }
    void changeStatusToDead() {
        status = MoujaStatus.Dead;
    }
}
