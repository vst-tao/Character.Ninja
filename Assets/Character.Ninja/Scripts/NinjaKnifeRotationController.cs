using UnityEngine;

namespace Assets.SpriteAnimations.Scripts {

	public sealed class NinjaKnifeRotationController : MonoBehaviour {

		[SerializeField]
		NinjaKnife _model = null;

		private void Update() {
			//Vector3 dir = _target.position - transform.position;
			Vector3 dir = transform.right;
			float atan2 = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			_model.Rotation = atan2;
		}

	}
}
