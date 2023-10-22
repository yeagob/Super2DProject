using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Jetpack : MonoBehaviour
{
	public enum Direction
	{
		Left,
		Right
	}

	#region Properties
	public float Energy { get; set; }
	#endregion

	#region Fields							     
	private Rigidbody2D _target;
	[SerializeField] private float _maxEnergy;
	[SerializeField] private float _energyFlyingRatio;
	[SerializeField] private float _energyRegenerationRatio;
	[SerializeField] private float _horizontalForce;
	[SerializeField] private float _flyForce;
	private bool _flying = false;

	#endregion

	#region Unity Callbacks
	private void Awake()
	{
		_target = GetComponent<Rigidbody2D>();
	}
	// Start is called before the first frame update
	void Start()
	{
		Energy = _maxEnergy;
	}

	// Update is called once per physic frame
	void FixedUpdate()
	{
		if (_flying)
			DoFly();
	}

	#endregion

	#region Public Methods
	public void FlyUp()
	{
		_flying = true;
	}
	public void StopFlying()
	{
		_flying = false;
	}

	public void Regenerate()
	{
		Energy += _energyRegenerationRatio;
	}

	public void Regenerate(float energy)
	{
		Energy += energy;
	}

	public void FlyHorizontal(Direction flyDirection)
	{
		if (!_flying)
			return;

		if (flyDirection == Direction.Left)
			_target.AddForce(Vector2.left * _horizontalForce);
		else
			_target.AddForce(Vector2.right * _horizontalForce);

	}
	#endregion

	#region Private Methods
	private void DoFly()
	{
		if (Energy > 0)
		{
			_target.AddForce(Vector2.up * _flyForce);
			Energy -= _energyFlyingRatio;
		}
		else
			_flying = false;
	}
	#endregion
}


