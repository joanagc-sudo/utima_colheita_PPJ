using System.Collections;
using UnityEngine;

public class PocaoSuperPulo : MonoBehaviour
{
    public float multiplicadorPulo = 2f;
    public float duracao = 5f;

    private void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Player"))
        {
            Controle controle = outro.GetComponent<Controle>();

            if (controle != null)
            {
                StartCoroutine(AumentarPulo(controle));
            }

            // Esconde a poção
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    IEnumerator AumentarPulo(Controle jogador)
    {
        int puloOriginal = jogador.forcaDoPulo;

        // aumenta o pulo
        jogador.forcaDoPulo = Mathf.RoundToInt(jogador.forcaDoPulo * multiplicadorPulo);

        yield return new WaitForSeconds(duracao);

        // volta ao normal
        jogador.forcaDoPulo = puloOriginal;

        Destroy(gameObject);
    }
}