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
}
