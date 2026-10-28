using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaInimigo : MonoBehaviour
{
    public int dano = 10;

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Player"))
        {
            Saude vida = outro.GetComponent<Saude>();

            if (vida != null)
            {
                vida.dano(dano);
            }

            Destroy(gameObject);
        }

        // opcional: destruir ao bater em parede
        if (outro.gameObject.layer == LayerMask.NameToLayer("Obstaculo"))
        {
            Destroy(gameObject);
        }
    }
}