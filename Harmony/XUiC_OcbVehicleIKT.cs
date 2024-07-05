using System;
using UnityEngine;
using static LightingAround;

public class XUiC_OcbVehicleIKT : XUiController
{

    // ####################################################################
    // ####################################################################

    private Vector3 GetIktPos(IKController.Target ikt)
    {
        if (ikt.transform == null) return ikt.position;
        return ikt.transform.localPosition;
    }

    private Vector3 GetIktRot(IKController.Target ikt)
    {
        if (ikt.transform == null) return ikt.rotation;
        return ikt.transform.localEulerAngles;
    }

    private void SetIktPos(ref IKController.Target ikt, Vector3 position)
    {
        ikt.position = position;
        if (ikt.transform == null) return;
        else ikt.transform.localPosition = position;
    }

    private void SetIktRot(ref IKController.Target ikt, Vector3 rotation)
    {
        ikt.rotation = rotation;
        if (ikt.transform == null) return;
        ikt.transform.localEulerAngles = rotation;
    }

    // ####################################################################
    // ####################################################################

    private XUiC_SimpleButton btnLoad;
    private XUiC_SimpleButton btnClose;

    private XUiC_ComboBoxList<int> uiPose;

    private XUiC_ComboBoxFloat uiSeatPosX;
    private XUiC_ComboBoxFloat uiSeatPosY;
    private XUiC_ComboBoxFloat uiSeatPosZ;
    private XUiC_ComboBoxFloat uiSeatRotX;
    private XUiC_ComboBoxFloat uiSeatRotY;
    private XUiC_ComboBoxFloat uiSeatRotZ;

    private XUiC_ComboBoxFloat uiLeftHandPosX;
    private XUiC_ComboBoxFloat uiLeftHandPosY;
    private XUiC_ComboBoxFloat uiLeftHandPosZ;
    private XUiC_ComboBoxFloat uiRightHandPosX;
    private XUiC_ComboBoxFloat uiRightHandPosY;
    private XUiC_ComboBoxFloat uiRightHandPosZ;
    private XUiC_ComboBoxFloat uiLeftHandRotX;
    private XUiC_ComboBoxFloat uiLeftHandRotY;
    private XUiC_ComboBoxFloat uiLeftHandRotZ;
    private XUiC_ComboBoxFloat uiRightHandRotX;
    private XUiC_ComboBoxFloat uiRightHandRotY;
    private XUiC_ComboBoxFloat uiRightHandRotZ;

    private XUiC_ComboBoxFloat uiLeftFootPosX;
    private XUiC_ComboBoxFloat uiLeftFootPosY;
    private XUiC_ComboBoxFloat uiLeftFootPosZ;
    private XUiC_ComboBoxFloat uiRightFootPosX;
    private XUiC_ComboBoxFloat uiRightFootPosY;
    private XUiC_ComboBoxFloat uiRightFootPosZ;
    private XUiC_ComboBoxFloat uiLeftFootRotX;
    private XUiC_ComboBoxFloat uiLeftFootRotY;
    private XUiC_ComboBoxFloat uiLeftFootRotZ;
    private XUiC_ComboBoxFloat uiRightFootRotX;
    private XUiC_ComboBoxFloat uiRightFootRotY;
    private XUiC_ComboBoxFloat uiRightFootRotZ;

    // ####################################################################
    // ####################################################################

    protected void GetComboBoxInt(ref XUiC_ComboBoxInt obj, string name)
    {
        obj = GetChildById(name) as XUiC_ComboBoxInt;
        if (obj == null) Log.Error("Could not find `{0}` in UI", name);
        else obj.OnValueChanged += (sender, prev, now) => OnCfgUpdate(name);
    }

    protected void GetComboBoxFloat(ref XUiC_ComboBoxFloat obj, string name)
    {
        obj = GetChildById(name) as XUiC_ComboBoxFloat;
        if (obj == null) Log.Error("Could not find `{0}` in UI", name);
        else obj.OnValueChanged += (sender, prev, now) => OnCfgUpdate(name);
    }

