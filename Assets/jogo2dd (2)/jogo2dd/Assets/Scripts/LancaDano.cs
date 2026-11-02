
using System.Collections.Generic;
using UnityEngine;

public class LancaDano : MonoBehaviour
{
    public int dano = 20;

    private Collider2D colisor;

    private HashSet<Saude> inimigosAtingidos =
        new HashSet<Saude>();

    void Awake()
    {
        colisor = GetComponent<Collider2D>();

        // Começa desativado
        colisor.enabled = false;
    }

    // ATIVA A ÁREA DE DANO
    public void iniciarAtaque()
    {
        inimigosAtingidos.Clear();
        colisor.enabled = true;
    }

    // DESATIVA A ÁREA DE DANO
    public void finalizarAtaque()
    {
        colisor.enabled = false;
    }

    // DETECTA O INIMIGO
    private void OnTriggerEnter2D(Collider2D outro)
    {
        Saude vida = outro.GetComponentInParent<Saude>();

        if (vida == null)
            return;

        // Não atinge a própria Gina
        if (vida.transform.root == transform.root)
            return;

        // Não causa dano repetido no mesmo golpe
        if (inimigosAtingidos.Contains(vida))
            return;

        inimigosAtingidos.Add(vida);

        vida.dano(dano);

        Debug.Log("Inimigo atingido pela lança!");
    }
}
