using UnityEngine;
using System.Collections;
using TMPro;

public class CristalVitoria : MonoBehaviour
{
    public GameObject painelVitoria;
    public GameObject painelCreditos;

    public TextMeshProUGUI textoVitoria;

    bool venceu = false;

    private void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Player") && !venceu)
        {
            venceu = true;

            Animator anim = outro.GetComponent<Animator>();

            if (anim != null)
            {
                anim.SetTrigger("Vitoria");
            }

            painelVitoria.SetActive(true);

            textoVitoria.text = "VOCÊ VENCEU!";

            // esconde cristal sem destruir
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            StartCoroutine(MostrarCreditos());
        }
    }

    IEnumerator MostrarCreditos()
    {
        Debug.Log("Esperando 3 segundos");

        yield return new WaitForSeconds(3f);

        Debug.Log("Mostrando créditos");

        painelVitoria.SetActive(false);

        painelCreditos.SetActive(true);
    }
}