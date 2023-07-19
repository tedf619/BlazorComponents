namespace BlazorComponents.Client
{
    public class EnumDto
    {
        public int Value { get; set; }
        public string Text { get; set; } = string.Empty;
        public override string ToString() { return Text; }
    }
}
