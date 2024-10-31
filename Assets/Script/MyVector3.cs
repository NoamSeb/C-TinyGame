using UnityEngine;

public struct MyVector3
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }


    public MyVector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public float Magnitude
    {
        get { return Mathf.Sqrt(SqrtMagnitude); }
    }

    public float SqrtMagnitude
    {
        get { return X * X + Y * Y + Z * Z; }
    }

    public MyVector3 Normalize()
    {
        float mag = Magnitude;
        this.X /= mag;
        this.Y /= mag;
        this.Z /= mag;
        return this;
    }

    #region Overpower operators

    public static MyVector3 operator +(MyVector3 vector1, MyVector3 vector2)
    {
        return new MyVector3(vector1.X + vector2.X, vector1.Y + vector2.Y, vector1.Z + vector2.Z);
    }

    public static MyVector3 operator *(MyVector3 vector1, MyVector3 vector2)
    {
        return new MyVector3(vector1.X * vector2.X, vector1.Y * vector2.Y, vector1.Z * vector2.Z);
    }

    public static MyVector3 operator *(MyVector3 vector1, int i)
    {
        return new MyVector3(vector1.X * i, vector1.Y * i, vector1.Z * i);
    }

    public static MyVector3 operator -(MyVector3 vector1, MyVector3 vector2)
    {
        return new MyVector3(vector1.X - vector2.X, vector1.Y - vector2.Y, vector1.Z - vector2.Z);
    }

    public static MyVector3 operator /(MyVector3 vector1, MyVector3 vector2)
    {
        return new MyVector3(vector1.X / vector2.X, vector1.Y / vector2.Y, vector1.Z / vector2.Z);
    }

    #endregion
}