namespace ContentConsole.Extentions
{
  using System;
  using System.ComponentModel;
  using System.Reflection;

  public static class Extentions
  {
    public static string GetDescription(this Enum value)
    {
      FieldInfo fi = value.GetType().GetField(value.ToString());

      var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

      if (attributes.Length > 0)
        return attributes[0].Description;

      return String.Empty;
    }

    public static T ToEnum<T>(this int source)
    {
      return (T)Enum.ToObject(typeof(T), source);
    }
  }
}
