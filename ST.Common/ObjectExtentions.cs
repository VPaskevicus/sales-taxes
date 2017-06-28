using Newtonsoft.Json;

namespace ST.Common
{
    public static class ObjectExtentions
    {
        public static string ToJsonString(this object o)
        {
            return JsonConvert.SerializeObject(o);
        }
    }
}
