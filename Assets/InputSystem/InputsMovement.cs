using UnityEngine;
using UnityEngine.InputSystem;


	public class InputsMovement : MonoBehaviour
	{
		
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool sprint;
		public float acceleration;

		private PlayerInput _PlayerInput;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = false;

#if ENABLE_INPUT_SYSTEM

		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

        public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnAcceleration(InputValue value)
		{
			AccelerationInput(value.Get<float>());
		}
#endif

	public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

        public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void AccelerationInput(float newAccelerationValue)
		{
			acceleration = newAccelerationValue;
		}

		//private void OnApplicationFocus(bool hasFocus)
		//{
		//	SetCursorState(cursorLocked);
		//}
	}
	
