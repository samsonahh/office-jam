using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    Vector3 startingPosition;

    public GameObject angryEyes;

    public float stareYPosition = 3.5f;
    public Vector2 zRotationRange = new Vector2(-30f, 30f);
    public Vector2 xPositionRange = new Vector2(-6.5f, 6.5f);
    public Vector2 riseDurationRange = new Vector2(1f, 2.5f);
    public Vector2 checkIntervalRange = new Vector2(3f, 6f);
    public Vector2 stareDurationRange = new Vector2(1f, 2f);

    public bool isStaring;
        
    private void Awake()
    {
        startingPosition = transform.position;

        StartCoroutine(CheckCoroutine());
    }

    private void Update()
    {
        angryEyes.SetActive(isStaring);
    }

    IEnumerator CheckCoroutine()
    {
        transform.position = startingPosition;
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(checkIntervalRange.x, checkIntervalRange.y));

            transform.position = new Vector3(Random.Range(xPositionRange.x, xPositionRange.y), transform.position.y, transform.position.z);
            transform.localRotation = Quaternion.Euler(0, 0, Random.Range(zRotationRange.x, zRotationRange.y));

            float randomRiseDuration = Random.Range(riseDurationRange.x, riseDurationRange.y);
            for (float t = 0; t < randomRiseDuration; t += Time.deltaTime)
            {
                float parameter = DOVirtual.EasedValue(0, 1, t / randomRiseDuration, Ease.InSine);
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, startingPosition.y, transform.position.z), new Vector3(transform.position.x, stareYPosition, transform.position.z), parameter);
                yield return null;
            }
            transform.position = new Vector3(transform.position.x, stareYPosition, transform.position.z);

            isStaring = true;
            yield return new WaitForSeconds(Random.Range(stareDurationRange.x, stareDurationRange.y));
            isStaring = false;

            for (float t = 0; t < randomRiseDuration; t += Time.deltaTime)
            {
                float parameter = DOVirtual.EasedValue(0, 1, t / randomRiseDuration, Ease.OutSine);
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, stareYPosition, transform.position.z), new Vector3(transform.position.x, startingPosition.y, transform.position.z), parameter);
                yield return null;
            }
            transform.position = new Vector3(transform.position.x, startingPosition.y, transform.position.z);
        }
    }

    public void Shake()
    {
        transform.DOShakePosition(10f, 4f).SetUpdate(true);
    }
}
