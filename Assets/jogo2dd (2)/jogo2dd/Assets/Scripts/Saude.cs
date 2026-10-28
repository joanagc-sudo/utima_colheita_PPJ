using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Saude : MonoBehaviour
{
    public bool morto;
    public int saude = 100;

    [Header("Tela de morte")]
    public GameObject painelMorte;
    public TextMeshProUGUI textoMorte;

    private Animator animator;

    // START
    void Start()
    {
        morto = false;
        animator = GetComponent<Animator>();
    }

    // DANO NORMAL
    public void dano(int x)
    {
        if (morto)
            return;

        saude -= x;

        Debug.Log(gameObject.name + " Vida: " + saude);

        if (saude <= 0)
        {
            morrer();
        }
    }

    // DANO INSTANTÂNEO
    public void danoMax()
    {
        if (morto)
            return;

        saude = 0;

        morrer();
    }

    // MORTE
    void morrer()
    {
        morto = true;

        // PARA MOVIMENTO
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }

        // ANIMAÇÃO
        if (animator != null)
        {
            animator.SetTrigger("Morte");
        }

        // PLAYER
        if (CompareTag("Player"))
        {
            mostrarTelaMorte();

            StartCoroutine(reiniciarFase());
        }
        else
        {
            // INIMIGO
            StartCoroutine(destruirInimigo());
        }
    }

    // TELA DE MORTE
    void mostrarTelaMorte()
    {
        if (painelMorte != null)
        {
            painelMorte.SetActive(true);
        }

        if (textoMorte != null)
        {
            textoMorte.text = "VOCÊ MORREU";
        }
    }

    // REINICIA FASE
    IEnumerator reiniciarFase()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // DESTROI INIMIGO
    IEnumerator destruirInimigo()
    {
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }
}