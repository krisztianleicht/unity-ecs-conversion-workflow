using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Entities;
using UnityEngine;

public class CustomIndexBehaviour : MonoBehaviour
, IConvertGameObjectToEntity
{
    
    public int Index;
    
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new CustomIndexComponent { Value = Index });

        var sb = new StringBuilder();
        sb.AppendLine("Adding CustomIndexComponent to entity: " + entity);
        sb.AppendLine("PrimaryEntity: " + conversionSystem.GetPrimaryEntity(this));
        sb.AppendLine("GetEntities.First: " + conversionSystem.GetEntities(this).First());
        Debug.Log(sb.ToString());
    }
    
}
