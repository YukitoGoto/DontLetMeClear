using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassedTimeDestory : MonoBehaviour {
    public float limitTime = 0.5f;
    void Start() {
        Destroy(this.gameObject, limitTime); // limitTime経過で消滅
    }
}
