using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoujaAnimeManager : MonoBehaviour {
    public string normalAnime, attackAnime;
    private string attack1Key, attack2Key, nowAnime;
    void Start() {
        attack1Key = this.GetComponent<MoujaAttackManager>().fireKey; // MoujaAttackManagerからKeyを取得
        attack2Key = this.GetComponent<MoujaAttackManager>().missileKey;
        nowAnime = normalAnime;
    }
    void Update() {
        if(Input.GetKey(attack1Key) | Input.GetKey(attack2Key)) {
            nowAnime = attackAnime;
        } else {
            nowAnime = normalAnime;
        }
    }
    void FixedUpdate() {
        this.GetComponent<Animator>().Play(nowAnime);
    }
}
