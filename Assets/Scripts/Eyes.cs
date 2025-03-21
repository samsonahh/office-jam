using System.Collections;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    Vector3 startingPosition;

    Vector2 zRotationRange = new Vector2(-30f, 30f);
        
    private void Awake()
    {
        startingPosition = transform.position;
    }

    IEnumerator CheckCoroutine()
    {
        yield return null;
    }
}
