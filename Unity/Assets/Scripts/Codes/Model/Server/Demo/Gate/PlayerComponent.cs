using System.Collections.Generic;
using System.Linq;

namespace ET.Server
{
	[ComponentOf(typeof(Scene))]
	public class PlayerComponent : Entity, IAwake, IDestroy
	{
		// {player.AccountId, player}
		public readonly Dictionary<long, Player> idPlayers = new Dictionary<long, Player>();
	}
}