using UnityEngine;

public class BallReturn : MonoBehaviour
{
    public BallLauncher ballLauncher;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.gameObject.SetActive(false);
        ballLauncher.ReturnBall();
    }

}