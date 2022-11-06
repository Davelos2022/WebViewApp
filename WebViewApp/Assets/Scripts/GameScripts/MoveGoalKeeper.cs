using UnityEngine;

public class MoveGoalKeeper : MonoBehaviour
{
    [SerializeField] private float maxMoveX;
    [SerializeField] private float minMoveX;


    private float speed;
    private float movetarget;
    private RectTransform thisObj;

    private void OnEnable()
    {
        speed = 25;

        movetarget = maxMoveX;
        thisObj = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        if (GameManager.Instance.IsGame)
            Move();
    }

    private void Move()
    {
        if (thisObj.anchoredPosition.x >= maxMoveX)
            movetarget = minMoveX;
        else if (thisObj.anchoredPosition.x <= minMoveX)
            movetarget = maxMoveX;

        thisObj.anchoredPosition = Vector2.MoveTowards(thisObj.anchoredPosition, new Vector2(movetarget, thisObj.anchoredPosition.y), speed);
    }

    public void NextLevel()
    {
        if (speed >= 70)
            speed = 70;
        else
            speed += 5;
    }
}
