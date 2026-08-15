using UnityEngine;
using UnityEngine.UI;

public class Reel : MonoBehaviour
{
    public Sprite[] symbols;

    private Image image;

    private bool isSpinning = false;

    public float changeInterval = 0.1f;

    private float timer = 0f;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (isSpinning)
        {
            timer += Time.deltaTime;

            if (timer >= changeInterval)
            {
                timer = 0f;

                int index = Random.Range(0, symbols.Length);
                image.sprite = symbols[index];
            }
        }
    }

    public void StartSpin()
    {
        isSpinning = true;
    }

    public void StopSpin()
    {
        isSpinning = false;
    }
}