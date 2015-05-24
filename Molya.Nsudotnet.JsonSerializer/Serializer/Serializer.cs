using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Json_serial
{
    class Serializer
    {
        public Serializer()
        {}

        static public void SerializeObject(Object o)
        {
            Type type = o.GetType();
            FieldInfo[] info = type.GetFields();
            if (info.Length == 0)
            {
                Console.WriteLine("filed empty");
            }
            var file = new StreamWriter(type.ToString() + ".json");
            file.WriteLine('{');
            foreach (var fieldInfo in info)
            {
                if (!fieldInfo.IsNotSerialized)
                {
                    var builder = SerializeField(fieldInfo, o);

                    if (fieldInfo.Equals(info.Last()))
                    {
                        builder.Remove(builder.Length - 2, 2);
                    }

                    file.WriteLine(builder.ToString());

                    if (fieldInfo.GetValue(o) == null)
                    {
                        Console.WriteLine("fields name ("+fieldInfo.Name+") - " + "field with null serialized");
                    }
                    else
                    {
                        Console.WriteLine("fields name (" + fieldInfo.Name+ ") - " + fieldInfo.GetValue(o).GetType() + " serialized");
                    }
                }
                else
                {
                    if (fieldInfo.GetValue(o) != null)
                    {
                        Console.WriteLine("value " + fieldInfo.GetValue(o).GetType() + " not serialized");
                    }
                    else
                    {
                        Console.WriteLine("field with null NONserialized");
                    }
                }
            }
            file.WriteLine('}');
            file.Close();
        }


        static private StringBuilder SerializeField(FieldInfo fieldInfo, object obj)
        {
            var builder = new StringBuilder();

            if (fieldInfo.GetValue(obj) == null)
            {
                SerializeNullItem(fieldInfo, builder);
            }
            else if (fieldInfo.GetValue(obj).GetType().IsArray)
            {
                SerializeArrayItem(fieldInfo, obj, builder);
            }
            else if (fieldInfo.GetValue(obj) is ICollection)
            {
                SerializeListItem(fieldInfo, obj, builder);
            }
            else
            {
                SerializeItem(fieldInfo, obj, builder);
            }
            return builder;
        }

        static private void SerializeItem(FieldInfo fieldInfo, object obj, StringBuilder builder)
        {
            builder.Append("\t");
            builder.Append('\"' + fieldInfo.Name + "\": ");
            Object object1 = fieldInfo.GetValue(obj);
            if (object1 is string)
            {
                builder.Append("\"" + (string)object1 + "\", ");
            }
            else
            {
                builder.Append(object1 + ", ");
            }
        }


        static private void SerializeArrayItem(FieldInfo fieldInfo, object obj, StringBuilder builder)
        {
            builder.Append('\t');
            builder.Append('\"' + fieldInfo.Name + "\": ");
            builder.Append("[ ");
            int count = -1;
            foreach (var value in (IEnumerable)fieldInfo.GetValue(obj))
            {
                count++;
                if (value == null)
                {
                    builder.Append("null, ");
                }
                else if (value is string)
                {
                    builder.Append("\"" + value + "\", ");
                }
                else if (count != 0)
                {
                    builder.Append(" " + value + ",");
                }
                else
                {
                    builder.Append(value + ",");
                }
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" ], ");
        }


        static private void SerializeNullItem(FieldInfo fieldInfo, StringBuilder builder)
        {
            builder.Append('\t');
            builder.Append('\"' + fieldInfo.Name + "\": ");
            builder.Append("null, ");
        }

        static private void SerializeListItem(FieldInfo fieldInfo, object obj, StringBuilder builder)
        {
            builder.Append('\t');
            builder.Append('\"' + fieldInfo.Name + "\": ");
            builder.Append("[ ");
            int count = -1;
            foreach (var value in (IList)fieldInfo.GetValue(obj))
            {
                count++;
                if (value == null)
                {
                    builder.Append("null,");
                }
                else if (value is string)
                {
                    builder.Append("\"" + value + " \",");
                }
                else if (count != 0)
                {
                    builder.Append(" " + value + ",");
                }
                else
                {
                    builder.Append(value + ",");
                }
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" ], ");
        }

    }
}
