using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Volume : MonoBehaviour
{
    public Slider Barravolume;
    public Text textoVol;
    public Button BotaoSalvarPref, BotaoVoltar,botaotext,botaocanvas;
    private float VOLUME;   
    public string nomeDaCena;
   
    // funcao descarta todos valores e preferencias salvas
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // funcao setup volume
    void Start()
    {
        // define valores defaut
        Cursor.visible = true;
        Time.timeScale = 1;
        Barravolume.minValue = 0;
        Barravolume.maxValue = 1;

        // busca o valor do slider e salva
        if (PlayerPrefs.HasKey("VOLUME"))
        {
            VOLUME = PlayerPrefs.GetFloat("VOLUME");
            Barravolume.value = VOLUME;
        }
        // atribui o valor total no volume
        else
        {
            PlayerPrefs.SetFloat("VOLUME", 1);
            Barravolume.value = 1;
        }
        // salva o valor escolhido no click do botao
        BotaoSalvarPref.onClick.AddListener(() => SalvarPreferencias());
        
    }
    // funcao ativa todos elementos da funcao global
    private void Opcoes(bool ativarOP)
    {
        textoVol.gameObject.SetActive(ativarOP);
        Barravolume.gameObject.SetActive(ativarOP);
        BotaoVoltar.gameObject.SetActive(ativarOP);
        botaotext.gameObject.SetActive(ativarOP);
        botaocanvas.gameObject.SetActive(ativarOP);
        BotaoSalvarPref.gameObject.SetActive(ativarOP);
    }

    // salva todas as alteracoes 
    private void SalvarPreferencias()
    {
        PlayerPrefs.SetFloat("VOLUME", Barravolume.value);
        VOLUME = PlayerPrefs.GetFloat("VOLUME");
    }

    // funcao pega novo valor de audio e descarta o antigo
    void Update()
    {
        if(SceneManager.GetActiveScene().name != nomeDaCena)
        {
            AudioListener.volume = VOLUME;
            Destroy(gameObject);
        }
    }
}
