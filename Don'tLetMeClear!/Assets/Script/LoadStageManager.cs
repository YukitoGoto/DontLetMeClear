using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStageManager : MonoBehaviour {
    public string loadStageName = "Stage1";
    public float loadSecond = 3.0f;
    void Start() {
        IEnumerator loadStage = LoadStageCoroutine();
        StartCoroutine(loadStage);
    }
    IEnumerator LoadStageCoroutine() {
        yield return new WaitForSeconds(loadSecond);
        SceneManager.LoadScene(loadStageName);
    }
}
