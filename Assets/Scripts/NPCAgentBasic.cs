using UnityEngine;
using System;
using UnityEngine.AI;

public class NPCAgentBasic : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[SerializeField] private Transform _target;
	private Animator _anim;
	private NavMeshAgent _meshAgent;
	private float _previousAcceleration;
	#endregion

	#region Unity Callbacks
	private void Awake()
	{
		_anim = GetComponent<Animator>();
		_meshAgent = GetComponent<NavMeshAgent>();
	}
	// Start is called before the first frame update
	void Start()
    {
        _previousAcceleration = _meshAgent.acceleration;
	}

    // Update is called once per frame
    void Update()
    {
        _meshAgent.destination = _target.position;
		if (_meshAgent.velocity.magnitude == 0)
		{
			//Attack 
			_anim.SetBool("Walking", false);
			_anim.SetBool("Attack", true);
			transform.LookAt(_target);
		}
		else
		{
			_anim.SetBool("Walking", true);
			_anim.SetBool("Attack", false);
		}

		if (Vector3.Distance(transform.position, _target.position) > 10)
		{
			//Idle
			_anim.SetBool("Walking", false); 
			_anim.SetBool("Attack", false);
			_meshAgent.acceleration = 0;
			_meshAgent.velocity = Vector3.zero;
		}
		else
		{
			_meshAgent.acceleration = _previousAcceleration;
		}
    }
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion
   
}
