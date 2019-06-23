using UnityEngine;

namespace Lean.Touch
{
	// funcao que permite move o gameObject em relacao a camera
	public class LeanTranslate : MonoBehaviour
	{
		public bool IgnoreStartedOverGui = true;
		public bool IgnoreIsOverGui;
		public int RequiredFingerCount;
		public LeanSelectable RequiredSelectable;
		public Camera Camera;

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

			// calcular o valor screenDelta com base nesses dedos
			var screenDelta = LeanGesture.GetScreenDelta(fingers);

			if (screenDelta != Vector2.zero)
			{
				// realiza a movimentacao
				if (transform is RectTransform)
				{
					TranslateUI(screenDelta);
				}
				else
				{
					Translate(screenDelta);
				}
			}
		}

		protected virtual void TranslateUI(Vector2 screenDelta)
		{
			// posicao da tela de tranformacao
			var screenPoint = RectTransformUtility.WorldToScreenPoint(Camera, transform.position);

			// adiciona o deltaPosicao
			screenPoint += screenDelta;

			// converte de volta ao espaco na tela
			var worldPoint = default(Vector3);

			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, screenPoint, Camera, out worldPoint) == true)
			{
				transform.position = worldPoint;
			}
		}

		protected virtual void Translate(Vector2 screenDelta)
		{
			// verifica se a camera existe
			var camera = LeanTouch.GetCamera(Camera, gameObject);

			if (camera != null)
			{
                // posicao da tela de tranformacao
                var screenPoint = camera.WorldToScreenPoint(transform.position);

                // adiciona o deltaPosicao
                screenPoint += (Vector3)screenDelta;

                // converte de volta ao espaco na tela
                transform.position = camera.ScreenToWorldPoint(screenPoint);
			}
		}
	}
}