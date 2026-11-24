
using UnityEngine;
using TMPro;
using System.Collections;

public class MensagemMinhoca : MonoBehaviour
{
    public GameObject painelMensagem;
    public TMP_Text textoMensagem;

    public float velocidadeDigitacao = 0.05f;
    public float tempoEntreFalas = 1.5f;

    private bool mensagemJaApareceu = false;

    private void Start()
    {
        Debug.Log("MensagemMinhoca: script iniciado!");

        if (painelMensagem == null)
        {
            Debug.LogError("ERRO: Painel Mensagem não foi conectado!");
            return;
        }

        if (textoMensagem == null)
        {
            Debug.LogError("ERRO: Texto Mensagem não foi conectado!");
            return;
        }

        painelMensagem.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D outro)
    {
        Debug.Log("Algo entrou na área: " + outro.name);
        Debug.Log("Tag do objeto: " + outro.tag);

        if (outro.CompareTag("Player"))
        {
            Debug.Log("Jorge foi detectado!");

            if (!mensagemJaApareceu)
            {
                mensagemJaApareceu = true;
                painelMensagem.SetActive(true);

                StartCoroutine(MostrarFalas());
            }
        }
    }

    IEnumerator MostrarFalas()
    {
        textoMensagem.text = "";

        string primeiraFala =
            "Jorge encontrou uma minhoca estranha!";

        foreach (char letra in primeiraFala)
        {
            textoMensagem.text += letra;
            yield return new WaitForSeconds(velocidadeDigitacao);
        }

        yield return new WaitForSeconds(tempoEntreFalas);

        textoMensagem.text = "";

        string segundaFala = "O que significa isso?";

        foreach (char letra in segundaFala)
        {
            textoMensagem.text += letra;
            yield return new WaitForSeconds(velocidadeDigitacao);
        }

        yield return new WaitForSeconds(3f);

        painelMensagem.SetActive(false);
    }
}
