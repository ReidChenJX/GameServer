using ET;
using ProtoBuf;
using System.Collections.Generic;
namespace ET
{
// using
	[ResponseType(nameof(ObjectQueryResponse))]
	[Message(InnerMessage.ObjectQueryRequest)]
	[ProtoContract]
	public partial class ObjectQueryRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long Key { get; set; }

		[ProtoMember(3)]
		public long InstanceId { get; set; }

	}

	[ResponseType(nameof(A2M_Reload))]
	[Message(InnerMessage.M2A_Reload)]
	[ProtoContract]
	public partial class M2A_Reload: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(InnerMessage.A2M_Reload)]
	[ProtoContract]
	public partial class A2M_Reload: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2G_LockResponse))]
	[Message(InnerMessage.G2G_LockRequest)]
	[ProtoContract]
	public partial class G2G_LockRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long Id { get; set; }

		[ProtoMember(3)]
		public string Address { get; set; }

	}

	[Message(InnerMessage.G2G_LockResponse)]
	[ProtoContract]
	public partial class G2G_LockResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2G_LockReleaseResponse))]
	[Message(InnerMessage.G2G_LockReleaseRequest)]
	[ProtoContract]
	public partial class G2G_LockReleaseRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long Id { get; set; }

		[ProtoMember(3)]
		public string Address { get; set; }

	}

	[Message(InnerMessage.G2G_LockReleaseResponse)]
	[ProtoContract]
	public partial class G2G_LockReleaseResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(ObjectAddResponse))]
	[Message(InnerMessage.ObjectAddRequest)]
	[ProtoContract]
	public partial class ObjectAddRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long Key { get; set; }

		[ProtoMember(3)]
		public long InstanceId { get; set; }

	}

	[Message(InnerMessage.ObjectAddResponse)]
	[ProtoContract]
	public partial class ObjectAddResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(ObjectLockResponse))]
	[Message(InnerMessage.ObjectLockRequest)]
	[ProtoContract]
	public partial class ObjectLockRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long Key { get; set; }

		[ProtoMember(3)]
		public long InstanceId { get; set; }

		[ProtoMember(4)]
		public int Time { get; set; }

	}

	[Message(InnerMessage.ObjectLockResponse)]
	[ProtoContract]
	public partial class ObjectLockResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(ObjectUnLockResponse))]
	[Message(InnerMessage.ObjectUnLockRequest)]
	[ProtoContract]
	public partial class ObjectUnLockRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long Key { get; set; }

		[ProtoMember(3)]
		public long OldInstanceId { get; set; }

		[ProtoMember(4)]
		public long InstanceId { get; set; }

	}

	[Message(InnerMessage.ObjectUnLockResponse)]
	[ProtoContract]
	public partial class ObjectUnLockResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(ObjectRemoveResponse))]
	[Message(InnerMessage.ObjectRemoveRequest)]
	[ProtoContract]
	public partial class ObjectRemoveRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long Key { get; set; }

	}

	[Message(InnerMessage.ObjectRemoveResponse)]
	[ProtoContract]
	public partial class ObjectRemoveResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(ObjectGetResponse))]
	[Message(InnerMessage.ObjectGetRequest)]
	[ProtoContract]
	public partial class ObjectGetRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long Key { get; set; }

	}

	[Message(InnerMessage.ObjectGetResponse)]
	[ProtoContract]
	public partial class ObjectGetResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public long InstanceId { get; set; }

	}

	[ResponseType(nameof(G2R_GetLoginKey))]
	[Message(InnerMessage.R2G_GetLoginKey)]
	[ProtoContract]
	public partial class R2G_GetLoginKey: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long AccountId { get; set; }

	}

	[Message(InnerMessage.G2R_GetLoginKey)]
	[ProtoContract]
	public partial class G2R_GetLoginKey: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public long Key { get; set; }

		[ProtoMember(5)]
		public long GateId { get; set; }

	}

	[Message(InnerMessage.G2M_SessionDisconnect)]
	[ProtoContract]
	public partial class G2M_SessionDisconnect: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(InnerMessage.ObjectQueryResponse)]
	[ProtoContract]
	public partial class ObjectQueryResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public byte[] Entity { get; set; }

	}

	[ResponseType(nameof(M2M_UnitTransferResponse))]
	[Message(InnerMessage.M2M_UnitTransferRequest)]
	[ProtoContract]
	public partial class M2M_UnitTransferRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long OldInstanceId { get; set; }

		[ProtoMember(3)]
		public byte[] Unit { get; set; }

		[ProtoMember(4)]
		public List<byte[]> Entitys { get; set; }

		[ProtoMember(5)]
		public int ZoneId { get; set; }

	}

	[Message(InnerMessage.M2M_UnitTransferResponse)]
	[ProtoContract]
	public partial class M2M_UnitTransferResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(L2R_LoginAccountResponse))]
	[Message(InnerMessage.R2L_LoginAccountRequest)]
	[ProtoContract]
	public partial class R2L_LoginAccountRequest: ProtoObject, IActorRequest
	{
// Realm 向LoginCenter 验证账户是否登录
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long AccountId { get; set; }

	}

	[Message(InnerMessage.L2R_LoginAccountResponse)]
	[ProtoContract]
	public partial class L2R_LoginAccountResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2L_DisconnectGateUnit))]
	[Message(InnerMessage.L2G_DisconnectGateUnit)]
	[ProtoContract]
	public partial class L2G_DisconnectGateUnit: ProtoObject, IActorRequest
	{
// LoginCenter 向 Gate 请求下线某个用户
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long AccountId { get; set; }

	}

	[Message(InnerMessage.G2L_DisconnectGateUnit)]
	[ProtoContract]
	public partial class G2L_DisconnectGateUnit: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(L2G_AddLoginRecord))]
	[Message(InnerMessage.G2L_AddLoginRecord)]
	[ProtoContract]
	public partial class G2L_AddLoginRecord: ProtoObject, IActorRequest
	{
// Gate 向 LoginCenter 记录登录的用户
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long AccountId { get; set; }

		[ProtoMember(3)]
		public int ZoneId { get; set; }

	}

	[Message(InnerMessage.L2G_AddLoginRecord)]
	[ProtoContract]
	public partial class L2G_AddLoginRecord: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2G_RequestEnterGameState))]
	[Message(InnerMessage.G2M_RequestEnterGameState)]
	[ProtoContract]
	public partial class G2M_RequestEnterGameState: ProtoObject, IActorLocationRequest
	{
// Gate 向 Game 确认角色是否在游戏服
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(InnerMessage.M2G_RequestEnterGameState)]
	[ProtoContract]
	public partial class M2G_RequestEnterGameState: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2G_RequestExitGame))]
	[Message(InnerMessage.G2M_RequestExitGame)]
	[ProtoContract]
	public partial class G2M_RequestExitGame: ProtoObject, IActorLocationRequest
	{
// Gate 向 Game 角色下线请求
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(InnerMessage.M2G_RequestExitGame)]
	[ProtoContract]
	public partial class M2G_RequestExitGame: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(L2G_RemoveLoginRecord))]
	[Message(InnerMessage.G2L_RemoveLoginRecord)]
	[ProtoContract]
	public partial class G2L_RemoveLoginRecord: ProtoObject, IActorRequest
	{
// Gate 向 LoginCenter 下线登录的用户
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long AccountId { get; set; }

		[ProtoMember(3)]
		public int ZoneId { get; set; }

	}

	[Message(InnerMessage.L2G_RemoveLoginRecord)]
	[ProtoContract]
	public partial class L2G_RemoveLoginRecord: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2G_UnitDataSave))]
	[Message(InnerMessage.G2M_UnitDataSave)]
	[ProtoContract]
	public partial class G2M_UnitDataSave: ProtoObject, IActorLocationRequest
	{
// 向 Map 保存角色数据
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(InnerMessage.M2G_UnitDataSave)]
	[ProtoContract]
	public partial class M2G_UnitDataSave: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

