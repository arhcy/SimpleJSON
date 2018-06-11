Here I store my improvements for SimpleJSON lib. [[Original repository]](https://github.com/Bunny83/SimpleJSON)

### Original lib tweaks:

You can check if key contains in JSONClass
```C#
public override bool ContainsKey(string key)
```



You can get list of JSONNode children
```C#
 public List<JSONNode> GetChildrenList()
 ```

### My extensions:

In file SimpleJSONExtensions.cs

#### GetValueByType

```C#
public static object GetValueByType(this JSONNode param, Type NeedType)
```

This method is suitable for reflection routines. It automatically selects suitable property to get the value of JSONNode.

```C#
FieldInfo[] fields = thisType.GetFields();
object value = myNode.GetValueByType(fields[0].FieldType);
fields[0].SetValue(InstanceOfType, value);
```

#### ParseData

```C#
public static void ParseData(JSONClass param, object instance)
```

You can deserialize JSONClass instance to object. First of all you need to add ParsableValue attribute to fields you want to deserialize. This fields must be named same as fields in JSON you want to deserialize. If you want to use a different name you can specify it in attributes constructor

```C#
public class EnergyModel
{
	[ParsableValue]//Searching for MaxEnergy in JSON
        public float MaxEnergy;
        [ParsableValue("RSpeed")]//Searching for RSpeed in JSON
        public float EnergyRecreationSpeed;
}
```

To deserialize just call the method. ParseData currently not supports arrays and nested object. 

```C#
EnergyModel model = new EnergyModel();
SimpleJSONExtensions.ParseData(json, model);
```
        
