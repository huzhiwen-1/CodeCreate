using System;

[AttributeUsage(AttributeTargets.Property)]
public class ColumnAttribute : System.Attribute
{
    private string _ColumnName;

    /// <summary>
    /// 列名
    /// </summary>
    public string ColumnName
    {
        get { return _ColumnName; }
        set { _ColumnName = value; }
    }

    private bool _IsPrimaryKey=false;

    /// <summary>
    /// 是否主键
    /// </summary>
    public bool IsPrimaryKey
    {
        get { return _IsPrimaryKey; }
        set { _IsPrimaryKey = value; }
    }

    public ColumnAttribute(string columnName)
    {
        _ColumnName = columnName;
        _IsPrimaryKey = false;
    }

    public ColumnAttribute(string columnName, bool isPrimaryKey)
    {
        _ColumnName = columnName;
        _IsPrimaryKey = isPrimaryKey;
    }
}