// -----------玩家缓存相关-------------------
	[ResponseType(nameof(UnitCache2Other_AddOrUpdateUnit))]
	[Message(InnerMessage.Other2UnitCache_AddOrUpdateUnit)]
	[ProtoContract]
	public partial class Other2UnitCache_AddOrUpdateUnit: ProtoObject, IActorRequest
	{
// 增加或者更新UnitCache
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long UnitId { get; set; }

		[ProtoMember(3)]
		public List<string> EntityTypes { get; set; }

		[ProtoMember(4)]
		public List<byte[]> EntityBytes { get; set; }

	}

	[Message(InnerMessage.UnitCache2Other_AddOrUpdateUnit)]
	[ProtoContract]
	public partial class UnitCache2Other_AddOrUpdateUnit: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(UnitCache2Other_GetUnit))]
	[Message(InnerMessage.Other2UnitCache_GetUnit)]
	[ProtoContract]
	public partial class Other2UnitCache_GetUnit: ProtoObject, IActorRequest
	{
// 获取Unit缓存
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long UnitId { get; set; }

		[ProtoMember(3)]
		public List<string> ComponentNameList { get; set; }

	}

	[Message(InnerMessage.UnitCache2Other_GetUnit)]
	[ProtoContract]
	public partial class UnitCache2Other_GetUnit: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public List<Entity> EntityList { get; set; }

		[ProtoMember(5)]
		public List<string> ComponentNameList { get; set; }

	}

	[ResponseType(nameof(UnitCache2Other_DeleteUnit))]
	[Message(InnerMessage.Other2UnitCache_DeleteUnit)]
	[ProtoContract]
	public partial class Other2UnitCache_DeleteUnit: ProtoObject, IActorRequest
	{
// 删除Unit缓存
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long UnitId { get; set; }

	}

	[Message(InnerMessage.UnitCache2Other_DeleteUnit)]
	[ProtoContract]
	public partial class UnitCache2Other_DeleteUnit: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	public static class InnerMessage
	{
		 public const ushort ObjectQueryRequest = 20002;
		 public const ushort M2A_Reload = 20003;
		 public const ushort A2M_Reload = 20004;
		 public const ushort G2G_LockRequest = 20005;
		 public const ushort G2G_LockResponse = 20006;
		 public const ushort G2G_LockReleaseRequest = 20007;
		 public const ushort G2G_LockReleaseResponse = 20008;
		 public const ushort ObjectAddRequest = 20009;
		 public const ushort ObjectAddResponse = 20010;
		 public const ushort ObjectLockRequest = 20011;
		 public const ushort ObjectLockResponse = 20012;
		 public const ushort ObjectUnLockRequest = 20013;
		 public const ushort ObjectUnLockResponse = 20014;
		 public const ushort ObjectRemoveRequest = 20015;
		 public const ushort ObjectRemoveResponse = 20016;
		 public const ushort ObjectGetRequest = 20017;
		 public const ushort ObjectGetResponse = 20018;
		 public const ushort R2G_GetLoginKey = 20019;
		 public const ushort G2R_GetLoginKey = 20020;
		 public const ushort G2M_SessionDisconnect = 20021;
		 public const ushort ObjectQueryResponse = 20022;
		 public const ushort M2M_UnitTransferRequest = 20023;
		 public const ushort M2M_UnitTransferResponse = 20024;
		 public const ushort R2L_LoginAccountRequest = 20025;
		 public const ushort L2R_LoginAccountResponse = 20026;
		 public const ushort L2G_DisconnectGateUnit = 20027;
		 public const ushort G2L_DisconnectGateUnit = 20028;
		 public const ushort G2L_AddLoginRecord = 20029;
		 public const ushort L2G_AddLoginRecord = 20030;
		 public const ushort G2M_RequestEnterGameState = 20031;
		 public const ushort M2G_RequestEnterGameState = 20032;
		 public const ushort G2M_RequestExitGame = 20033;
		 public const ushort M2G_RequestExitGame = 20034;
		 public const ushort G2L_RemoveLoginRecord = 20035;
		 public const ushort L2G_RemoveLoginRecord = 20036;
		 public const ushort G2M_UnitDataSave = 20037;
		 public const ushort M2G_UnitDataSave = 20038;
		 public const ushort Other2UnitCache_AddOrUpdateUnit = 20039;
		 public const ushort UnitCache2Other_AddOrUpdateUnit = 20040;
		 public const ushort Other2UnitCache_GetUnit = 20041;
		 public const ushort UnitCache2Other_GetUnit = 20042;
		 public const ushort Other2UnitCache_DeleteUnit = 20043;
		 public const ushort UnitCache2Other_DeleteUnit = 20044;
	}
}
