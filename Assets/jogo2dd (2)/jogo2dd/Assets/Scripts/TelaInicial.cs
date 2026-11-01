using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script : MonoBehaviour
{
  public void inicarJogo(){
    SceneManager.LoadScene("CutSceneInicial");
  }
  public void  fecharJogo(){
    Application.Quit();
  }
}
