
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatGira : MonoBehaviour
{
    [Header("Configurações da ponte")]
    public float anguloAberto = 90f;
    public float velocidade = 60f;
    public float tempoParaAbrir = 5f;
    public float tempoAberta = 3f;

    private Rigidbody2D rb;
    private float rotacaoFechada;
    private float rotacaoAberta;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;

        rotacaoFechada = rb.rotation;
        rotacaoAberta = rotacaoFechada + anguloAberto;

        StartCoroutine(ControlarPonte());
    }

    IEnumerator ControlarPonte()
    {
        while (true)
        {
            yield return new WaitForSeconds(tempoParaAbrir);

            yield return GirarPonte(rotacaoAberta);

            yield return new WaitForSeconds(tempoAberta);

            yield return GirarPonte(rotacaoFechada);
        }
    }

    IEnumerator GirarPonte(float destino)
    {
        while (Mathf.Abs(Mathf.DeltaAngle(rb.rotation, destino)) > 0.1f)
        {
            float novaRotacao = Mathf.MoveTowardsAngle(
                rb.rotation,
                destino,
                velocidade * Time.fixedDeltaTime
            );

            rb.MoveRotation(novaRotacao);

            yield return new WaitForFixedUpdate();
        }

        rb.MoveRotation(destino);
    }
}
