namespace CptClientShared.QueryForms
{
    public class QfNewValue
    {
        public CptValueType ValType { get; set; }
        public string String { get; set; } = string.Empty;
        public string ObjectName { get; set; } = string.Empty;
        public double Number { get; set; }
        public bool Bool { get; set; }
        public byte[] ByteArray { get; set; } = Array.Empty<byte>();
    }

    public enum CptValueType
    {
        String,
        Number,
        Boolean,
        ObjectName,
        ByteArray
    }
}