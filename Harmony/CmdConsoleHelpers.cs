using System.Collections.Generic;
using UnityEngine;

public class ConsoleHelpersCmd : ConsoleCmdAbstract
{

    private static string info = "OcbConsoleHelpers";

    public override string[] GetCommands()
    {
        return new string[2] { info, "ocb" };
    }

    public override string GetDescription() => "OCB Console Helpers";

    public override string GetHelp() => @"OCB Console Helpers
Player/vehicle IK position
    
  `ocb ikts`
  `ocb ikp LeftHand 5,8,-7`
  `ocb ikp RightFoot -4,3,2`

Other utility functions

  `ocb cvars` to list all player.Buffs.CVars
  `ocb progression` to list ProgressionValues 
  `ocb qfp` to list all QuestFactionPoints

 You can also use this mod to modify certain values:

  `ocb cvar <name> [float]`
  `ocb progression <name> [level]`
  `ocb qfp <name> <value>`
";

    public bool UpdateVehiclePartIKR(EntityPlayer player, string name, Vector3 vec)
    {
        // Creates one once and then re-returns it
        var ikct = player.emodel.AddIKController();
        if (ikct == null) Log.Error("No IK Animator found on model?");
        for (int i = 0; i < ikct.targets.Count; i++)
        {
            IKController.Target ikt = ikct.targets[i];
            if (name != ikt.avatarGoal.ToString()) continue;
            Transform tr = ikt.transform; // All have one?
            if (tr != null) tr.localEulerAngles = vec;
            else ikt.rotation.Set(vec.x, vec.y, vec.z);
            // ikct.OnAnimatorIK(); // Update stuff
            return true;
        }
        return false;
    }

    public bool UpdateVehiclePartIKP(EntityPlayer player, string name, Vector3 vec)
    {
        // Creates one once and then re-returns it
        var ikct = player.emodel.AddIKController();
        if (ikct == null) Log.Error("No IK Animator found on model?");
        for (int i = 0; i < ikct.targets.Count; i++)
        {
            IKController.Target ikt = ikct.targets[i];
            if (name != ikt.avatarGoal.ToString()) continue;
            Transform tr = ikt.transform; // All have one?
            if (tr != null) tr.localPosition = vec;
            else ikt.position.Set(vec.x, vec.y, vec.z);
            // ikct.OnAnimatorIK(); // Update stuff
            return true;
        }
        return false;
    }

    public IKController.Target? GetPlayerIK(EntityPlayer player, string name)
    {
        // Creates one once and then re-returns it
        var ikct = player.emodel.AddIKController();
        if (ikct == null) Log.Error("No IK Animator found on model?");
        for (int i = 0; i < ikct.targets.Count; i++)
        {
            IKController.Target ikt = ikct.targets[i];
            if (name != ikt.avatarGoal.ToString()) continue;
            return ikt;
        }
        return null;
    }

    private string ItemOrBlockToString(ItemValue item)
    {
        if (item.type < Block.ItemsStartHere)
        {
            return item.ToBlockValue().Block.GetBlockName();
        }
        else
        {
            return item.ItemClass.GetItemName();
        }
    }

    private void ExecuteLootGroupTest(string name, float stage = 0f)
    {
        var manager = GameManager.Instance.lootManager;
        var container = LootContainer.GetLootContainer(name);
        if (container == null) throw new System.Exception(
            string.Format("Loot container is not known {0}", name));
        EntityPlayer player = GameManager.Instance.World.GetPrimaryPlayer();
        FastTags tags = new FastTags();
        Log.Out("Reporting loot from {0} at tier {1}", name, 0);
        IList<ItemStack> itemStackList = container.Spawn(manager.Random,
            3, stage, 0.0f, player, tags);
        foreach (var item in itemStackList)
        {
            player.bag.AddItem(item);
            Log.Out("  {0} x {1}", item.count, ItemOrBlockToString(item.itemValue));
        }
    }

