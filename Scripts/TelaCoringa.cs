using UnityEngine;

public class TelaCoringa : MonoBehaviour {

     float A;    
     float screemwidth;
     float screemheighth;

    // funcao aplica tamanho da tela redimensionada
    void Start()
    {
        A = transform.localScale.x;
    }
    // funcao calcula a proporcao da tela
	void Update ()
    {
        // recebe a altura e largura da tela
        screemwidth = Screen.width;
        screemheighth = Screen.height;
        
        // calcula o novo valor proporcional da tela
        transform.localScale = new Vector3((A * screemwidth / screemheighth), transform.localScale.y, transform.localScale.z);
    }
}
