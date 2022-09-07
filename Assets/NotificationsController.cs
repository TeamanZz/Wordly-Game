using System.Collections;
using System.Collections.Generic;
using BBG;
using UnityEngine;

public class NotificationsController : MonoBehaviour, ISaveable
{
    public static NotificationsController Instance;
    public bool isNotificationsOn;
    public string SaveId { get { return "notifications_controller"; } }

    private void Awake()
    {
        Instance = this;
        SaveManager.Instance.Register(this);

        if (!LoadSave())
        {
            // Debug.Log("!loadsave");
            isNotificationsOn = true;
        }
    }

    public void SetNotificationsOnOff(bool isOn)
    {
        if (isOn == isNotificationsOn)
        {
            return;
        }

        isNotificationsOn = isOn;
    }

    public Dictionary<string, object> Save()
    {
        Dictionary<string, object> json = new Dictionary<string, object>();

        json["is_notifications_on"] = isNotificationsOn;

        return json;
    }

    public bool LoadSave()
    {
        JSONNode json = SaveManager.Instance.LoadSave(this);

        if (json == null)
        {
            return false;
        }

        isNotificationsOn = json["is_notifications_on"].AsBool;

        return true;
    }

}
