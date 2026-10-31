using UnityEngine;
using UnityEngine.EventSystems;

public class ControleMobile : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler
{
    public enum TipoBotao
    {
        Esquerda,
        Direita,
        Pulo,
        Tiro
    }

    public TipoBotao tipo;

    public static bool esquerda;
    public static bool direita;
    public static bool pulo;
    public static bool tiro;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (tipo == TipoBotao.Esquerda)
            esquerda = true;

        if (tipo == TipoBotao.Direita)
            direita = true;

        if (tipo == TipoBotao.Pulo)
            pulo = true;

        if (tipo == TipoBotao.Tiro)
            tiro = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (tipo == TipoBotao.Esquerda)
            esquerda = false;

        if (tipo == TipoBotao.Direita)
            direita = false;

        if (tipo == TipoBotao.Pulo)
            pulo = false;

        if (tipo == TipoBotao.Tiro)
            tiro = false;
    }
}