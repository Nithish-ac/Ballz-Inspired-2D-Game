using UnityEngine;

public class Gameover : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Touchline")
        {
            GameManager.Instance.GameOver();
        }
    }
}
