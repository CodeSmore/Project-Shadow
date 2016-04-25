using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour {

	[SerializeField]
	private bool walkEnabled = false;
	[SerializeField]
	private float speed = 0;

	private Rigidbody2D monsterRigidbody;
	private LevelManager levelManager;
	private Camera mainCamera;
	private Animator animator;

	[SerializeField]
	private bool shakeWalkEnabled = false;
	[SerializeField]
	private float shakeAmount = 0;
	[SerializeField]
	private float shakeLength = 0;

	[SerializeField]
	private bool loadLevelOnTriggerEnabled = false;
	[SerializeField]
	private string levelToLoad = null;

	[SerializeField]
	private TriggerController walkTrigger = null;

	private Vector3 originalCameraPosition;

	// Use this for initialization
	void Start () {
		monsterRigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		if (walkEnabled) {
			monsterRigidbody.velocity = new Vector2 (speed, 0f);
			animator.SetBool("walking", walkEnabled);
		}

		levelManager = GameObject.FindObjectOfType<LevelManager>();
		mainCamera = Camera.main;

	}

	void Update () {
		if (walkTrigger) {
			if (walkTrigger.IsTriggered()) {
				EnableWalk();
			}
		}
	}
	
	void OnTriggerEnter2D (Collider2D collider) {
		if (loadLevelOnTriggerEnabled && collider.gameObject.tag == "Player") {
			levelManager.LoadLevel(levelToLoad);
		} else if (collider.gameObject.tag == "EnemyKiller") {
			Destroy(gameObject);
		}
	}

	public void Stomp () {
		if (shakeWalkEnabled) {
			originalCameraPosition = mainCamera.transform.localPosition;
			InvokeRepeating("DoShake", 0, 0.01f);
			Invoke("StopShake", shakeLength);
		}
	}

	void DoShake () {
		if (shakeAmount > 0) {
			Vector3 camPos = mainCamera.transform.position;

			float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
			float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
			camPos.x += offsetX;
			camPos.y += offsetY;

			mainCamera.transform.position = camPos;	
		}
	}

	void StopShake () {
		CancelInvoke("DoShake");
		mainCamera.transform.localPosition = originalCameraPosition;
	}

	public void EnableWalk () {
		walkEnabled = true;
		monsterRigidbody.velocity = new Vector2 (speed, 0f);
		animator.SetBool("walking", walkEnabled);
	}

	public void DisableWalk () {
		animator.SetBool("walking", false);
	}
}
