namespace ET
{
    // 将 serverinfo 通过proto 传递
    [FriendOf(typeof(ServerInfo))]
    public static class ServerInfoSystem
    {
        public static void FromMessage(this ServerInfo self, ServerInfoProto serverInfoProto)
        {
            self.Id = serverInfoProto.Id;
            self.Status = serverInfoProto.Status;
            self.ServerName = serverInfoProto.ServerName;
        }

        public static ServerInfoProto ToMessage(this ServerInfo self)
        {
            Log.Debug("ServerInfoProto ToMessage");
            return new ServerInfoProto()
            {
                Id = (int)self.Id, 
                Status = self.Status, 
                ServerName = self.ServerName
            };
        }

    }
}