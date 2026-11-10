
using System.Collections;
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
        "Ah! bom dia Jorge, estava esperando por você!",
        "Sim, senhor! Cheguei agora mesmo. É um prazer conhecê-lo!",
        "Seja bem-vindo! Confesso que as coisas por aqui andam melhores do que nunca",
        "Talvez seja o novo caseiro trazendo boas energias para a fazenda",
        "Percebi! Nunca vi uma fazenda com plantações tão bonitas e abundantes!",
        "Hehehe! É uma maravilha, não é? Há pouco tempo, mal conseguíamos colher o suficiente para nos manter",
        "Sério? E o que aconteceu para tudo mudar assim?",
        "Ah.. isso é uma longa história! O importante é que agora temos muito trabalho pela frente!",
        "Pode deixar comigo, senhor! Por onde devo começar?",
        "Quero que recolha dez itens de milho e trigo antes do anoitecer. Preciso deles para organizar a produção de amanhã.",
        "Dez itens? Pode deixar! Vou começar agora mesmo!"
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
    private bool cenaIniciada = false;

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

        cenaIniciada = true;

        MostrarFala();
    }

    void MostrarFala()
    {
        // Verifica se todos os diálogos terminaram
        if (indiceFala >= falas.Length)
        {
            FinalizarCutScene();
            return;
        }

        if (efeitoDigitacao != null)
        {
            StopCoroutine(efeitoDigitacao);
            efeitoDigitacao = null;
        }

        efeitoDigitacao = StartCoroutine(
            DigitarTexto(falas[indiceFala])
        );
    }

    IEnumerator DigitarTexto(string fala)
    {
        digitando = true;

        // Define quem está falando
        animacao.falando = falasFazendeiro[indiceFala];

        Texto.text = "";

        foreach (char letra in fala)
        {
            Texto.text += letra;

            yield return new WaitForSeconds(0.04f);
        }

        digitando = false;

        animacao.falando = false;

        efeitoDigitacao = null;
    }

    void AvancarDialogo()
    {
        if (!cenaIniciada)
            return;

        if (TelaPreta.gameObject.activeSelf)
            return;

        // Se ainda está digitando, mostra a frase inteira
        if (digitando)
        {
            if (efeitoDigitacao != null)
            {
                StopCoroutine(efeitoDigitacao);
                efeitoDigitacao = null;
            }

            Texto.text = falas[indiceFala];

            digitando = false;
            animacao.falando = false;
        }
        else
        {
            // Passa para a próxima fala
            indiceFala++;

            MostrarFala();
        }
    }

    void FinalizarCutScene()
    {
        Debug.Log("Diálogos finalizados!");
        Debug.Log("Carregando cena: " + nomeCenaJogo);

        animacao.falando = false;

        SceneManager.LoadScene(nomeCenaJogo);
    }
}
