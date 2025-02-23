using System;
using UnityEngine;

namespace Source.Data.Surrogates
{
    [Serializable]
    public struct Vector3Surrogate
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3Surrogate(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;

            IsValid = true;
        }

        public Vector3Surrogate(Vector3 vector) : this(vector.x, vector.y, vector.z) { }

        public bool IsValid { get; }

        public readonly Vector3 ConvertToVector3() =>
            new(X, Y, Z);
    }
}
