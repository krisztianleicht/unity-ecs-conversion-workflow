using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Unity.Entities;
using UnityEngine;

public class ConversionOrderPrinterSystem : GameObjectConversionSystem
{
    /// <summary>
    /// Subscene & ConvertToEntity workflow creation
    /// </summary>
    protected virtual string worldName
    {
        get { return "GameObject World"; }
    }

    protected override void OnUpdate()
    {
        var targetAssenbly = typeof(Unity.Entities.ConvertToEntity).Assembly;
        var assemblyTypes = targetAssenbly.GetTypes();
        var world = World.AllWorlds.FirstOrDefault(p => p.Name == worldName);
        const string initGroupName = "Unity.Entities.GameObjectConversionInitializationGroup";
        const string conversionGroupName = "Unity.Entities.GameObjectConversionGroup";
        const string afterConvertGroupName = "Unity.Entities.GameObjectAfterConversionGroup";

        var initSystems =
            (ComponentSystemGroup) world.GetExistingSystem(Array.Find(assemblyTypes,
                type => type.FullName == initGroupName));
        var conversionSystems =
            (ComponentSystemGroup) world.GetExistingSystem(Array.Find(assemblyTypes,
                type => type.FullName == conversionGroupName));
        var afterConvertSystems =
            (ComponentSystemGroup) world.GetExistingSystem(Array.Find(assemblyTypes,
                type => type.FullName == afterConvertGroupName));

        int index = 0;
        var sb = new StringBuilder();
        sb.AppendLine("[CONVERSION SYSTEMS ORDER]");
        sb.AppendLine();
        PrintGroup("Init systems:", initSystems, ref index, sb);
        PrintGroup("Conversion systems:", conversionSystems, ref index, sb);
        PrintGroup("After conversion systems:", afterConvertSystems, ref index, sb);
        Debug.Log(sb.ToString());
    }

    private static void PrintGroup(string groupName, ComponentSystemGroup systemGroup, ref int index, StringBuilder sb)
    {
        sb.AppendLine(groupName);
        sb.AppendLine("----------------------");
        foreach (var system in systemGroup.Systems)
        {
            sb.AppendLine("    " + index + ".: " + system.GetType().FullName);
            index++;
        }

        sb.AppendLine();
    }
}