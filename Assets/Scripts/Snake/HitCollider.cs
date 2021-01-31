using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D trigger)
	{
		Debug.Log("Trigger");
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("Collision");
	}
}
