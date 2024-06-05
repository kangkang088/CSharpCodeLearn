using System.Collections;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 玩家数据类
/// </summary>
public class PlayerData : BaseData
{
    public string name;
    public int atk;
    public int lev;

    public override int GetBytesNum()
    {
        return 4 + 4 + 4 + Encoding.UTF8.GetBytes(name).Length;
    }

    public override int Reading(byte[] bytes, int beginIndex = 0)
    {
        int index = beginIndex;
        name = ReadString(bytes, ref index);
        atk = ReadInt(bytes, ref index);
        lev = ReadInt(bytes, ref index);
        return index - beginIndex;
    }

    public override byte[] Writing()
    {
        int index = 0;
        byte[] bytes = new byte[GetBytesNum()];
        WriteString(bytes, name, ref index);
        WriteInt(bytes, atk, ref index);
        WriteInt(bytes, lev, ref index);
        return bytes;
    }
}
