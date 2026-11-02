
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

    // Animação do fazendeiro
    public AnimacaoFala animacao;

    public string nomeCenaJogo = "SampleScene";

    private string[] falas = {
        "Ah! bom dia Jorge, estava esperando por você!", // Fazendeiro
        "Sim, senhor! Cheguei agora mesmo. É um prazer conhecê-lo!", // Caseiro
        "Seja bem-vindo! Confesso que as coisas por aqui andam melhores do que nunca", // Fazendeiro
        "Talvez seja o novo caseiro trazendo boas energias para a fazenda", // Fazendeiro
        "Percebi! Nunca vi uma fazenda com plantações tão bonitas e abundantes!", // Caseiro
        "Hehehe! É uma maravilha, não é? Há pouco tempo, mal conseguíamos colher o suficiente para nos manter", // Fazendeiro
        "Sério? E o que aconteceu para tudo mudar assim?", // Caseiro
        "Ah.. isso é uma longa história! O importante é que agora temos muito trabalho pela frente!", // Fazendeiro
        "Pode deixar comigo, senhor! Por onde devo começar?", // Caseiro
        "Quero que recolha dez itens de milho e trigo antes do anoitecer. Preciso deles para organizar a produção de amanhã.", // Fazendeiro
        "Dez itens? Pode deixar! Vou começar agora mesmo!" // Caseiro
    };

    // true = Fazendeiro | false = Caseiro
    private bool[] falasFazendeiro = {
        true,
        false,
        true,
        true,
        false,
        true,
        false,
        true,
        false,
        true,
        false
    };

    private int indiceFala = 0;
    private bool digitando = false;
    private Coroutine efeitoDigitacao;

    void Start()
    {
        StartCoroutine(ExecutarCutScene());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AvancarDialogo();
        }
    }

    IEnumerator ExecutarCutScene()
    {
        animacao.falando = false;
        TelaPreta.gameObject.SetActive(true);
        Texto.text = "";

        yield return new WaitForSeconds(1f);

        Color cor = TelaPreta.color;

        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            cor.a = 1f - t;
            TelaPreta.color = cor;
            yield return null;
        }

        cor.a = 0f;
        TelaPreta.color = cor;

        TelaPreta.gameObject.SetActive(false);

        MostrarFala();
    }

    void MostrarFala()
    {
        if (indiceFala >= falas.Length)
        {
            animacao.falando = false;

            SceneManager.LoadScene(nomeCenaJogo);
            return;
        }

        if (efeitoDigitacao != null)
        {
            StopCoroutine(efeitoDigitacao);
        }

        efeitoDigitacao = StartCoroutine(
            DigitarTexto(falas[indiceFala])
        );
    }

    IEnumerator DigitarTexto(string fala)
    {
        digitando = true;

        // Verifica se a fala pertence ao fazendeiro
        animacao.falando = falasFazendeiro[indiceFala];

        Texto.text = "";

        foreach (char letra in fala)
        {
            Texto.text += letra;

            yield return new WaitForSeconds(0.04f);
        }

        // Terminou de digitar
        digitando = false;

        // Fecha a boca do fazendeiro
        animacao.falando = false;

        efeitoDigitacao = null;
    }

    void AvancarDialogo()
    {
        if (TelaPreta.gameObject.activeSelf)
        {
            return;
        }

        if (digitando)
        {
            StopCoroutine(efeitoDigitacao);

            Texto.text = falas[indiceFala];

            digitando = false;

            // Fecha a boca ao pular o texto
            animacao.falando = false;

            efeitoDigitacao = null;
        }
        else
        {
            indiceFala++;

            MostrarFala();
        }
    }
}
