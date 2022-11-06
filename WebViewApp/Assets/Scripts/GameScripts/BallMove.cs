using System.Collections;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    [SerializeField] private float speedBall;

    private RectTransform ball;
    private Vector3 saveScale;
    private Vector2 savePositon;
    private Vector3 moveScale = new Vector3(0.5f, 0.5f);

    private void OnEnable()
    {
        ball = GetComponent<RectTransform>();

        saveScale = ball.localScale;
        savePositon = ball.anchoredPosition;
    }
    public void Move(Vector2 move)
    {
        StartCoroutine(moveBall(move));
    }

    private IEnumerator moveBall(Vector2 move)
    {
        while (ball.localScale != moveScale)
        {
            ball.anchoredPosition = Vector2.MoveTowards(ball.anchoredPosition, move, speedBall);
            ball.localScale = Vector2.MoveTowards(ball.localScale, moveScale, 1.08f * Time.deltaTime);
            yield return null;
        }

        yield break;
    }

    public void ResetGame()
    {
        ball.anchoredPosition = savePositon;
        ball.localScale = saveScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.IsGame)
        {
            if (collision.transform.tag == "Goalkeeper")
            {
                GameManager.Instance.LoseGame();
            }
            else if (collision.transform.tag == "Goal")
                GameManager.Instance.WinGame();
        }
    }
}
