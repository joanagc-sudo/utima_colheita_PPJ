using UnityEngine;
using TMPro;

public class Pontos : MonoBehaviour
{
    public static Pontos gc;

    public TextMeshProUGUI coinsText;
    public int coins;

    void Awake()
    {
        if (gc == null)
        {
            gc = this;
        }
        else if (gc != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (coinsText != null)
        {
            coinsText.text = coins.ToString();
        }
    }
}