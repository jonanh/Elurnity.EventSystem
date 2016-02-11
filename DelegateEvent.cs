namespace Events
{
    public delegate void DelegateEvent<T>(T EventArgs) where T : Event;
}