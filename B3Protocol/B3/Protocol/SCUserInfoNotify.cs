// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace B3.Protocol
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SCUserInfoNotify : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_24_3_6(); }
  public static SCUserInfoNotify GetRootAsSCUserInfoNotify(ByteBuffer _bb) { return GetRootAsSCUserInfoNotify(_bb, new SCUserInfoNotify()); }
  public static SCUserInfoNotify GetRootAsSCUserInfoNotify(ByteBuffer _bb, SCUserInfoNotify obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SCUserInfoNotify __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public short MyUserID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetShort(o + __p.bb_pos) : (short)0; } }

  public static Offset<B3.Protocol.SCUserInfoNotify> CreateSCUserInfoNotify(FlatBufferBuilder builder,
      short MyUserID = 0) {
    builder.StartTable(1);
    SCUserInfoNotify.AddMyUserID(builder, MyUserID);
    return SCUserInfoNotify.EndSCUserInfoNotify(builder);
  }

  public static void StartSCUserInfoNotify(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddMyUserID(FlatBufferBuilder builder, short myUserID) { builder.AddShort(0, myUserID, 0); }
  public static Offset<B3.Protocol.SCUserInfoNotify> EndSCUserInfoNotify(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<B3.Protocol.SCUserInfoNotify>(o);
  }
}


static public class SCUserInfoNotifyVerify
{
  static public bool Verify(Google.FlatBuffers.Verifier verifier, uint tablePos)
  {
    return verifier.VerifyTableStart(tablePos)
      && verifier.VerifyField(tablePos, 4 /*MyUserID*/, 2 /*short*/, 2, false)
      && verifier.VerifyTableEnd(tablePos);
  }
}

}
