using UnityEngine;

namespace Assets.SpriteAnimations.Scripts {

	public sealed class NinjaBehaviourController : MonoBehaviour {

		[SerializeField]
		Ninja _model = null;

		[SerializeField]
		[ContextMenuItem("Use control preset 1", "UsePreset1")]
		[ContextMenuItem("Use control preset 2", "UsePreset2")]
		NinjaControlParams _controlKeys = NinjaControlParams.Preset1;

		private void Awake() {
			var body = _model.Body;
			body.drag = 0f;
			body.constraints = RigidbodyConstraints2D.FreezeRotation;
		}

		private void Update() {

			var keys = _controlKeys;
			var ninja = _model;
			var body = ninja.Body;

			if (ninja.IsGrounded) {
				if (ninja.IsAttackingMelee || ninja.IsAttackingRange) {
					return;
				}
				if (Input.GetKeyDown(keys.SwordAttack) && !ninja.IsSliding) {
					ninja.IsAttackingMelee = true;
					ninja.IsRunning = false;
				} else if (Input.GetKeyDown(keys.RangeAttack) && !ninja.IsSliding) {
					ninja.IsAttackingRange = true;
					ninja.IsRunning = false;
				} else {
					// when attacking with a sword, ninja cannot run
					// jump or slide
					if (Input.GetKey(keys.MoveLeft)) {
						ninja.IsRunning = true;
						ninja.IsFacingLeft = true;
						ninja.IsSliding = Input.GetKey(keys.Slide);
					} else if (Input.GetKey(keys.MoveRight)) {
						ninja.IsRunning = true;
						ninja.IsFacingLeft = false;
						ninja.IsSliding = Input.GetKey(keys.Slide);
					} else {
						ninja.IsRunning = false;
						ninja.IsSliding = false;
					}
					if (!ninja.IsSliding && Input.GetKeyDown(keys.Jump)) {
						body.AddForce(Vector2.up * ninja.JumpForce, ForceMode2D.Impulse);
					}
				}
			} else {
				ninja.IsRunning = false;
				if (ninja.IsAttackingMelee || ninja.IsAttackingRange) {
					return;
				}
				if (Input.GetKeyDown(keys.SwordAttack)) {
					ninja.IsAttackingMelee = true;
				} else if (Input.GetKeyDown(keys.RangeAttack)) {
					ninja.IsAttackingRange = true;
				} else {
					if (Input.GetKey(keys.MoveLeft)) {
						ninja.IsFacingLeft = true;
					} else if (Input.GetKey(keys.MoveRight)) {
						ninja.IsFacingLeft = false;
					}
				}
			}

			ninja.UpdateGrounded();

		}

		private void FixedUpdate() {
			var ninja = _model;
			if (ninja.IsKnockedOut) {
				return;
			}
			var body = ninja.Body;
			if (ninja.IsRunning) {
				if (ninja.IsSliding) {
					Vector2 resistant = Vector2.zero;
					resistant.x = ninja.CalculateSlideDrag(body.velocity.x);
					body.AddForce(resistant);
				} else {
					float modifier = ninja.IsFacingLeft ? -1 : 1;
					Vector2 force = new Vector2(modifier * ninja.RunForce, 0f);
					force.x += ninja.CalculateRunDrag(body.velocity.x);
					body.AddForce(force);
				}
			} else {
				if (ninja.IsGrounded) {
					// braking force can only be applied if the ninja
					// is on the ground
					Vector2 brakingForce = Vector2.zero;
					brakingForce.x = ninja.CalculateBrakingForce(body.velocity.x);
					body.AddForce(brakingForce);
				} else {
					// mid-air...
					float vx = body.velocity.x;
					if (ninja.IsFacingLeft && vx > 0f) {
						// ninja is facing left but is moving in air
						// to the right
						body.AddForce(new Vector2(-ninja.MidairFlyForce, 0f));
					} else if (!ninja.IsFacingLeft && vx < 0f) {
						// ninja is facing right but is moving in air
						// to the left
						body.AddForce(new Vector2(ninja.MidairFlyForce, 0f));
					}
					if (Input.GetKey(_controlKeys.Jump) && body.velocity.y > 0f) {
						// if player continues pressing the jump button, we allow
						// the ninja to reach a slighly higher altitude by applying a
						// small upward force
						body.AddForce(Vector2.up * ninja.HoverForce);
					}
				}
			}
		}

		private void UsePreset1() {
			_controlKeys = NinjaControlParams.Preset1;
		}

		private void UsePreset2() {
			_controlKeys = NinjaControlParams.Preset2;
		}

	}
}
