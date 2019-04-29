using System;
using Unity.Entities;

[Serializable]
public struct TransformedIndexComponent : IComponentData
{
    public int Value;
}