    protected void GetComboBoxBool(ref XUiC_ComboBoxBool obj, string name)
    {
        obj = GetChildById(name) as XUiC_ComboBoxBool;
        if (obj == null) Log.Error("Could not find `{0}` in UI", name);
        else obj.OnValueChanged += (sender, prev, now) => OnCfgUpdate(name);
    }

    protected void GetComboBoxList<T>(ref XUiC_ComboBoxList<T> obj, string name) where T : struct, IConvertible
    {
        obj = GetChildById(name) as XUiC_ComboBoxList<T>;
        if (obj == null) Log.Error("Could not find `{0}` in UI", name);
        else obj.OnValueChanged += (sender, prev, now) => OnCfgUpdate(name);
    }

    protected void GetComboBoxEnum<T>(ref XUiC_ComboBoxEnum<T> obj, string name) where T : struct, IConvertible
    {
        obj = GetChildById(name) as XUiC_ComboBoxEnum<T>;
        if (obj == null) Log.Error("Could not find `{0}` in UI", name);
        else obj.OnValueChanged += (sender, prev, now) => OnCfgUpdate(name);
    }

    // ####################################################################
    // ####################################################################

    public override void Init()
    {
        base.Init();
        btnLoad = GetChildById("btnLoad") as XUiC_SimpleButton;
        btnLoad.OnPressed += OnLoadPressed;
        btnClose = GetChildById("btnClose") as XUiC_SimpleButton;
        btnClose.OnPressed += OnClosePressed;
        GetComboBoxList<int>(ref uiPose, "uiPose");
        GetComboBoxFloat(ref uiSeatPosX, "uiSeatPosX");
        GetComboBoxFloat(ref uiSeatPosY, "uiSeatPosY");
        GetComboBoxFloat(ref uiSeatPosZ, "uiSeatPosZ");
        GetComboBoxFloat(ref uiSeatRotX, "uiSeatRotX");
        GetComboBoxFloat(ref uiSeatRotY, "uiSeatRotY");
        GetComboBoxFloat(ref uiSeatRotZ, "uiSeatRotZ");
        GetComboBoxFloat(ref uiLeftHandPosX, "uiLeftHandPosX");
        GetComboBoxFloat(ref uiLeftHandPosY, "uiLeftHandPosY");
        GetComboBoxFloat(ref uiLeftHandPosZ, "uiLeftHandPosZ");
        GetComboBoxFloat(ref uiRightHandPosX, "uiRightHandPosX");
        GetComboBoxFloat(ref uiRightHandPosY, "uiRightHandPosY");
        GetComboBoxFloat(ref uiRightHandPosZ, "uiRightHandPosZ");
        GetComboBoxFloat(ref uiLeftHandRotX, "uiLeftHandRotX");
        GetComboBoxFloat(ref uiLeftHandRotY, "uiLeftHandRotY");
        GetComboBoxFloat(ref uiLeftHandRotZ, "uiLeftHandRotZ");
        GetComboBoxFloat(ref uiRightHandRotX, "uiRightHandRotX");
        GetComboBoxFloat(ref uiRightHandRotY, "uiRightHandRotY");
        GetComboBoxFloat(ref uiRightHandRotZ, "uiRightHandRotZ");
        GetComboBoxFloat(ref uiLeftFootPosX, "uiLeftFootPosX");
        GetComboBoxFloat(ref uiLeftFootPosY, "uiLeftFootPosY");
        GetComboBoxFloat(ref uiLeftFootPosZ, "uiLeftFootPosZ");
        GetComboBoxFloat(ref uiRightFootPosX, "uiRightFootPosX");
        GetComboBoxFloat(ref uiRightFootPosY, "uiRightFootPosY");
        GetComboBoxFloat(ref uiRightFootPosZ, "uiRightFootPosZ");
        GetComboBoxFloat(ref uiLeftFootRotX, "uiLeftFootRotX");
        GetComboBoxFloat(ref uiLeftFootRotY, "uiLeftFootRotY");
        GetComboBoxFloat(ref uiLeftFootRotZ, "uiLeftFootRotZ");
        GetComboBoxFloat(ref uiRightFootRotX, "uiRightFootRotX");
        GetComboBoxFloat(ref uiRightFootRotY, "uiRightFootRotY");
        GetComboBoxFloat(ref uiRightFootRotZ, "uiRightFootRotZ");
    }

