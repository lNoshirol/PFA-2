using System.Collections.Generic;
using UnityEngine;

public class PROTODrawProjection : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private CastSpriteShape jsaisPasGros;

    public void Button()
    {
        Texture2D texture = new(1920, 1080, TextureFormat.ARGB32, false);

        foreach (Vector2 point in jsaisPasGros.GetDrawData().points)
        {
            print(point);
            texture.SetPixel((int)point.x, (int)point.y, Color.black);
        }
        texture.Apply();

        _renderer.material.mainTexture = texture;
    }

}
