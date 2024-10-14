using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveState : IState<Bee>
{
    private Vector3 targetPosition;
    private float moveDuration = 2f;
    private float time;
    private float maxWaitTime = 1f;
    private bool isTouch;

    public void OnEnter(Bee b)
    {
        // Giai đoạn bay ngẫu nhiên
        targetPosition = b.GetPos();
        time = 0;
        b.StartCoroutine(WaitBeforeAttack(b, moveDuration));
    }

    public void OnExecute(Bee b)
    {
        if (LevelManager.Ins.timesUp)
        {
            b.TransitionToState(b.stopState);
        }

        time += Time.deltaTime;
        Vector2 direction = (targetPosition - b.transform.position).normalized;

        // Kiểm tra xem có nên giảm tốc độ không
        if (Vector2.Distance(b.transform.position, targetPosition) < 0.5f)
        {
            b.rb.velocity = direction * (b.speed * 0.5f); // Giảm tốc độ khi gần mục tiêu
        }
        else
        {
            b.rb.velocity = direction * b.speed; // Giữ tốc độ bình thường
        }

        // Kiểm tra nếu đã đến điểm ngẫu nhiên
        if (time > maxWaitTime)
        {
            b.TransitionToState(b.backState);
            time = 0;
        }
    }

    public void OnExit(Bee b)
    {
        // Khi thoát khỏi trạng thái ngẫu nhiên
        b.rb.velocity = Vector2.zero;
    }

    private IEnumerator WaitBeforeAttack(Bee b, float waitTime)
    {
        // Đợi một khoảng thời gian trước khi chuyển sang giai đoạn tấn công
        yield return new WaitForSeconds(waitTime);
        targetPosition = b.GetPos(); // Đặt mục tiêu là người chơi
    }
}
