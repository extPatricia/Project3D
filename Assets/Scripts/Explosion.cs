using UnityEngine;
using System;

public class Explosion : MonoBehaviour
{
	#region Properties
	#endregion

	#region 
	[SerializeField] private Camera _mainCamera;
	[SerializeField] private Camera _explosionCamera;
	[SerializeField] private GameObject _explosionEffect;
	[SerializeField] private GameObject _explosionPlayer;
	[SerializeField] private float _explosionArea;
	[SerializeField] private float _explosionForce;
	#endregion

	#region Unity Callbacks
	private void Update()
	{
		if(_explosionEffect != null)
		{
			_explosionCamera.transform.LookAt(_explosionPlayer.transform.position);
			_explosionCamera.transform.Translate(_explosionCamera.transform.forward * Time.deltaTime * 2f, Space.Self);
			if (Vector3.Distance(_explosionCamera.transform.position, _explosionPlayer.transform.position) < 3)
			{
				_explosionCamera.enabled = false;
				_mainCamera.enabled = true;
				Vector3 currentPos = _explosionPlayer.transform.position;
				_explosionPlayer.transform.localPosition = Vector3.zero;
				_explosionPlayer.transform.GetComponent<CharacterController>().enabled = false;
				_explosionPlayer.transform.position = currentPos;
				_explosionPlayer.transform.GetComponent<CharacterController>().enabled = true;
				_explosionPlayer.transform.GetComponent<Animator>().enabled = true;
				Destroy(gameObject);
				//Vector3 currentPos = _explosionPlayer.transform.position;
				//_explosionPlayer.transform.position = currentPos;
			}
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == _explosionPlayer)
		{
			// Switch cameras
			_mainCamera.enabled = false;
			_explosionCamera.enabled = true;

			// Activate explosion effect
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
