using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace TilemapShadowCaster.Runtime
{
    public class PathShadow : ShadowCaster2D
    {
        static FieldInfo shapeFieldInfo = typeof(ShadowCaster2D).GetField("m_ShapePath",
            BindingFlags.NonPublic | BindingFlags.Instance);

        static FieldInfo shapeHashFieldInfo = typeof(ShadowCaster2D).GetField("m_ShapePathHash",
            BindingFlags.NonPublic | BindingFlags.Instance);

        static FieldInfo sortingLayersFieldInfo = typeof(ShadowCaster2D).GetField("m_ApplyToSortingLayers",
            BindingFlags.NonPublic | BindingFlags.Instance);

        internal void SetShape(List<Vector2> points,  int[] sortingLayers)
        {
            Vector3[] shapev3 = points.ConvertAll((point) => new Vector3(point.x, point.y)).ToArray();
            shapeFieldInfo.SetValue(this, shapev3);
            sortingLayersFieldInfo.SetValue(this, sortingLayers);
            shapeHashFieldInfo.SetValue(this, (int) UnityEngine.Random.Range(0f, 10000000f));
        }
    }
}