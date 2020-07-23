using UnityEngine.UI;

//Simple UI component for mouse event catching
public class EventTarget : Graphic
{
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
    }
}
