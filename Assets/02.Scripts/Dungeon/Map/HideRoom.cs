using UnityEngine;

public class HideRoom : MonoBehaviour
{
    private  MeshRenderer _meshRenderers;

    private void Awake()
    {
        _meshRenderers = GetComponent<MeshRenderer>();
    }
    public void ShowRoom() 
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();

        _meshRenderers.GetPropertyBlock(propBlock);
        propBlock.SetColor("_Color", new Color(0.5f, 0.5f, 0.5f, 1f));
        _meshRenderers.SetPropertyBlock(propBlock);
    }
}
