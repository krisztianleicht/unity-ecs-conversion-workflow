//#define DEBUG_ENTITY
//#define DEBUG_SYSTEM_SORTING

using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.Entities;
using UnityEngine;

public static class CustomIndexConversionUtility
{
    public static void Log(Entity entity, object component, EntityManager manager,
        GameObjectConversionSystem conversionSystem)
    {
        var em = manager;
        var sb = new StringBuilder();
        sb.AppendLine("Conversion Component: " + component.GetType());
        sb.AppendLine("Conversion Parameter Entity: " + entity);
        var types = em.GetComponentTypes(entity);
        foreach (var type in types)
        {
            sb.AppendLine("Component Types: " + type);
        }

        sb.AppendLine("");

        foreach (var innerEntity in conversionSystem.GetEntities(((Component) component).gameObject))
        {
            sb.AppendLine("Behaviour Reference Entity: " + innerEntity);
            var innerTypes = em.GetComponentTypes(innerEntity);
            foreach (var type in innerTypes)
            {
                sb.AppendLine("Component Types: " + type);
            }
        }

        Debug.Log(sb.ToString());
    }

    public static void Log(object component, EntityManager manager,
        GameObjectConversionSystem conversionSystem)
    {
        var em = manager;
        var sb = new StringBuilder();
        sb.AppendLine("Conversion Component: " + component.GetType());
        var primaryEntity = conversionSystem.GetPrimaryEntity(((Component) component).gameObject);
        sb.AppendLine("Behaviour Reference Entity: " + primaryEntity);
        var primaryTypes = em.GetComponentTypes(primaryEntity);
        foreach (var type in primaryTypes)
        {
            sb.AppendLine("Component Types: " + type);
        }

        sb.AppendLine("");

        foreach (var innerEntity in conversionSystem.GetEntities(((Component) component).gameObject))
        {
            sb.AppendLine("Behaviour Reference Entity: " + innerEntity);
            var innerTypes = em.GetComponentTypes(innerEntity);
            foreach (var type in innerTypes)
            {
                sb.AppendLine("Component Types: " + type);
            }
        }

        Debug.Log(sb.ToString());
    }
}

#if DEBUG_SYSTEM_SORTING
public class CustomIndexConversionSystemByDefault : GameObjectConversionSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((CustomIndexBehaviour behaviourComponent) =>
        {
            CustomIndexConversionUtility.Log(behaviourComponent, DstEntityManager, this);
        });
   }
}
#endif

[UpdateInGroup(typeof(GameObjectAfterConversionGroup))]
public class CustomIndexConversionSystemAfterConversionGroup : GameObjectConversionSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, CustomIndexBehaviour behaviourComponent) =>
        {
            CustomIndexConversionUtility.Log(
#if DEBUG_ENTITY
                entity,
#endif
                behaviourComponent, DstEntityManager, this);
        });
    }
}