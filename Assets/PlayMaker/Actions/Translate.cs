// (c) Copyright HutongGames, LLC 2010-2011. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Translates a Game Object. Use a Vector3 variable and/or XYZ components. To leave any axis unchanged, set variable to 'None'.")]
	public class Translate : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The game object to translate.")]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("A translation vector. NOTE: You can override individual axis below.")]
		public FsmVector3 vector;
		
		[Tooltip("Translation along x axis.")]
		public FsmFloat x;

		[Tooltip("Translation along y axis.")]
		public FsmFloat y;

		[Tooltip("Translation along z axis.")]
		public FsmFloat z;

		[Tooltip("Translate in local or world space.")]
		public Space space;
		
		[Tooltip("Translate over one second")]
		public bool perSecond;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		[Tooltip("Perform the rotation in LateUpdate. This is useful if you want to override the rotation of objects that are animated or otherwise rotated in Update.")]
		public bool lateUpdate;		

		public override void Reset()
		{
			gameObject = null;
			vector = null;
			// default axis to variable dropdown with None selected.
			x = new FsmFloat { UseVariable = true };
			y = new FsmFloat { UseVariable = true };
			z = new FsmFloat { UseVariable = true };
			space = Space.Self;
			perSecond = true;
			everyFrame = true;
			lateUpdate = false;
		}

		public override void OnEnter()
		{
			if (!everyFrame && !lateUpdate)
			{
				DoTranslate();
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (!lateUpdate)
			{
				DoTranslate();
			}
		}

		public override void OnLateUpdate()
		{
			if (lateUpdate)
			{
				DoTranslate();
			}

			if (!everyFrame)
			{
				Finish();
			}
		}

		void DoTranslate()
		{
			// init

			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			// Use vector if specified

			var translate = vector.IsNone ? new Vector3(x.Value, y.Value, z.Value) : vector.Value;

			// override any axis

			if (!x.IsNone) translate.x = x.Value;
			if (!y.IsNone) translate.y = y.Value;
			if (!z.IsNone) translate.z = z.Value;
		
			// apply
			
			if (!perSecond)
			{
				go.transform.Translate(translate, space);
			}
			else
			{
				go.transform.Translate(translate * Time.deltaTime, space);
			}
		}

	}
}