using UnityEngine;
using System.Collections;

public class BouyancyController : MonoBehaviour {

	[SerializeField]
	private float upwardForce = 0;

	private float waterSurfaceYPos = 0;

	private SoundController soundController;

	void Start () {
		BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
		soundController = GameObject.Find("SoundController").GetComponent<SoundController>();

		waterSurfaceYPos = transform.position.y + boxCollider.size.y / 2 + boxCollider.offset.y;
	}

	void OnTriggerStay2D (Collider2D collider) {
		if (collider.tag == "Floater" && collider.transform.position.y < waterSurfaceYPos) {
			collider.GetComponent<Rigidbody2D>().drag = 1;
			float depth = collider.gameObject.transform.position.y;

			// Force must take into account... 
			// how deep (deeper = more force)
			// mass (higher mass = more force)
			// At point it should settle (when collider.transform.pos.y roughly == watersurfaceYPos), force should roughly equal gravity force * mass
			collider.GetComponent<Rigidbody2D>().AddForce(new Vector2 (0, upwardForce * (waterSurfaceYPos + 1 - depth)));
		}
	}

	void OnTriggerEnter2D (Collider2D collider) {
		// play sploosh
		// added delay due to box that starts in water causing a ruckus!
		if (Time.timeSinceLevelLoad > 2) {
			soundController.PlaySoundEffect(1);
		}
	}
}
