using System.Collections.Generic;
using UnityEngine;

// Registration is independent of interaction and inventory.
[ExecuteAlways, DisallowMultipleComponent, RequireComponent(typeof(MeshRenderer))]
public sealed class CollectibleOutlineTarget : MonoBehaviour
{
    internal static readonly HashSet<CollectibleOutlineTarget> Active = new HashSet<CollectibleOutlineTarget>();
    [SerializeField] private MeshRenderer targetRenderer;
    public MeshRenderer TargetRenderer => targetRenderer != null ? targetRenderer : GetComponent<MeshRenderer>();

    private void Reset() => targetRenderer = GetComponent<MeshRenderer>();
    private void OnEnable() => Active.Add(this);
    private void OnDisable() => Active.Remove(this);
}
