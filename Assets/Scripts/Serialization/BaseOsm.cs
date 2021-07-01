using System;
using System.Xml;
using System.Globalization;

class BaseOsm
{
    protected T GettAttribute<T>(string attrName, XmlAttributeCollection attributes)
    {
        string strValue = attributes[attrName].Value;
        
        return (T)Convert.ChangeType(strValue, typeof(T));
    }

    protected float GetFloat(string attrName, XmlAttributeCollection attributes)
    {
        string strValue = attributes[attrName].Value;

        return float.Parse(strValue, CultureInfo.InvariantCulture);

    }
}
