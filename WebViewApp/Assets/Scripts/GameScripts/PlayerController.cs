using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [Header("Link to BallMoveScript")]
    [SerializeField] private BallMove ball;

    [Header("Aim settings")]
    [SerializeField] private RectTransform aim;
    [SerializeField] private Canvas canvas;
    [Space]
    [SerializeField] private float maxMoveX;
    [SerializeField] private float minMoveX;
    [SerializeField] private float maxMoveY;
    [SerializeField] private float minMoveY;
  
    public void OnDrag(PointerEventData eventData)
    {
        aim.anchoredPosition = new Vector2(Mathf.Clamp(aim.anchoredPosition.x + eventData.delta.x / canvas.scaleFactor, minMoveX, maxMoveX),
            Mathf.Clamp(aim.anchoredPosition.y + eventData.delta.y / canvas.scaleFactor, minMoveY, maxMoveY));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        AudioManager.Instance?.PlaySound(AudioManager.Sounds.Push);

        aim.gameObject.SetActive(false);
        ball.Move(aim.anchoredPosition);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        aim.gameObject.SetActive(true);
    }
}
