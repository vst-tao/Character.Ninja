using UnityEngine;

namespace Assets.SpriteAnimations.Scripts {

	public class Ninja : MonoBehaviour {

		[SerializeField]
		Rigidbody2D _body = null;

		[SerializeField]
		float _runForce = 3000f;

		[SerializeField]
		float _runBrakeForce = 8000f;

		[SerializeField]
		float _jumpForce = 600f;

		[SerializeField]
		float _hoverForce = 500f;

		[SerializeField]
		float _maxRunSpeed = 10f;

		[SerializeField]
		float _midairFlyForce = 1000f;

		[SerializeField]
		float _slideResistant = 100f;

		[SerializeField]
		Vector2 _groundCheckOffset = Vector2.zero;

		[SerializeField]
		LayerMask _groundLayer = default(LayerMask);

		[SerializeField]
		CapsuleCollider2D _collider = null;

		[SerializeField]
		float _standHeight = 1.4f;

		[SerializeField]
		float _slideHeight = 0.8f;

		public Rigidbody2D Body {
			get { return _body; }
		}

		/// <summary>
		/// Running force applied when the ninja is running. Also used
		/// in drag calculation.
		/// </summary>
		public float RunForce {
			get { return _runForce; }
		}

		/// <summary>
		/// Braking force to apply if the ninja stops running.
		/// </summary>
		public float RunBrakeForce {
			get { return _runBrakeForce; }
		}

		/// <summary>
		/// Jumping force to apply when the ninja jumps.
		/// </summary>
		public float JumpForce {
			get { return _jumpForce; }
		}

		public float HoverForce {
			get { return _hoverForce; }
		}

		/// <summary>
		/// Resistive force acting on the ninja when sliding.
		/// </summary>
		public float SlideResistant {
			get { return _slideResistant; }
		}

		/// <summary>
		/// Force the ninja may apply mid-air to change direction (left/right).
		/// </summary>
		public float MidairFlyForce {
			get { return _midairFlyForce; }
		}

		/// <summary>
		/// Whether the ninja looking to the left.
		/// </summary>
		public bool IsFacingLeft {
			get;
			set;
		}

		/// <summary>
		/// Whether the ninja is running.
		/// </summary>
		public bool IsRunning {
			get;
			set;
		}

		/// <summary>
		/// Whether the ninja is currently on the ground.
		/// </summary>
		public bool IsGrounded {
			get;
			private set;
		}

		/// <summary>
		/// Whether the ninja is currently sliding on the ground.
		/// </summary>
		public bool IsSliding {
			get { return _isSliding; }
			set {
				if (_isSliding == value) return;
				_isSliding = value;
				var size = _collider.size;
				var offset = _collider.offset;
				if (value) {
					// is sliding
					size.y = _slideHeight;
					offset.y = _slideHeight * 0.5f;
				} else {
					// is standing
					size.y = _standHeight;
					offset.y = _standHeight * 0.5f;
				}
				_collider.size = size;
				_collider.offset = offset;
			}
		}
		bool _isSliding = false;

		/// <summary>
		/// Whether the ninja is currently knocked out.
		/// </summary>
		public bool IsKnockedOut {
			get;
			set;
		}

		/// <summary>
		/// Whether the ninja is reviving from a knock out.
		/// </summary>
		public bool IsReviving {
			get;
			set;
		}

		/// <summary>
		/// Calculate the drag applied on the ninja when running.
		/// </summary>
		/// <param name="velocity">The speed at which the ninja is running.</param>
		/// <returns>Drag in newtons.</returns>
		public float CalculateRunDrag(float velocity) {
			// the greater the velocity, the greater the drag is
			// ultimately, at max speed, the drag will completely
			// balance out the run force, preventing the ninja
			// from getting higher velocity
			float percentage = velocity / _maxRunSpeed;
			return -_runForce * percentage;
		}

		/// <summary>
		/// Calculate the drag applied on the ninja when sliding.
		/// </summary>
		/// <param name="velocity">The speed at which the ninja is running.</param>
		/// <returns>Drag in newtons.</returns>
		public float CalculateSlideDrag(float velocity) {
			// the greater the velocity, the greater the drag is
			// ultimately, at max speed, the drag will completely
			// balance out the run force, preventing the ninja
			// from getting higher velocity
			float percentage = velocity / _maxRunSpeed;
			return -_slideResistant * percentage;
		}

		/// <summary>
		/// Calculate the braking force when the ninja stops running.
		/// </summary>
		/// <param name="velocity">The velocity at which the ninja is currently moving at.</param>
		/// <returns></returns>
		public float CalculateBrakingForce(float velocity) {
			// the greater the velocity, the greater the braking force to apply
			float percentage = velocity / _maxRunSpeed;
			return -_runBrakeForce * percentage;
		}

		/// <summary>
		/// Update the <see cref="IsGrounded"/> property.
		/// </summary>
		public void UpdateGrounded() {
			Vector2 check = _body.position + _groundCheckOffset;
			var collider = Physics2D.OverlapPoint(check, _groundLayer);
			IsGrounded = _body.velocity.y <= 0f && collider != null;
		}

		protected virtual void OnDrawGizmosSelected() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere((Vector2)transform.position + _groundCheckOffset, 0.15f);
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere(_body.worldCenterOfMass, 0.1f);
		}

		#region Attack

		/// <summary>
		/// Whether the ninja is performing a melee attack.
		/// </summary>
		public bool IsAttackingMelee {
			get;
			set;
		}

		/// <summary>
		/// Whether the ninja is performing a range attack.
		/// </summary>
		public bool IsAttackingRange {
			get;
			set;
		}

		/// <summary>
		/// Perform a range attack.
		/// </summary>
		public virtual void PerformRangeAttack() {

		}

		/// <summary>
		/// Perform a melee attack.
		/// </summary>
		public virtual void PerformMeleeAttack() {

		}

		#endregion

	}
}
