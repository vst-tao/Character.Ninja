using UnityEngine;

namespace Assets.SpriteAnimations.Scripts {

	/// <summary>
	/// Struct holding all keys for controlling a ninja.
	/// </summary>
	[System.Serializable]
	public struct NinjaControlParams {

		[SerializeField]
		KeyCode _moveLeft;

		[SerializeField]
		KeyCode _moveRight;

		[SerializeField]
		KeyCode _jump;

		[SerializeField]
		KeyCode _slide;

		[SerializeField]
		KeyCode _swordAttack;

		[SerializeField]
		KeyCode _rangeAttack;

		[SerializeField]
		KeyCode _glide;

		public static NinjaControlParams Preset1 {
			get {
				return new NinjaControlParams() {
					_moveLeft = KeyCode.LeftArrow,
					_moveRight = KeyCode.RightArrow,
					_jump = KeyCode.UpArrow,
					_slide = KeyCode.DownArrow,
					_swordAttack = KeyCode.RightControl,
					_rangeAttack = KeyCode.RightShift,
					_glide = KeyCode.DownArrow
				};
			}
		}

		public static NinjaControlParams Preset2 {
			get {
				return new NinjaControlParams() {
					_moveLeft = KeyCode.A,
					_moveRight = KeyCode.D,
					_jump = KeyCode.W,
					_slide = KeyCode.S,
					_swordAttack = KeyCode.LeftControl,
					_rangeAttack = KeyCode.LeftShift,
					_glide = KeyCode.S
				};
			}
		}

		public KeyCode MoveLeft {
			get {
				return _moveLeft;
			}
		}

		public KeyCode MoveRight {
			get {
				return _moveRight;
			}
		}

		public KeyCode Jump {
			get {
				return _jump;
			}
		}

		public KeyCode Slide {
			get {
				return _slide;
			}
		}

		public KeyCode SwordAttack {
			get {
				return _swordAttack;
			}
		}

		public KeyCode RangeAttack {
			get {
				return _rangeAttack;
			}
		}

		public KeyCode Glide {
			get {
				return _glide;
			}
		}

	}
}
