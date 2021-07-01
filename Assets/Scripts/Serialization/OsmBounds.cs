using System.Xml;

class OsmBounds : BaseOsm
{
    public float MinLat { get; private set; }
    
    public float MaxLat { get; private set; }

    public float MinLon { get; private set; }

    public float MaxLon { get; private set; }

    public UnityEngine.Vector3 Centre { get; private set; }

    public OsmBounds(XmlNode node)
    {
        MinLat = GetFloat("minlat", node.Attributes);
        MaxLat = GetFloat("maxlat", node.Attributes);
        MinLon = GetFloat("minlon", node.Attributes);
        MaxLon = GetFloat("maxlon", node.Attributes);   

        float x = (float)((MercatorProjection.lonToX(MaxLon) + MercatorProjection.lonToX(MinLon)) / 2);
        float y = (float)((MercatorProjection.latToY(MaxLat) + MercatorProjection.latToY(MinLat)) / 2);

        Centre = new UnityEngine.Vector3(x, 0, y);
    }
}
