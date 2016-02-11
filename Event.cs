namespace Events
{
    public class Event
    {
        public string name
        {
            get
            {
                return this.GetType().ToString();
            }
        }

        public bool stopPropagation { get; set; }
    }
}