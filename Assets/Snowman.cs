using UnityEngine;
using System.Collections;

public class Snowman : MonoBehaviour {

	[SerializeField]
	private GameObject snowManHead = null;

	void OnDestroy () {
		Instantiate(snowManHead, transform.position, Quaternion.identity);
	}
}
