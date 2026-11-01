using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CutScene1 : MonoBehaviour
{
  public Image TelaPreta;
  public TMP_Text Texto;

  public string nomeCenaJogo = "SampleScene";

  private string[] falas = {
    "Olá, que belo dia hoje, não?",
    "Hoje terá muito trabalho a se realizar haha",
    "Que maravilha essa fartura, a anos não vejo a fazenda tão radiante assim!",
    "Então...vamos ao trabalho"};

  private int indiceFala = 0;
  private bool digitando = false;
  private Coroutine efeitoDigitacao;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExecutarCutScene());
    }

     void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            AvancarDialogo();
        }
    }
    IEnumerator ExecutarCutScene(){
        TelaPreta.gameObject.SetActive(true);
        Texto.text = "";

        yield return new WaitForSeconds(1f);

        Color cor = TelaPreta.color;

        for(float t = 0; t < 1f; t+= Time.deltaTime){
            cor.a = 1f - t;
            TelaPreta.color = cor;
            yield return null;
        }
        cor.a = 0f;
        TelaPreta.color = cor;

        TelaPreta.gameObject.SetActive(false);
        MostrarFala();

    }

    void MostrarFala(){
        if(indiceFala >= falas.Length){
            SceneManager.LoadScene(nomeCenaJogo);
            return;
        }
        if (efeitoDigitacao != null){
            StopCoroutine(efeitoDigitacao);
        }
        efeitoDigitacao = StartCoroutine(DigitarTexto(falas[indiceFala]));
    }

        IEnumerator DigitarTexto (string fala){
            digitando = false;
            Texto.text = "";

            foreach(char letra in fala){
                Texto.text += letra;
            yield return new WaitForSeconds(0.04f);
        }
        digitando = false;
        efeitoDigitacao = null;
}
void AvancarDialogo(){
if (TelaPreta.gameObject.activeSelf){
    return;
}
if (digitando){
    StopCoroutine(efeitoDigitacao);
    Texto.text = falas [indiceFala];

    digitando = false;
    efeitoDigitacao = null;
}
   else {
    indiceFala++;
    MostrarFala();
   }
}
}

