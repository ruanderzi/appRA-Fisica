using UnityEngine;

public class TrocaCenas : MonoBehaviour
{
    // faz carregamento e trocas de telas no app
    public void Camera()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("camera");
    }
    public void Menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("menu");
    }
    public void Sobrera()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("sobre RA");
    }
    public void Opcoes()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("opcoes");
    }
    public void Videos()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("videos");
    }

    // funcao sair do app
    public void Sair()
    {
        Application.Quit();
    }
}