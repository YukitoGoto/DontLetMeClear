using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveManager : MonoBehaviour { // 移動(移動、ノックバック、停止)管理 Status(Move)変更
    public float moveSpeed = 3.0f, superuserSpeed = 6.0f, knockBackPowerX = 5.0f, knockBackPowerY = 7.0f, knockBackSecond = 0.5f, jumpPower = 6.0f; // move superuserのスピード ノックバックのパワー・継続時間[s] ジャンプのパワー
    public string goalObjectName = "takara", moujaName = "mouja";
	public bool leftFlag = false; // playerが左向きかどうか
    private float vx = 0.0f, vy = 0.0f; // xy方向の移動量
	private bool jumpFlag = false; // ジャンプ可能かどうか
	private bool knockBaking = false; // ノックバック中
	private bool touchingMouse = false; // マウスでタッチしているか
	private bool freezeFlag = false; // ゲーム終了時にフリーズ
	private float knockBackTimer = 0.0f; // ノックバックタイマー
	GameObject goalObject, moujaObject;
    Rigidbody2D player;
	MoujaAttackManager moujaAttack;
	PlayerStatusManager playerStatus;
    Vector3 vectorUnit;

	void Start () {
        goalObject = GameObject.Find(goalObjectName); // 目標を発見
		moujaObject = GameObject.Find(moujaName); // moujaオブジェクトを発見
        moujaAttack = moujaObject.GetComponent<MoujaAttackManager>(); // MoujaAttackManagerコンポーネント取得
        player = this.GetComponent<Rigidbody2D>();
		player.constraints = RigidbodyConstraints2D.FreezeRotation; // 衝突時回転無効
		playerStatus = this.GetComponent<PlayerStatusManager>(); // Status更新
	}
	void Update() {
		vx = 0.0f;
		vy = player.velocity.y; // y方向は重力を維持
		if(!freezeFlag){
			if(touchingMouse) {
				touchingMouse = Input.GetMouseButton(0); // ドラッグ判定
			} else {
				vectorUnit = (goalObject.transform.position - this.transform.position).normalized; // 目標への方向ベクトルを取得
				if(playerStatus.status == PlayerStatusManager.PlayerStatus.Move || playerStatus.status == PlayerStatusManager.PlayerStatus.Superuser) {
					vx = vectorUnit.x * ((playerStatus.status == PlayerStatusManager.PlayerStatus.Move) ? moveSpeed : superuserSpeed); // スピード選択
					vy = player.velocity.y; // y方向は重力を維持
				}
				if(playerStatus.status == PlayerStatusManager.PlayerStatus.KnockBack) {
					if(knockBaking){
						vx = player.velocity.x; // 速度維持
						vy = player.velocity.y;
						knockBackTimer += Time.deltaTime; // タイマー更新
						if(knockBackTimer >= knockBackSecond){
							playerStatus.changeStatusToMove(); // StatusをMoveに変更
							knockBaking = false;
							knockBackTimer = 0.0f; // タイマーリセット
						}
					} else {
						player.velocity = new Vector2(0.0f, 0.0f); // 速度リセット
						player.AddForce(new Vector2(-vectorUnit.x * knockBackPowerX, knockBackPowerY), ForceMode2D.Impulse); // ノックバック
						knockBaking = true; // ノックバック処理中有効
					}
				}
				if(jumpFlag && playerStatus.status == PlayerStatusManager.PlayerStatus.Jump) {
					jumpFlag = false;
					player.AddForce(new Vector2(0.0f, jumpPower), ForceMode2D.Impulse); // ジャンプ
					playerStatus.changeStatusToMove(); // StatusをMoveに変更
				}
			}
		}
	}
	void FixedUpdate() {
		leftFlag = (vectorUnit.x < 0) ? true : false;
		this.GetComponent<SpriteRenderer>().flipX = leftFlag; // キャラクターを左右反転
		player.velocity = new Vector2(vx, vy); // 移動量更新
	}
	void OnMouseDown() { // マウスでタッチした
		touchingMouse = true;
	}
	void OnMouseExit() { // マウスが離れた時
		touchingMouse = false;
	}
	void OnCollisionEnter2D(Collision2D collision) {
        if(playerStatus.status != PlayerStatusManager.PlayerStatus.Superuser) { // 無敵状態でないとき
            string collisionObjectName = collision.gameObject.name;
            if(collisionObjectName.Contains(moujaName) || collisionObjectName.Contains(moujaAttack.firePrefab.name) || collisionObjectName.Contains(moujaAttack.misslePrefab.name)) { // 敵と衝突
				knockBaking = false;
				knockBackTimer = 0.0f; // タイマーリセット
            }
        }
    }
	void OnTriggerStay2D(Collider2D collision) {
		jumpFlag = true;
	}
	public void FreezePlayer() {
		freezeFlag = true;
	}
}