    public override void Execute(List<string> _params, CommandSenderInfo _senderInfo)
    {

        EntityPlayer player = GameManager.Instance.World.GetPrimaryPlayer();

        if (_params.Count == 1)
        {
            switch (_params[0])
            {
                case "cvars":
                    Log.Out("List of player.Buffs.CVars");
                    foreach (var kv in player.Buffs.CVars)
                        Log.Out("{0} => {1}", kv.Key, kv.Value);
                    break;
                case "qfp":
                    Log.Out("List of QuestJournal.QuestFactionPoints");
                    foreach (var kv in XUiM_Player.GetPlayer().QuestJournal.QuestFactionPoints)
                        Log.Out("{0} => {1}", kv.Key, kv.Value);
                    break;
                case "progression":
                    Log.Out("List of player.ProgressionValues");
                    foreach (var kv in player.Progression.ProgressionValues.Dict)
                        Log.Out("{0} => {1} (next {2}%, cost: {3})",
                            kv.Value.Name, kv.Value.Level,
                            kv.Value.PercToNextLevel,
                            kv.Value.CostForNextLevel);
                    break;
                case "ikts":
                    ReportIKT(player, "LeftHand");
                    ReportIKT(player, "RightHand");
                    ReportIKT(player, "LeftFoot");
                    ReportIKT(player, "RightFoot");
                    break;
            }
        }

        else if (_params.Count == 2)
        {
            switch (_params[0])
            {
                case "cvar":
                    var cvar = player.Buffs.CVars[_params[1]];
                    Log.Out("{0} => {1}", _params[1], cvar);
                    break;
                case "progression":
                    var prog = player.Progression.GetProgressionValue(_params[1]);
                    Log.Out("{0} => {1} (next {2}%, cost: {3})",
                        prog.Name, prog.Level,
                        prog.PercToNextLevel, prog.CostForNextLevel);
                    break;
                case "lootgroup":
                    ExecuteLootGroupTest(_params[1]);
                    break;
                default:
                    Log.Warning("Unknown command " + _params[0]);
                    break;
            }
        }

        else if (_params.Count == 3)
        {
            switch (_params[0])
            {
                case "cvar":
                    var cvar = player.Buffs.CVars[_params[1]];
                    Log.Out("Got {0} => {1}", _params[1], cvar);
                    player.Buffs.CVars[_params[1]] = float.Parse(_params[2]);
                    player.Buffs.SetCustomVar(_params[1], cvar);
                    cvar = player.Buffs.CVars[_params[1]];
                    Log.Out("Set {0} => {1}", _params[1], cvar);
                    break;

                case "ikp":
                    SetPlayerIKP(player, _params[1], StringParsers.ParseVector3(_params[2]));
                    break;
                case "ikr":
                    SetPlayerIKR(player, _params[1], StringParsers.ParseVector3(_params[2]));
                    break;

                case "qfp":
                    Log.Out("List of all quest faction points");
                    byte key = byte.Parse(_params[1]);
                    int value = int.Parse(_params[2]);
                    if (XUiM_Player.GetPlayer().QuestJournal.QuestFactionPoints.ContainsKey(key))
                        XUiM_Player.GetPlayer().QuestJournal.QuestFactionPoints[key] = value;
                    else XUiM_Player.GetPlayer().QuestJournal.QuestFactionPoints.Add(key, value);
                    break;
                case "progression":
                    var prog = player.Progression.GetProgressionValue(_params[1]);
                    Log.Out("Got {0} => {1}", _params[1], prog.Level);
                    prog.Level = int.Parse(_params[2]);
                    prog = player.Progression.GetProgressionValue(_params[1]);
                    Log.Out("Set {0} => {1}", _params[1], prog.Level);
                    break;
                case "lootgroup":
                    ExecuteLootGroupTest(_params[1],
                        float.Parse(_params[2]));
                    break;
                default:
                    Log.Warning("Unknown command " + _params[0]);
                    break;
            }
        }

    }

    private void ReportIKT(EntityPlayer player, string name)
    {
        IKController.Target? ikt = GetPlayerIK(player, name);
        if (ikt == null)
        {
            Log.Out("{0} Position: {1}", name, "not available");
            Log.Out("{0} Rotation: {1}", name, "not available");
        }
        else if (ikt.Value.transform == null)
        {
            Log.Out("{0} Position: {1}", name, ReportVector3(ikt.Value.position));
            Log.Out("{0} Rotation: {1}", name, ReportVector3(ikt.Value.rotation));
        }
        else
        {
            Log.Out("{0} Position: {1}", name, ReportVector3(ikt.Value.transform.localPosition));
            Log.Out("{0} Rotation: {1}", name, ReportVector3(ikt.Value.transform.localEulerAngles));
        }
    }

    private void SetPlayerIKP(EntityPlayer player, string name, Vector3 vec)
    {
        if (UpdateVehiclePartIKP(player, name, vec))
            Log.Out("Set {0} Position to {1}", name, ReportVector3(vec));
        else Log.Error("IK Target {0} not available?", name);
    }

    private void SetPlayerIKR(EntityPlayer player, string name, Vector3 vec)
    {
        if (UpdateVehiclePartIKR(player, name, vec))
            Log.Out("Set {0} Rotation to {1}", name, ReportVector3(vec));
        else Log.Error("IK Target {0} not available?", name);
    }

    private string ReportVector3(Vector3 vector)
    {
        return string.Format("{0:0.####}, {1:0.####} {2:0.####}",
            vector.x, vector.y, vector.z);
    }
}
