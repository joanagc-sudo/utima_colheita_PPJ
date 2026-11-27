using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaSuperPulo : MonoBehaviour
{
    public float forcaSuperPulo = 20f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);

                rb.AddForce(Vector2.up * forcaSuperPulo, ForceMode2D.Impulse);
            }
        }
    }
}