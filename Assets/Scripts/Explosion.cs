using UnityEngine;
using System;

public class Explosion : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[SerializeField] private GameObject _explosionEffect;
	[SerializeField] private GameObject _explosionTarget;
	#endregion

	#region Unity Callbacks
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == _explosionTarget)
			_explosionEffect.SetActive(true);
	}
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion

}