    private void OnClosePressed(XUiController _sender, int _mouseButton)
    {
        var wm = xui.playerUI.windowManager;
        wm.Close("ocbVehicleIKT");
    }

    private void OnLoadPressed(XUiController _sender, int _mouseButton)
    {
        World world = GameManager.Instance.World;
        EntityPlayerLocal player = world.GetPrimaryPlayer();
        // Ensure we have a player model
        if (player.emodel == null)
        {
            Log.Error("Must be in a vehicle");
            return;
        }
        // This is really just a getter (adds once)
        IKController ikct = player.emodel.AddIKController();
        if (ikct == null) Log.Error("No IK Animator found on model?");
        // Get the pose option for vehicle
        uiPose.Value = player.vehiclePoseMode;
        // Get position and rotation for seat position
        Vector3 pos = player.ModelTransform.localPosition;
        Vector3 rot = player.ModelTransform.localEulerAngles;
        uiSeatPosX.Value = pos.x;
        uiSeatPosY.Value = pos.y;
        uiSeatPosZ.Value = pos.z;
        uiSeatRotX.Value = rot.x;
        uiSeatRotY.Value = rot.y;
        uiSeatRotZ.Value = rot.z;
        // Get position and rotation for all ik targets
        for (int i = 0; i < ikct?.targets?.Count; i++)
        {
            pos = GetIktPos(ikct.targets[i]);
            rot = GetIktRot(ikct.targets[i]);
            switch (ikct.targets[i].avatarGoal)
            {
                case UnityEngine.AvatarIKGoal.LeftHand:
                    uiLeftHandPosX.Value = pos.x;
                    uiLeftHandPosY.Value = pos.y;
                    uiLeftHandPosZ.Value = pos.z;
                    uiLeftHandRotX.Value = rot.x;
                    uiLeftHandRotY.Value = rot.y;
                    uiLeftHandRotZ.Value = rot.z;
                    break;
                case UnityEngine.AvatarIKGoal.RightHand:
                    uiRightHandPosX.Value = pos.x;
                    uiRightHandPosY.Value = pos.y;
                    uiRightHandPosZ.Value = pos.z;
                    uiRightHandRotX.Value = rot.x;
                    uiRightHandRotY.Value = rot.y;
                    uiRightHandRotZ.Value = rot.z;
                    break;
                case UnityEngine.AvatarIKGoal.LeftFoot:
                    uiLeftFootPosX.Value = pos.x;
                    uiLeftFootPosY.Value = pos.y;
                    uiLeftFootPosZ.Value = pos.z;
                    uiLeftFootRotX.Value = rot.x;
                    uiLeftFootRotY.Value = rot.y;
                    uiLeftFootRotZ.Value = rot.z;
                    break;
                case UnityEngine.AvatarIKGoal.RightFoot:
                    uiRightFootPosX.Value = pos.x;
                    uiRightFootPosY.Value = pos.y;
                    uiRightFootPosZ.Value = pos.z;
                    uiRightFootRotX.Value = rot.x;
                    uiRightFootRotY.Value = rot.y;
                    uiRightFootRotZ.Value = rot.z;
                    break;
                default:
                    break;
            }
        }

    }

