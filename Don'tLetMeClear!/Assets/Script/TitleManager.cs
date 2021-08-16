using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour {
    public GameObject titleCanvasPrefab, stageSelectCanvasPrefab, controlDescriptionCanvasprefab; // タイトル ステージ選択 操作説
    public string[] loadStageName;
    GameObject currentCanvas;
    void Start() {
        Title();
    }
    void Title() {
        Destroy(currentCanvas); // 前回のキャンバスを消去
        currentCanvas = Instantiate(titleCanvasPrefab);
        Button[] buttons = currentCanvas.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(StageSelect);
        buttons[1].onClick.AddListener(ControlDescription);
    }
    void StageSelect() {
        Destroy(currentCanvas);
        currentCanvas = Instantiate(stageSelectCanvasPrefab);
        Button[] buttons = currentCanvas.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(() => LoadStage(loadStageName[0]));
        buttons[1].onClick.AddListener(() => LoadStage(loadStageName[1]));
        buttons[2].onClick.AddListener(() => LoadStage(loadStageName[2]));
        buttons[3].onClick.AddListener(Title);
    }
    void ControlDescription() {
        Destroy(currentCanvas);
        currentCanvas = Instantiate(controlDescriptionCanvasprefab);
        Button[] buttons = currentCanvas.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(Title);
    }
    void LoadStage(string stageName) {
        SceneManager.LoadScene("Load" + stageName); // Load画面から読み込む
    }
}
