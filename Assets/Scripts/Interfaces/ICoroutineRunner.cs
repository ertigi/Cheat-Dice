using System.Collections;
using UnityEngine;

public interface ICoroutineRunner
{
    Coroutine StartCoroutine(IEnumerator routine);
    void StopCoroutine(IEnumerator routine);
    void StopCoroutine(Coroutine routine);
}