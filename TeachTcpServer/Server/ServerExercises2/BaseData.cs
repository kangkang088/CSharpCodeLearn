using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public abstract class BaseData
{
    /// <summary>
    /// 获取字节数组大小
    /// </summary>
    /// <returns></returns>
    public abstract int GetBytesNum();
    /// <summary>
    /// 获取对象字节数组
    /// </summary>
    /// <returns></returns>
    public abstract byte[] Writing();
    /// <summary>
    /// 将字节数组反序列化为对象
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="beginIndex"></param>
    /// <returns></returns>
    public abstract int Reading(byte[] bytes, int beginIndex = 0);
    /// <summary>
    /// 将int类型变量转换为字节数组并存储到容器中
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="value"></param>
    /// <param name="index"></param>
    protected void WriteInt(byte[] bytes, int value, ref int index)
    {
        BitConverter.GetBytes(value).CopyTo(bytes, index);
        index += 4;
    }
    protected void WriteShort(byte[] bytes, short value, ref int index)
    {
        BitConverter.GetBytes(value).CopyTo(bytes, index);
        index += 2;
    }
    protected void WriteLong(byte[] bytes, long value, ref int index)
    {
        BitConverter.GetBytes(value).CopyTo(bytes, index);
        index += 8;
    }
    protected void WriteFloat(byte[] bytes, float value, ref int index)
    {
        BitConverter.GetBytes(value).CopyTo(bytes, index);
        index += 4;
    }
    protected void WriteByte(byte[] bytes, byte value, ref int index)
    {
        bytes[index] = value;
        index += 1;
    }
    protected void WriteBool(byte[] bytes, bool value, ref int index)
    {
        BitConverter.GetBytes(value).CopyTo(bytes, index);
        index += 1;
    }
    protected void WriteString(byte[] bytes, string value, ref int index)
    {
        byte[] bytes1 = Encoding.UTF8.GetBytes(value);
        WriteInt(bytes, bytes1.Length, ref index);
        bytes1.CopyTo(bytes, index);
        index += bytes1.Length;
    }
    protected void WriteData(byte[] bytes, BaseData data, ref int index)
    {
        data.Writing().CopyTo(bytes, index);
        index += data.GetBytesNum();
    }
    protected int ReadInt(byte[] bytes, ref int index)
    {
        int value = BitConverter.ToInt32(bytes, index);
        index += 4;
        return value;
    }
    protected short ReadShort(byte[] bytes, ref int index)
    {
        short value = BitConverter.ToInt16(bytes, index);
        index += 2;
        return value;
    }
    protected long ReadLong(byte[] bytes, ref int index)
    {
        long value = BitConverter.ToInt64(bytes, index);
        index += 8;
        return value;
    }
    protected float ReadFloat(byte[] bytes, ref int index)
    {
        float value = BitConverter.ToSingle(bytes, index);
        index += 4;
        return value;
    }
    protected byte ReadByte(byte[] bytes, ref int index)
    {
        byte value = bytes[index];
        index += 1;
        return value;
    }
    protected bool ReadBool(byte[] bytes, ref int index)
    {
        bool value = BitConverter.ToBoolean(bytes, index);
        index += 1;
        return value;
    }
    protected string ReadString(byte[] bytes, ref int index)
    {
        int length = ReadInt(bytes, ref index);
        string value = Encoding.UTF8.GetString(bytes, index, length);
        index += length;
        return value;
    }
    protected T ReadData<T>(byte[] bytes, ref int index) where T : BaseData, new()
    {
        T value = new T();
        index += value.Reading(bytes, index);
        return value;
    }
}
