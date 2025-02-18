using System.Collections;
using UnityEngine;

namespace Assets.Sources.Initialization.Contracts
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}
