using UnityEngine;

public class AbriraPorta : MonoBehaviour
{
    public Animator animatorPorta; // Arraste a porta aqui no Inspetor

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se o player encostar no gatilho, a porta abre
        if (collision.CompareTag("Player"))
        {
            if (animatorPorta != null)
            {
                animatorPorta.SetTrigger("Abrir");
            }
        }
    }
}