using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	// If we hit player we definitely have to make something nasty to him.
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			RunnerCharacter.Instance.TakeDamage();
		}
	}
}
