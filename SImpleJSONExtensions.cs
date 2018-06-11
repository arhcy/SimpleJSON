using System;
using System.Reflection;
using SimpleJSON;

namespace artics.extensions.SimpleJSON
{
    public class ParsableValue : Attribute
    {
        public string Name;

        public ParsableValue() { }

        public ParsableValue(string name)
        {
            Name = name;
        }
    }

    public static class SimpleJSONExtensions
    {
        public static void ParseData(JSONClass param, object instance)
        {
            Type thisType = instance.GetType();
            FieldInfo[] fields = thisType.GetFields();

            foreach (var info in fields)
            {
                ParsableValue attribute = info.GetCustomAttribute<ParsableValue>(true);

                if (attribute == null)
                    continue;

                string name = attribute.Name != null && attribute.Name != string.Empty ? attribute.Name : info.Name;

                if (param.containsKey(name))
                {
                    object value = param[name].GetValueByType(info.FieldType);
                    info.SetValue(instance, value);
                }
            }
        }

        public static object GetValueByType(this JSONNode param, Type NeedType)
        {
            if (NeedType == typeof(int))
                return param.AsInt;
            else
            if (NeedType == typeof(float))
                return param.AsFloat;
            else
            if (NeedType == typeof(double))
                return param.AsDouble;
            else
            if (NeedType == typeof(long))
                return param.AsLong;
            else
            if (NeedType == typeof(bool))
                return param.AsBool;
            else
            if (NeedType == typeof(string))
                return param.Value;

            throw (new Exception("Type " + NeedType.Name + " is not supported"));
        }

        public static T GetValueByType<T>(this JSONNode param)
        {
            Type NeedType = typeof(T);

            if (NeedType == typeof(int))
                return (T)(object)param.AsInt;
            else
            if (NeedType == typeof(float))
                return (T)(object)param.AsFloat;
            else
            if (NeedType == typeof(double))
                return (T)(object)param.AsDouble;
            else
            if (NeedType == typeof(long))
                return (T)(object)param.AsLong;
            else
            if (NeedType == typeof(bool))
                return (T)(object)param.AsBool;
            else
            if (NeedType == typeof(string))
                return (T)(object)param.Value;

            throw (new Exception("Type " + NeedType.Name + " is not supported"));
            //return default(T);
        }
    }
}