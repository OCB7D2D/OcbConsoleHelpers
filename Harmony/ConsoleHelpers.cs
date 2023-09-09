using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

public class ConsoleHelpers : IModApi
{

    static public string path { get; protected set; }

    public void InitMod(Mod mod)
    {
        path = mod.Path; // make statically available
        Log.Out(" Loading Patch: " + GetType().ToString());
        Harmony harmony = new Harmony(GetType().ToString());
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }

    [HarmonyPatch(typeof(Vehicle))]
    [HarmonyPatch("MakeItemValue")]
    public class Vehicle_MakeItemValue
    {
        public static void Prefix(
            Vehicle __instance)
        {
            string name = __instance.GetName();
            if (ItemClass.GetItemClass(name + "Placeable", true) == null)
                Log.Error("Missing item named {0}Placeable", name);
        }
    }

	[HarmonyPatch(typeof(EntityVehicle))]
	[HarmonyPatch("Init")]
	public static class EntityVehicle_Init
	{

		static void CheckVehicle(EntityVehicle entity)
        {
            if (string.IsNullOrEmpty(entity.GetLootList()))
                Log.Error("LootList for vehicle not defined?");
            if (LootContainer.GetLootContainer(entity.GetLootList()) == null)
                Log.Error("Invalid Loot Container {0}?", entity.GetLootList());
            if (entity.PhysicsTransform == null)
                Log.Error("Vehicle has no Physics Transform?");
            if (entity.PhysicsTransform.GetComponent<Rigidbody>() == null)
                Log.Warning("Vehicle has no Physics Rigidbody?");
        }

        public static void Prefix(
            EntityVehicle __instance,
            int _entityClass)
        {
        }
        
        static IEnumerable<CodeInstruction> Transpiler
			(IEnumerable<CodeInstruction> instructions)
		{
			var codes = new List<CodeInstruction>(instructions);
			for (var i = 0; i < codes.Count; i++)
			{
				if (codes[i].opcode == OpCodes.Call)
                {
					if (codes[i].operand is MethodInfo info)
                    {
						if (info.Name == "Init")
                        {
							// Make sure check is called after base init is done
							codes.Insert(i + 1, new CodeInstruction(OpCodes.Ldarg_0, null)); // this
							codes.Insert(i + 2, CodeInstruction.Call(typeof(EntityVehicle_Init), "CheckVehicle"));
							break;
						}
					}
				}
			}
			return codes;
		}
	}

    [HarmonyPatch(typeof(EntityVehicle))]
    [HarmonyPatch("HandleNavObject")]
    public static class EntityVehicle_HandleNavObject
    {

        public static void Prefix(
            EntityVehicle __instance)
        {
            if (__instance.GetVehicle().GetMeshTransform() == null)
                Log.Error("Vehicle has no mesh transform?");
            var nav = EntityClass.list[__instance.entityClass].NavObject;
            if (string.IsNullOrEmpty(nav)) Log.Error("No NavObject defined?");
        }

    }

    [HarmonyPatch(typeof(NavObjectClass))]
    [HarmonyPatch("GetNavObjectClass")]
    public static class NavObjectClass_GetNavObjectClass
    {
        public static void Postfix(
            NavObjectClass __result,
            string className)
        {
            if (__result != null) return;
            Log.Error("NavObjectClass not found {0}", className);
        }
    }

    /*
        string name = "#@modfolder(OcbMod):Resources/Bundle.unity3d?" + _params[0];
        var original = DataLoader.LoadAsset<GameObject>(name);
        Log.Warning("Loading {0} => {1}", name, original);
        if (original == null) return;
        GameObject go = UnityEngine.Object.Instantiate<GameObject>(original);
        Log.Warning("Instancing {0} => {1}", name, go);
        if (go == null) return;
        foreach (var comp in go.GetComponents<UnityEngine.Component>())
            Log.Warning(" ===> {0}", comp);
    */

}
