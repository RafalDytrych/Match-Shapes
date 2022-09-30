using UnityEngine;

public static class Extensions
{
    public static void On(this GameObject go) => go.SetActive(true);
    public static void Off(this GameObject go) => go.SetActive(false);
    public static void SelfActive(this Panel p) => p.gameObject.SetActive(!p.gameObject.activeSelf);
}
