using System;
using Unity.Entities;

[Serializable]
public struct CustomIndexComponent : IComponentData
{
    public int Value;
}
