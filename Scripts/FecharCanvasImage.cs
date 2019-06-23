using UnityEngine;
using Vuforia;

public class FecharCanvasImage : MonoBehaviour, ITrackableEventHandler
{
    [SerializeField] private TrackableBehaviour trackableBehaviour;
    [SerializeField] private GameObject canvasControllers;

    // funcao guarda todos eventos dentro do image target
    private void Start()
    {
        trackableBehaviour.RegisterTrackableEventHandler(this);
    }

    // funcao recebe a acao do image target
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {   
        // atualiza o status do reconhecimento
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            // desativa o canvas da tela
            canvasControllers.SetActive(false);
        }
    }
}