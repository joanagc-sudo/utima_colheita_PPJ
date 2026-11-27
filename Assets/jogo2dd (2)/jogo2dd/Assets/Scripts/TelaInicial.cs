using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaInicial : MonoBehaviour
{
    public void inicarJogo()
    {
        Debug.Log("Botão de iniciar foi clicado!");

        SceneManager.LoadScene("CutSceneInicial");
    }

    public void fecharJogo()
    {
        Application.Quit();
    }
}