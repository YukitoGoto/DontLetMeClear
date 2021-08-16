using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {
    public string currentStageName = "Stage1", nextStageName = "Stage2", titleName = "title";
    public string moujaName = "mouja", playerTag = "Player", takaraName = "takara";
    public GameObject gameoverCanvasPrefab, clearCanvasPrefab;
    public AudioClip gameOverMusic, gameClearMusic;
    public bool nextStageDoNotExist = false;
    GameObject moujaObject, takaraObject;
    GameObject[] playerObjects;
    AudioSource stageMusic;
    private bool isnotCalledClearGameover = true;
    private bool allPlayerDead = false;
    void Start() {
        moujaObject = GameObject.Find(moujaName);
        playerObjects = GameObject.FindGameObjectsWithTag(playerTag); // 全player取得
        takaraObject = GameObject.Find(takaraName);
        stageMusic = this.GetComponent<AudioSource>();
        stageMusic.Play(); // bgm再生
    }
    void Update() {
        if(isnotCalledClearGameover) { // ゲームオーバー・クリア画面は一度のみ表示
            if((moujaObject.GetComponent<MoujaStatusManager>().status == MoujaStatusManager.MoujaStatus.Dead) || takaraObject.GetComponent<OnCollisionInform>().collidedObject) {
                Gameover();
            }
            foreach(GameObject playerObject in playerObjects) { // playerが全て死んだか
                if(playerObject != null){
                    allPlayerDead = false;
                    break;
                } else {
                    allPlayerDead = true;
                }
            }
            if(allPlayerDead) {
                Clear();
            }
        }
    }
    void Clear() {
        isnotCalledClearGameover = false;
        FreezeObject(); // オブジェクト停止
        stageMusic.Stop(); // bgm停止
        stageMusic.PlayOneShot(gameClearMusic); // クリア効果音
        GameObject clearCanvas = Instantiate(clearCanvasPrefab);
        Button[] buttons = clearCanvas.GetComponentsInChildren<Button>();
        if(nextStageDoNotExist) { // 最終ステージの場合
            buttons[0].interactable = false; // 選択不可能にする
            buttons[1].onClick.AddListener(LoadRetry);
            buttons[2].onClick.AddListener(LoadTitle);
        } else {
            buttons[0].onClick.AddListener(LoadNextStage);
            buttons[1].onClick.AddListener(LoadRetry);
            buttons[2].onClick.AddListener(LoadTitle);
        }
    }
    void Gameover() { 
        isnotCalledClearGameover = false;
        stageMusic.Stop();
        stageMusic.PlayOneShot(gameOverMusic);
        FreezeObject();
        GameObject gameoverCanvas = Instantiate(gameoverCanvasPrefab);
        Button[] buttons = gameoverCanvas.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(LoadRetry);
        buttons[1].onClick.AddListener(LoadTitle);
    }
    void FreezeObject() {
        // コンポーネントを無効化
        moujaObject.GetComponent<MoujaStatusManager>().enabled = false;
        moujaObject.GetComponent<MoujaAnimeManager>().enabled = false;
        moujaObject.GetComponent<MoujaMoveManager>().FreezeMouja();
        moujaObject.GetComponent<MoujaAttackManager>().enabled = false;
        moujaObject.GetComponent<MoujaDamageManager>().enabled = false;
        moujaObject.GetComponent<Animator>().enabled = false;
        if(!allPlayerDead) {
            foreach(GameObject playerObject in playerObjects) {
                if(playerObject != null) { // nullでないplayerのコンポーネントを無効化
                    playerObject.GetComponent<PlayerStatusManager>().enabled = false;
                    playerObject.GetComponent<PlayerAnimeManager>().enabled = false;
                    playerObject.GetComponent<PlayerMoveManager>().FreezePlayer();
                    playerObject.GetComponent<PlayerAttackManager>().enabled = false;
                    playerObject.GetComponent<PlayerDamageManager>().enabled = false;
                    playerObject.GetComponent<Animator>().enabled = false;
                }
            }
        }
    }
    void LoadRetry() {
        SceneManager.LoadScene(currentStageName);
    }
    void LoadNextStage() {
        SceneManager.LoadScene("Load" + nextStageName); // Load画面から読み込む
    }
    void LoadTitle() {
        SceneManager.LoadScene(titleName);
    }
}
