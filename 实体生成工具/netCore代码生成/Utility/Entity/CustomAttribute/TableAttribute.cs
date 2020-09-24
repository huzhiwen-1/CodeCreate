using System;

[AttributeUsage(AttributeTargets.Class)]
public class TableAttribute : System.Attribute
{

    private string _name;


    public string TableName
    {
        get { return _name; }
        set { _name = value; }
    }

    public TableAttribute(string name)
    {
        _name = name;
    }
}