namespace UCSB
{
    public class VKWall
    {
        public ResponseDescripton? Response { get; set; }
    }

    public class ResponseDescripton
    {
        public int Count { get; set; }
        public IEnumerable<ItemsDescriprion>? Items { get; set; }
    }

    public class ItemsDescriprion
    {
        public string? Text { get; set; }
    }
}
