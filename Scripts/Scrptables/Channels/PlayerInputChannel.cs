using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ArChi
{
	[CreateAssetMenu(fileName = "PlayerInputChannel", menuName = "Channels/Inputs/Player")]
	public class PlayerInputChannel : ScriptableObject
	{
		[SerializeField] PlayerInput playerInput;
		public event UnityAction<Vector2> moveEvent;
		public event UnityAction action;


		public void OnMove(InputAction.CallbackContext context)
		{
			if (moveEvent != null)
			{
				moveEvent.Invoke(context.ReadValue<Vector2>());
			}
		}
		public void OnAction(InputAction.CallbackContext context)
		{
			if (action != null
				&& context.phase == InputActionPhase.Performed)
				action.Invoke();
		}
	}
}