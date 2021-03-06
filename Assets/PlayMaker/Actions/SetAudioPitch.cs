// (c) Copyright HutongGames, LLC 2010-2011. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Sets the Pitch of the Audio Clip played by the AudioSource component on a Game Object.")]
	public class SetAudioPitch : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault gameObject;
		public FsmFloat pitch;
		public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			pitch = 1;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetAudioPitch();
			
			if (!everyFrame)
				Finish();
		}
				
		public override void OnUpdate ()
		{
			DoSetAudioPitch();
		}	
		
		void DoSetAudioPitch()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				var audio = go.audio;
				if (audio != null)
				{
					if (!pitch.IsNone)
						audio.pitch = pitch.Value;
				}
			}
		}
	}
}