using UnityEngine;

namespace Lean.Touch
{
    // funcao que gira o GameObject atual em torno de um eixo local espec�fico usando tor��es de dedo
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
 
            // Calcula os valores de rota��o baseados nesses dedos
            var twistDegrees = LeanGesture.GetTwistDegrees(fingers);

            // Realiza rota��o
            transform.Rotate(Axis, twistDegrees, Space);
		}
	}
}