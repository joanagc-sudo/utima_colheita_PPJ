using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script : MonoBehaviour
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