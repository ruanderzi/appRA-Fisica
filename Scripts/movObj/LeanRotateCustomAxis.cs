using UnityEngine;

namespace Lean.Touch
{
    // funcao que gira o GameObject atual em torno de um eixo local específico usando torções de dedo
	public class LeanRotateCustomAxis : MonoBehaviour
	{
		public bool IgnoreStartedOverGui = true;
		public bool IgnoreIsOverGui;
		public int RequiredFingerCount;
		public LeanSelectable RequiredSelectable;
		public Vector3 Axis = Vector3.down;
		public Space Space = Space.Self;

#if UNITY_EDITOR
		protected virtual void Reset()
		{
			Start();
		}
#endif

		protected virtual void Start()
		{
			if (RequiredSelectable == null)
			{
				RequiredSelectable = GetComponent<LeanSelectable>();
			}
		}

		protected virtual void Update()
		{
            // pega os dedos que queremos usar
            var fingers = LeanSelectable.GetFingers(IgnoreStartedOverGui, IgnoreIsOverGui, RequiredFingerCount, RequiredSelectable);
 
            // Calcula os valores de rotação baseados nesses dedos
            var twistDegrees = LeanGesture.GetTwistDegrees(fingers);

            // Realiza rotação
            transform.Rotate(Axis, twistDegrees, Space);
		}
	}
}