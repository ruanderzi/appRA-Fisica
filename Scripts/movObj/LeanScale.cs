using UnityEngine;

namespace Lean.Touch
{
    // funcao que permite escalar o GameObject atual
	public class LeanScale : MonoBehaviour
	{
		public bool IgnoreStartedOverGui = true;
		public bool IgnoreIsOverGui;
		public int RequiredFingerCount;
		public LeanSelectable RequiredSelectable;
		public Camera Camera;
		[Range(-1.0f, 1.0f)]
		public float WheelSensitivity;
		public bool Relative;
		public bool ScaleClamp;
		public Vector3 ScaleMin;
		public Vector3 ScaleMax;

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

            // calcula pinchScale e verifique se ela é válida
            var pinchScale = LeanGesture.GetPinchScale(fingers, WheelSensitivity);

			if (pinchScale != 1.0f)
			{
                // realiza a translacao se esta for uma escala relativa
                if (Relative == true)
				{
					var pinchScreenCenter = LeanGesture.GetScreenCenter(fingers);

					if (transform is RectTransform)
					{
						TranslateUI(pinchScale, pinchScreenCenter);
					}
					else
					{
						Translate(pinchScale, pinchScreenCenter);
					}
				}
                
                // realiza o dimensionamento
                Scale(transform.localScale * pinchScale);
			}
		}

		protected virtual void TranslateUI(float pinchScale, Vector2 pinchScreenCenter)
		{
            // posição da tela da transformação
            var screenPoint = RectTransformUtility.WorldToScreenPoint(Camera, transform.position);
            
            // afasta a posição da tela do ponto de referência com base na escala
            screenPoint.x = pinchScreenCenter.x + (screenPoint.x - pinchScreenCenter.x) * pinchScale;
			screenPoint.y = pinchScreenCenter.y + (screenPoint.y - pinchScreenCenter.y) * pinchScale;

            // converte de volta ao espaço mundial
            var worldPoint = default(Vector3);

			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, screenPoint, Camera, out worldPoint) == true)
			{
				transform.position = worldPoint;
			}
		}

		protected virtual void Translate(float pinchScale, Vector2 screenCenter)
		{
            // certifica de que a câmera existe
            var camera = LeanTouch.GetCamera(Camera, gameObject);

			if (camera != null)
			{
                // pega posição da tela da transformação
                var screenPosition = camera.WorldToScreenPoint(transform.position);
                
                // afasta a posição da tela do ponto de referência com base na escala
                screenPosition.x = screenCenter.x + (screenPosition.x - screenCenter.x) * pinchScale;
				screenPosition.y = screenCenter.y + (screenPosition.y - screenCenter.y) * pinchScale;

                // Converter de volta ao espaço mundial
                transform.position = camera.ScreenToWorldPoint(screenPosition);
			}
		}

		protected virtual void Scale(Vector3 scale)
		{
			if (ScaleClamp == true)
			{
                scale.x = Mathf.Clamp(scale.x, ScaleMin.x, ScaleMax.x);
                scale.y = Mathf.Clamp(scale.y, ScaleMin.y, ScaleMax.y);
                scale.z = Mathf.Clamp(scale.z, ScaleMin.z, ScaleMax.z);
			}

			transform.localScale = scale;
		}
	}
}