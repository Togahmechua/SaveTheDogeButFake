using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache
{
    private static Dictionary<Collider2D, PlayerCtrl> players = new Dictionary<Collider2D, PlayerCtrl>();

    public static PlayerCtrl GetCharacter(Collider2D collider)
    {
        if (!players.ContainsKey(collider))
        {
            players.Add(collider, collider.GetComponent<PlayerCtrl>());
        }

        return players[collider];
    }

    private static Dictionary<Collider2D, Bee> bees = new Dictionary<Collider2D, Bee>();

    public static Bee GetBee(Collider2D collider)
    {
        if (!bees.ContainsKey(collider))
        {
            bees.Add(collider, collider.GetComponent<Bee>());
        }

        return bees[collider];
    }

    private static Dictionary<Collider2D, Rock> rocks = new Dictionary<Collider2D, Rock>();

    public static Rock GetRock(Collider2D collider)
    {
        if (!rocks.ContainsKey(collider))
        {
            rocks.Add(collider, collider.GetComponent<Rock>());
        }

        return rocks[collider];
    }

    private static Dictionary<Collider2D, Axit> axits = new Dictionary<Collider2D, Axit>();

    public static Axit GetAxit(Collider2D collider)
    {
        if (!axits.ContainsKey(collider))
        {
            axits.Add(collider, collider.GetComponent<Axit>());
        }

        return axits[collider];
    }
}
