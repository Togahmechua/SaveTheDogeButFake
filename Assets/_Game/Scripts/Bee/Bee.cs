using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bee : GameUnit
{
    [Header("-------------------Other-------------------")]
    [SerializeField] private PlayerCtrl player;
    public float speed;
    public Rigidbody2D rb;

    [SerializeField] private GameObject model;
    [SerializeField] private float radius;

    private bool isWithinRadius = false;
    private float distanceToPlayer;

    [Header("-------------------Bee AI-------------------")]
    public IState<Bee> currentState;
    public MoveState moveState;
    public BackState backState;
    public StopState stopState;

    private void Start()
    {
        player = LevelManager.Ins.level.playerCtrl;
        moveState = new MoveState();
        backState = new BackState();
        stopState = new StopState();
        TransitionToState(moveState);
    }

    private void Update()
    {

        if (player != LevelManager.Ins.level.playerCtrl)
        {
            player = LevelManager.Ins.level.playerCtrl;
        }
        CheckPlayerDistance();
        currentState?.OnExecute(this);
    }

    public void TransitionToState(IState<Bee> newState)
    {
        currentState?.OnExit(this);
        currentState = newState;
        currentState?.OnEnter(this);
    }

    public Vector3 GetPos()
    {
        return player.RandomPlayerPos();
    }

    private void CheckPlayerDistance()
    {
        distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceToPlayer < radius)
        {
            FaceToDog();
            if (!isWithinRadius)
            {
                isWithinRadius = true;
                if (LevelManager.Ins.timesUp)
                    return;
                player.beeRangeDetector.SetIsNear(true);
            }
        }
        else if (distanceToPlayer >= radius && isWithinRadius)
        {
            isWithinRadius = false;
            player.beeRangeDetector.SetIsNear(false);
        }
    }

    private void FaceToDog()
    {
        Vector3 playerPos = player.transform.position;
        Vector2 distance = playerPos - transform.position;
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        model.transform.rotation = Quaternion.Euler(new Vector3(0f, angle >= -90f && angle < 90f ? 0f : 180f, 90f));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}