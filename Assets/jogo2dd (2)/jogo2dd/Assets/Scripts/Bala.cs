using UnityEngine;

public class Bala : MonoBehaviour
{
    public int danoDaBala = 10;
    public float tempoDeVida = 3f;

    private bool jaBateu = false;

    void Start()
    {
        Destroy(gameObject, tempoDeVida);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // evita múltiplas colisões no mesmo frame
        if (jaBateu) return;
        jaBateu = true;

        // não acerta o player
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        // pega o script de vida
        Saude vida = collision.gameObject.GetComponent<Saude>();

        if (vida != null)
        {
            vida.dano(danoDaBala);
        }

        Destroy(gameObject);
    }
}