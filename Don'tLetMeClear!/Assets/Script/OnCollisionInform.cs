using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionInform : MonoBehaviour {
    public string targetObjectName = "player";
    public bool collidedObject = false;
    void OnCollisionEnter2D(Collision2D collision) {
        string collisionObjectName = collision.gameObject.name;
        if(collisionObjectName.Contains(targetObjectName)) { // ターゲットと衝突
            collidedObject = true;
        }
    }
}
