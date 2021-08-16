using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoujaMoveManager : MonoBehaviour {

	public string rightKey = "right", leftKey = "left", jumpKey = "space", hardDropKey = "down";
    public float speedX = 3.0f, speedY = 15.0f, jumpPower = 8.0f;
    public int jumpMaxCount = 2; // Moujaは2段ジャンプまで可能
	private int jumpCouter = 0;
	private float vx = 0.0f, vy = 0.0f; // xy方向の移動量
	private bool leftFlag = false; // moujaが左向きかどうか
	private bool jumpFlag = false; // ジャンプ可能かどうか
	private bool freezeFlag = false; // ゲーム終了時にフリーズ
	Rigidbody2D mouja;

	void Start () {
		mouja = this.GetComponent<Rigidbody2D>();
		mouja.constraints = RigidbodyConstraints2D.FreezeRotation; // 衝突時回転無効
	}

	void Update () {
		vx = 0.0f;
        vy = mouja.velocity.y; // y方向は重力を維持
		if(!freezeFlag) {
			if(Input.GetKey(rightKey)) {
				vx = speedX;
				leftFlag = false;
			}
			if(Input.GetKey(leftKey)) {
				vx = -speedX;
				leftFlag = true;
			}
			if(Input.GetKeyDown(jumpKey) & (jumpCouter < jumpMaxCount)) {
		    	jumpFlag = true; // ジャンプを許可
            	jumpCouter++;
			}
        	if(Input.GetKey(hardDropKey) & (jumpCouter != 0)) {
            	vy = -speedY; // ハードドロップ
        	}
		}
	}
	void FixedUpdate() {
		mouja.velocity = new Vector2(vx, vy); // 移動量更新
		this.GetComponent<SpriteRenderer>().flipX = leftFlag; // キャラクターを左右反転
		if(jumpFlag) {
			jumpFlag = false;
			mouja.AddForce(new Vector2(0.0f, jumpPower), ForceMode2D.Impulse); // y方向に一瞬力を加える
		}
	}
	void OnTriggerStay2D(Collider2D collision) {
        jumpCouter = 0; // 地面についたらリセット
	}
	public void FreezeMouja() {
		freezeFlag = true;
	}
}
