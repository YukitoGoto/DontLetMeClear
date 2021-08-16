using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimeManager : MonoBehaviour {
    public string normalAnime, superuserAnime;
    private string nowAnime;
    PlayerStatusManager playerStatus;
    void Start() {
        nowAnime = normalAnime;
        playerStatus = this.GetComponent<PlayerStatusManager>(); // Status更新
    }
    void FixedUpdate() {
        if(playerStatus.status == PlayerStatusManager.PlayerStatus.Superuser) {
            nowAnime = superuserAnime;
        }
        if(playerStatus.status == PlayerStatusManager.PlayerStatus.Move || playerStatus.status == PlayerStatusManager.PlayerStatus.Jump) {
            nowAnime = normalAnime;
        }
        this.GetComponent<Animator>().Play(nowAnime);
    }
}
