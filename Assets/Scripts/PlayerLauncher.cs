using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerLauncher : MonoBehaviour {

	private Animator playerAnimator;
	private EventSystem eventSystem;

	[SerializeField]
	private GameObject projectile = null;
	[SerializeField]
	private float throwForce = 0;
	[SerializeField]
	private float timeBetweenThrows = 0;

	private float throwTimer = 0;

	// Use this for initialization
	void Start () {
		playerAnimator = GetComponentInParent<Animator>();
		eventSystem = FindObjectOfType<EventSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0) && eventSystem.currentSelectedGameObject == null) {
			ThrowSnowball();
		}

		throwTimer += Time.deltaTime;
	}

	private void ThrowSnowball () {
		if (throwTimer > timeBetweenThrows && playerAnimator.GetBool("firing")) {
			// first calculate the vector from player launch position to where screen was tapped
			// and normalize it.
			Vector2 touchPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			Vector2 throwVector = touchPos - new Vector2(transform.position.x, transform.position.y);
			throwVector.Normalize();

			// Then, instantiate the object and apply force according to throwVector
			GameObject thrownObject = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
			thrownObject.GetComponent<Rigidbody2D>().AddForce (throwVector * throwForce);

			throwTimer = 0;
		}
	}
}
