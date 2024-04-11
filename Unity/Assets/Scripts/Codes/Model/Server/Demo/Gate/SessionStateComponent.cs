namespace ET.Server
{
    public enum SessionState
    {
        Gate,
        Game,
    }

    [ComponentOf(typeof (Session))]
    public class SessionStateComponent: Entity, IAwake
    {
        public SessionState State { get; set; }
    }
}