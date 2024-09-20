using System.Collections;
using UnityEngine;

public class GravityLookAt : MonoBehaviour
{
    Transform toLookAt;
    public Vector3 lookAtOffset;
    public Vector3 correctRotation;

    void Start()
    {
        StartCoroutine(GetPlayer());
    }
    void Update()
    {
        if (toLookAt != null)
        {
            this.transform.LookAt(toLookAt.position + lookAtOffset);
            this.transform.Rotate(correctRotation);
        }
    }

    IEnumerator GetPlayer()
    {
        yield return new WaitUntil(() => PersistentManager.Instance != null);
        toLookAt = PersistentManager.Instance.GetPlayer().transform;
    }
}
