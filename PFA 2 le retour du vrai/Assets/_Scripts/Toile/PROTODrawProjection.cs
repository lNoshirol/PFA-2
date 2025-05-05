using UnityEngine;

public class PROTODrawProjection : MonoBehaviour
{
    [SerializeField] private CastSpriteShape jsaisPasGros;
    

    private void Update()
    {
        print(jsaisPasGros.GetDrawData());
    }
}