    protected void OnCfgUpdate(string name)
    {
        World world = GameManager.Instance.World;
        EntityPlayerLocal player = world.GetPrimaryPlayer();
        // Ensure we have a player model
        if (player.emodel == null)
        {
            Log.Error("Must be in a vehicle");
            return;
        }
        // This is really just a getter (adds once)
        IKController ikct = player.emodel.AddIKController();
        if (ikct == null) Log.Error("No IK Animator found on model?");
        // Update the pose setting for vehicle
        player.SetVehiclePoseMode((int)uiPose.Value);
        // Update the seat position and rotation
        player.ModelTransform.localPosition = new Vector3(
            (float)uiSeatPosX.Value,
            (float)uiSeatPosY.Value,
            (float)uiSeatPosZ.Value);
        player.ModelTransform.localEulerAngles = new Vector3(
            (float)uiSeatRotX.Value,
            (float)uiSeatRotY.Value,
            (float)uiSeatRotZ.Value);
        // States for debouncing
        bool handChanged = false;
        bool footChanged = false;
        // Update position and rotation for all ik targets
        for (int i = 0; i < ikct?.targets?.Count; i++)
        {
            Vector3 npos, nrot;
            var pos = GetIktPos(ikct.targets[i]);
            var rot = GetIktRot(ikct.targets[i]);
            IKController.Target ikt = ikct.targets[i];
            switch (ikt.avatarGoal)
            {
                case UnityEngine.AvatarIKGoal.LeftHand:
                    SetIktPos(ref ikt, npos = new Vector3(
                        (float)uiLeftHandPosX.Value,
                        (float)uiLeftHandPosY.Value,
                        (float)uiLeftHandPosZ.Value));
                    SetIktRot(ref ikt, nrot = new Vector3(
                        (float)uiLeftHandRotX.Value,
                        (float)uiLeftHandRotY.Value,
                        (float)uiLeftHandRotZ.Value));
                    handChanged |= (pos != npos);
                    handChanged |= (rot != nrot);
                    break;
                case UnityEngine.AvatarIKGoal.RightHand:
                    SetIktPos(ref ikt, npos = new Vector3(
                        (float)uiRightHandPosX.Value,
                        (float)uiRightHandPosY.Value,
                        (float)uiRightHandPosZ.Value));
                    SetIktRot(ref ikt, nrot = new Vector3(
                        (float)uiRightHandRotX.Value,
                        (float)uiRightHandRotY.Value,
                        (float)uiRightHandRotZ.Value));
                    handChanged |= (pos != npos);
                    handChanged |= (rot != nrot);
                    break;
                case UnityEngine.AvatarIKGoal.LeftFoot:
                    SetIktPos(ref ikt, npos = new Vector3(
                        (float)uiLeftFootPosX.Value,
                        (float)uiLeftFootPosY.Value,
                        (float)uiLeftFootPosZ.Value));
                    SetIktRot(ref ikt, nrot = new Vector3(
                        (float)uiLeftFootRotX.Value,
                        (float)uiLeftFootRotY.Value,
                        (float)uiLeftFootRotZ.Value));
                    footChanged |= (pos != npos);
                    footChanged |= (rot != nrot);
                    break;
                case UnityEngine.AvatarIKGoal.RightFoot:
                    SetIktPos(ref ikt, npos = new Vector3(
                        (float)uiRightFootPosX.Value,
                        (float)uiRightFootPosY.Value,
                        (float)uiRightFootPosZ.Value));
                    SetIktRot(ref ikt, nrot = new Vector3(
                        (float)uiRightFootRotX.Value,
                        (float)uiRightFootRotY.Value,
                        (float)uiRightFootRotZ.Value));
                    footChanged |= (pos != npos);
                    footChanged |= (rot != nrot);
                    break;
                default:
                    break;
            }
            // Assign back to struct
            ikct.targets[i] = ikt;
        }
        // This seems to cause crashes sometimes (probably when animation is still running)
        // ikct.GetComponent<UnityEngine.Animations.Rigging.RigBuilder>().graph.Stop();
        // ikct.GetComponent<UnityEngine.Animations.Rigging.RigBuilder>().Clear();
        // ikct.GetComponent<UnityEngine.Animations.Rigging.RigBuilder>().Build();
        // Avoid it at least when not necessary to change
        if (footChanged) ikct.ModifyRig();

    }

    public override void OnOpen()
    {
        base.OnOpen();
        // if (GameManager.IsDedicatedServer) return;
        //Cursor.lockState = SoftCursor.DefaultCursorLockState;
        //Cursor.visible = true;
    }

    public override void OnClose()
    {
        base.OnClose();
    }

}
