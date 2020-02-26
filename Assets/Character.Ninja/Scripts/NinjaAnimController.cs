using UnityEngine;

namespace Assets.SpriteAnimations.Scripts {


	public class NinjaAnimController : MonoBehaviour {

		[SerializeField]
		Ninja _model = null;

		[SerializeField]
		Animator _animator = null;

		[SerializeField]
		SpriteRenderer _renderer = null;

		readonly static int LAYER_GROUND = 0;
		readonly static int LAYER_AIR = 1;
		readonly static int ANIM_RUNNING = Animator.StringToHash("Running");
		readonly static int ANIM_SWORD_ATTACKING = Animator.StringToHash("SwordAttacking");
		readonly static int ANIM_THROW_ATTACKING = Animator.StringToHash("ThrowAttacking");
		readonly static int ANIM_GROUNDED = Animator.StringToHash("Grounded");
		readonly static int ANIM_VERTICAL_VELOCITY = Animator.StringToHash("VerticalVelocity");
		readonly static int ANIM_SLIDING = Animator.StringToHash("Slide");
		readonly static int ANIM_KNOCKED_OUT = Animator.StringToHash("KnockedOut");
		readonly static int ANIM_REVIVING = Animator.StringToHash("Reviving");

		private void LateUpdate() {
			var ninja = _model;
			var animator = _animator;
			_renderer.flipX = ninja.IsFacingLeft;
			animator.SetBool(ANIM_KNOCKED_OUT, ninja.IsKnockedOut);
			animator.SetBool(ANIM_GROUNDED, ninja.IsGrounded);
			animator.SetBool(ANIM_RUNNING, ninja.IsRunning);
			animator.SetFloat(ANIM_VERTICAL_VELOCITY, ninja.Body.velocity.y);
			animator.SetBool(ANIM_SLIDING, ninja.IsSliding);
			animator.SetBool(ANIM_REVIVING, ninja.IsReviving);
			if (ninja.IsGrounded) {
				animator.SetLayerWeight(LAYER_GROUND, 1f);
				animator.SetLayerWeight(LAYER_AIR, 0f);
			} else {
				animator.SetLayerWeight(LAYER_GROUND, 0f);
				animator.SetLayerWeight(LAYER_AIR, 1f);
			}
			animator.SetBool(ANIM_SWORD_ATTACKING, ninja.IsAttackingMelee);
			animator.SetBool(ANIM_THROW_ATTACKING, ninja.IsAttackingRange);
		}

		private void OnRangeAttackCommenced() {
			_model.PerformRangeAttack();
		}

		private void OnMeleeAttackCommenced() {
			_model.PerformMeleeAttack();
		}

		private void OnMeleeAttackEnded() {
			_animator.SetBool(ANIM_SWORD_ATTACKING, false);
			_model.IsAttackingMelee = false;
		}

		private void OnRangeAttackEnded() {
			_animator.SetBool(ANIM_THROW_ATTACKING, false);
			_model.IsAttackingRange = false;
		}

		private void OnRevived() {
			_animator.SetBool(ANIM_REVIVING, false);
			_model.IsReviving = false;
			_model.IsKnockedOut = false;
		}

	}

}
