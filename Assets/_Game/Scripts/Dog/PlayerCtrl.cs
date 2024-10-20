using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerCtrl : MonoBehaviour
{
    public BeeRangeDetector beeRangeDetector;
    public Rigidbody2D rb;

    [SerializeField] private float approachRadius;
    [Header("----------------SpriteLibrary----------------")]
    [SerializeField] private SpriteLibraryAsset[] sprLibraryAsset;
    [SerializeField] private SpriteLibrary sprLibrary;

    public Vector3 RandomPlayerPos()
    {
        Vector2 randomOffset = Random.insideUnitCircle * approachRadius;
        Vector3 randomPos = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);

        return randomPos;
    }

    public void SetSprAsset(int i)
    {
        sprLibrary.spriteLibraryAsset = sprLibraryAsset[i];
    }

/*    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, approachRadius);
    }*/
}
