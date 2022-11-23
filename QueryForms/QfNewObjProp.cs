namespace CptClientShared.QueryForms
{
    public class QfNewObjProp
    {
        public string PropertyName { get; set; } = string.Empty;
        public List<QfNewValue> Values { get; set; } = new();
    }
}