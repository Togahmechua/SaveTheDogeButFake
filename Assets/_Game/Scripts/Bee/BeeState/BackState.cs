using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackState : IState<Bee>
{
    private float retreatTime = 3f; // Thời gian lùi lại
    private float timeElapsed = 0f; // Thời gian đã trôi qua

    public void OnEnter(Bee b)
    {
        timeElapsed = 0f; // Đặt lại thời gian đã trôi qua
    }

    public void OnExecute(Bee b)
    {
        timeElapsed += Time.deltaTime;

        // Di chuyển lùi lại
        Vector2 retreatDirection = (b.transform.position - b.GetPos()).normalized; // Tính hướng lùi lại
        b.rb.velocity = retreatDirection * b.speed;

        // Kiểm tra xem đã lùi đủ khoảng cách chưa
        if (timeElapsed >= RandomTime())
        {
            b.rb.velocity = Vector2.zero; // Dừng lại
            b.TransitionToState(b.moveState); // Chuyển lại về MoveState
        }
    }

    public void OnExit(Bee b)
    {
        // Khi thoát khỏi trạng thái lùi lại
        b.rb.velocity = Vector2.zero; // Đảm bảo dừng lại
    }

    private float RandomTime()
    {
        return Random.Range(0, retreatTime);
    }
}
