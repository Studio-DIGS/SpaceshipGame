using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float length, float strength)
    {
        Vector3 orgPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < length)
        {
            float x = Random.Range(-1f, 1f) * strength;
            float y = Random.Range(-1f, 1f) * strength;

            transform.localPosition = new Vector3(x, y, orgPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = orgPos;
    }
}
