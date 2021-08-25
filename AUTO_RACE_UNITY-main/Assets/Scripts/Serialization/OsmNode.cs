using System.Numerics;
using System.Xml;

class OsmNode : BaseOsm
{
    public ulong ID { get; private set; }
    public float Latitude { get; private set; }

    public float Longitude { get; private set; }

    public float X { get; private set; }

    public float Y { get; private set; }

    public static implicit operator Vector3 (OsmNode node)
    {
        return new Vector3(node.X, 0, node.Y);
    }


    public OsmNode(XmlNode node)
    {
        ID = GettAttribute<ulong>("id", node.Attributes);
        Latitude = GetFloat("lat", node.Attributes);
        Longitude = GetFloat("lon", node.Attributes);

        X = (float)MercatorProjection.lonToX(Longitude);
        Y = (float)MercatorProjection.latToY(Latitude);
    }
}
