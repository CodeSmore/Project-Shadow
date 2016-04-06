using UnityEngine;
using System.Collections;

public class Raycasting : MonoBehaviour {

	public bool clung = false;

	[SerializeField]
	private GameObject origin = null;
	[SerializeField]
	private float viableClingDistance = 0;
	[SerializeField]
	private LayerMask layersToHit = -1;

	private PlayerMovement playerMovement;
	private Rigidbody2D playerRigidbody2D;
	private Animator playerAnimator;

	private Vector2 originVector;
	private RaycastHit2D hit;

	void Start () {
		playerMovement = GetComponent<PlayerMovement>();
		playerRigidbody2D = GetComponent<Rigidbody2D>();
		playerAnimator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {

		if ((playerMovement.IsGrounded() || playerRigidbody2D.velocity.y < 0)) {
			UpdateRayYAxis();
		} 

		// must UpdateRayYAxis() if falling
		if (hit.distance > 0 && hit.distance <= viableClingDistance && !playerMovement.IsGrounded()) {
			playerAnimator.SetBool("ledge grab", true);
			playerMovement.DisableAllMovement();
			playerRigidbody2D.drag = 1000;

		} else {
			playerAnimator.SetBool("ledge grab", false);
			playerMovement.EnableAllMovement();
			playerRigidbody2D.drag = 0;
		}

		originVector = new Vector2 (origin.transform.position.x, originVector.y);
		hit = Physics2D.Raycast(new Vector2 (originVector.x, originVector.y), Vector2.down, 2f, layersToHit);

		// 'hit.point' reverts to (0,0) if hit == false, so it's set to max distance down instead
		if (!hit) {
			Debug.DrawLine(originVector, new Vector2 (originVector.x, originVector.y - 2));
		} else {
			Debug.DrawLine(originVector, hit.point);
		}

	}

	void UpdateRayYAxis () {
		originVector = new Vector2 (origin.transform.position.x, origin.transform.position.y);
	}
}
