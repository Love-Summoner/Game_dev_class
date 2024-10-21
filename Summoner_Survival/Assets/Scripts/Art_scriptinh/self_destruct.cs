using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class self_destruct : MonoBehaviour
{
    public float lifetime;
    void Start()
    {
        StartCoroutine(delayed_destruct());
    }

    IEnumerator delayed_destruct()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
