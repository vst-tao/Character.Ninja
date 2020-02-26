using UnityEngine;

namespace Assets.SpriteAnimations.Scripts {

	public sealed class SampleNinja : Ninja {

		[SerializeField]
		NinjaKnife _knifePrefab = null;

		[SerializeField]
		Vector2 _knifeSpawnOffset = Vector2.zero;

		[SerializeField]
		float _knifeThrowForce = 5f;

		[SerializeField]
		float _knifeThrowAngle = 70f;

		[SerializeField]
		float _knifeAngularVelocity = 120f;

		[SerializeField]
		Vector2 _swordAttackOffset = new Vector2(0.5f, 0.75f);

		[SerializeField]
		float _swordAttackRadius = 0.5f;

		private Vector2 KnifeSpawnPosition {
			get {
				float multiplier = IsFacingLeft ? -1f : 1f;
				Vector2 offset = _knifeSpawnOffset;
				offset.x *= multiplier;
				return Body.position + offset;
			}
		}

		private Vector2 SwordAttackCenter {
			get {
				float multiplier = IsFacingLeft ? -1f : 1f;
				Vector2 offset = _swordAttackOffset;
				offset.x *= multiplier;
				return Body.position + offset;
			}
		}

		private float KnifeSpawnRotation {
			get {
				return IsFacingLeft ? _knifeThrowAngle : -_knifeThrowAngle;
			}
		}

		private float KnifeAngularVelocity {
			get {
				return IsFacingLeft ? _knifeAngularVelocity : -_knifeAngularVelocity;
			}
		}

		public override void PerformMeleeAttack() {
			var colliders = Physics2D.OverlapCircleAll(SwordAttackCenter, _swordAttackRadius);

		}

		public override void PerformRangeAttack() {
			var knife = Instantiate(_knifePrefab, KnifeSpawnPosition, Quaternion.Euler(0, 0, KnifeSpawnRotation));
			knife.Shoot(Body.velocity, KnifeAngularVelocity, _knifeThrowForce);
		}

		protected override void OnDrawGizmosSelected() {
			base.OnDrawGizmosSelected();
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(KnifeSpawnPosition, 0.05f);
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(SwordAttackCenter, _swordAttackRadius);
		}

	}
}
