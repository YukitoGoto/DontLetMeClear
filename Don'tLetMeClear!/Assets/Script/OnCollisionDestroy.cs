using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionDestroy : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D collision) {
        Destroy(this.gameObject); // 衝突時消滅
    }
}
