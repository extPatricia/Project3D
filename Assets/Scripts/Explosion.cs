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
	[SerializeField] private AudioClip _explosionSound;
	#endregion

	#region Unity Callbacks
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == _explosionPlayer)
		{
			// Switch cameras
			_mainCamera.enabled = false;
			_explosionCamera.enabled = true;
			_explosionCamera.transform.LookAt(_explosionPlayer.transform.position);
			_explosionCamera.transform.Translate(_explosionCamera.transform.forward * Time.deltaTime * 2f, Space.Self);

			// Activate explosion effect
			_explosionEffect.SetActive(true);
			// Play explosion sound
			AudioSource.PlayClipAtPoint(_explosionSound, transform.position);

			CharacterController playerController = other.GetComponent<CharacterController>();
			if (playerController != null)
				playerController.enabled = false;

			Animator playerAnim = other.GetComponent<Animator>();
			if (playerAnim != null)
				playerAnim.enabled = false;

			ExplosionForce();

			Invoke(nameof(ResetPlayer), 4f);
		}
	}

	private void ResetPlayer()
	{
		// Switch back cameras
		_explosionCamera.enabled = false;
		_mainCamera.enabled = true;

		
		// Reset player position and state
		Vector3 currentPos = _explosionPlayer.transform.position;
		_explosionPlayer.transform.localPosition = Vector3.zero;
		_explosionPlayer.transform.position = currentPos;

		CharacterController playerController = _explosionPlayer.GetComponent<CharacterController>();
		if (playerController != null)
			playerController.enabled = true;

		Animator playerAnim = _explosionPlayer.GetComponent<Animator>();
		if (playerAnim != null)
			playerAnim.enabled = true;

		Destroy(gameObject);
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
