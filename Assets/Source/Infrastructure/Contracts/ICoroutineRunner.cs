using System.Collections;
using UnityEngine;

namespace Source.Infrastructure.Contracts
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}
