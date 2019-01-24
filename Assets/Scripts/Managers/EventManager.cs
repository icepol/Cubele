using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

class VectorEvent : UnityEvent<Vector3> {

}

class BoolEvent : UnityEvent<bool> {

}

class IntEvent : UnityEvent<int> {

}

class StringEvent : UnityEvent<string> {

}

public class EventManager : MonoBehaviour {

    static Dictionary<string, UnityEvent> eventsVoid = new Dictionary<string, UnityEvent>();
    static Dictionary<string, UnityEvent<Vector3>> eventsVector3 = new Dictionary<string, UnityEvent<Vector3>>();
    static Dictionary<string, UnityEvent<bool>> eventsBool = new Dictionary<string, UnityEvent<bool>>();
    static Dictionary<string, UnityEvent<int>> eventsInt = new Dictionary<string, UnityEvent<int>>();
    static Dictionary<string, UnityEvent<string>> eventsString = new Dictionary<string, UnityEvent<string>>();

    #region VOID
    public static void AddListener(string eventName, UnityAction listener) {
        if (!eventsVoid.ContainsKey(eventName)) {
            eventsVoid.Add(eventName, new UnityEvent());
        }

        eventsVoid[eventName].AddListener(listener);
    }

    public static void RemoveListener(string eventName, UnityAction listener) {
        if (!eventsVoid.ContainsKey(eventName))
            return;

        eventsVoid[eventName].RemoveListener(listener);
    }

    public static void TriggerEvent(string eventName) {
        if (eventsVoid.ContainsKey(eventName))
            eventsVoid[eventName].Invoke();
    }
    #endregion

    #region VECTOR
    public static void AddListener(string eventName, UnityAction<Vector3> listener) {
        if (!eventsVector3.ContainsKey(eventName)) {
            eventsVector3.Add(eventName, new VectorEvent());
        }

        eventsVector3[eventName].AddListener(listener);
    }

    public static void RemoveListener(string eventName, UnityAction<Vector3> listener) {
        if (!eventsVector3.ContainsKey(eventName))
            return;

        eventsVector3[eventName].RemoveListener(listener);
    }

    public static void TriggerEvent(string eventName, Vector3 arg) {
        if (eventsVector3.ContainsKey(eventName))
            eventsVector3[eventName].Invoke(arg);
    }
    #endregion

    #region BOOL
    public static void AddListener(string eventName, UnityAction<bool> listener) {
        if (!eventsBool.ContainsKey(eventName)) {
            eventsBool.Add(eventName, new BoolEvent());
        }

        eventsBool[eventName].AddListener(listener);
    }

    public static void RemoveListener(string eventName, UnityAction<bool> listener) {
        if (!eventsBool.ContainsKey(eventName))
            return;

        eventsBool[eventName].RemoveListener(listener);
    }

    public static void TriggerEvent(string eventName, bool arg) {
        if (eventsBool.ContainsKey(eventName))
            eventsBool[eventName].Invoke(arg);
    }
    # endregion

    #region INT
    public static void AddListener(string eventName, UnityAction<int> listener) {
        if (!eventsInt.ContainsKey(eventName)) {
            eventsInt.Add(eventName, new IntEvent());
        }

        eventsInt[eventName].AddListener(listener);
    }

    public static void RemoveListener(string eventName, UnityAction<int> listener) {
        if (!eventsInt.ContainsKey(eventName))
            return;

        eventsInt[eventName].RemoveListener(listener);
    }

    public static void TriggerEvent(string eventName, int arg) {
        if (eventsInt.ContainsKey(eventName))
            eventsInt[eventName].Invoke(arg);
    }
    # endregion

    #region STRING
    public static void AddListener(string eventName, UnityAction<string> listener) {
        if (!eventsString.ContainsKey(eventName)) {
            eventsString.Add(eventName, new StringEvent());
        }

        eventsString[eventName].AddListener(listener);
    }

    public static void RemoveListener(string eventName, UnityAction<string> listener) {
        if (!eventsString.ContainsKey(eventName))
            return;

        eventsString[eventName].RemoveListener(listener);
    }

    public static void TriggerEvent(string eventName, string arg) {
        if (eventsString.ContainsKey(eventName))
            eventsString[eventName].Invoke(arg);
    }
    # endregion
}
