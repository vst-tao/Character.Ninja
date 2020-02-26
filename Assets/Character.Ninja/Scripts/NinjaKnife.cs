using UnityEngine;

namespace Assets.SpriteAnimations.Scripts {

	public sealed class NinjaKnife : MonoBehaviour {

		[SerializeField]
		Rigidbody2D _body = null;

		public float Rotation {
			set {
				_body.MoveRotation(value);
			}
		}

		public Vector2 Velocity {
			get {
				return _body.velocity;
			}
		}

		public void Shoot(Vector2 initialVelocity, float angularVelocity, float forceMagnitude) {
			_body.velocity = initialVelocity;
			_body.angularVelocity = angularVelocity;
			_body.AddForce(transform.up * forceMagnitude, ForceMode2D.Impulse);
		}

	}
}
