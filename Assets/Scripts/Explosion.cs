using UnityEngine;
using System;

public class Explosion : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[SerializeField] private GameObject _explosionEffect;
	[SerializeField] private GameObject _explosionTarget;
	[SerializeField] private float _explosionArea;
	[SerializeField] private float _explosionForce;
	#endregion

	#region Unity Callbacks
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == _explosionTarget)
		{
			_explosionEffect.SetActive(true);
			Animator playerAnim = other.GetComponent<Animator>();
			if (playerAnim != null)
				playerAnim.enabled = false;
			ExplosionForce();
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, _explosionArea);
	}
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	private void ExplosionForce()
	{
		Collider[] colliders =
		Physics.OverlapSphere(transform.position, _explosionArea);
		for (int i = 0; i < colliders.Length; i++) 
		{ 
			Rigidbody rb = colliders[i].GetComponent<Rigidbody>(); 
			if (rb != null) 
			{ 
				rb.AddExplosionForce(_explosionForce, transform.position, _explosionArea); 
			} 
		}
	}
	#endregion

}
