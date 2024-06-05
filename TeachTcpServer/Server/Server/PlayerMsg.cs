using System.Collections;
using System.Collections.Generic;

public class PlayerMsg : BaseMsg
{
    public int playerID;
    public PlayerData playerData;
    public override byte[] Writing()
    {
        int index = 0;
        byte[] bytes = new byte[GetBytesNum()];
        WriteInt(bytes, GetID(), ref index);
        WriteInt(bytes, playerID, ref index);
        WriteData(bytes, playerData, ref index);
        return bytes;
    }
    public override int Reading(byte[] bytes, int beginIndex = 0)
    {
        //不需要解析消息ID，因为在解析消息内容之前，就要把消息类型ID先解析出来，用来判断到底使用哪一个消息类来反序列化
        int index = beginIndex;
        playerID = ReadInt(bytes, ref index);
        playerData = ReadData<PlayerData>(bytes, ref index);
        return index - beginIndex;
    }
    public override int GetBytesNum()
    {
        return 4 + 4 + playerData.GetBytesNum();
    }
    public override int GetID()
    {
        return 1001;
    }
}
