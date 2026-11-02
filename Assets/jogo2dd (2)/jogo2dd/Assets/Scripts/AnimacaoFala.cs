
using UnityEngine;
using UnityEngine.UI;

public class AnimacaoFala : MonoBehaviour
{
    public Image personagem;

    public Sprite bocaFechada;
    public Sprite bocaAberta;
    public Sprite bocaAberta2;

    public bool falando = false;

    private float tempo = 0;
    public float intervalo = 0.15f;

    private bool segundoFrame = false;

    void Update()
    {
        if (falando)
        {
            tempo += Time.deltaTime;

            if (tempo >= intervalo)
            {
                tempo = 0;
                segundoFrame = !segundoFrame;

                personagem.sprite = segundoFrame
                    ? bocaAberta
                    : bocaAberta2;
            }
        }
        else
        {
            personagem.sprite = bocaFechada;

            segundoFrame = false;
            tempo = 0;
        }
    }
}
