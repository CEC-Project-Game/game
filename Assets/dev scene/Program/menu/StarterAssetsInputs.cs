using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = false;
		public bool cursorInputForLook = true;

		void Update()
		{
			if(Input.GetKeyDown(KeyCode.Q))
        	{
				if (cursorLocked)
            	{
                	Unlock();

            	} else
            	{
                	Lock();
            	}
				
           
        	}
		}
		
		public void Lock()
		{
			Cursor.lockState = CursorLockMode.Locked;
			cursorLocked = true;
		}


		public void Unlock()
		{
			Cursor.lockState = CursorLockMode.None;
			cursorLocked = false;
		}


		
		
		private void OnApplicationFocus(bool hasFocus)
		{
			
			SetCursorState(cursorLocked);
            
            
        	
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}