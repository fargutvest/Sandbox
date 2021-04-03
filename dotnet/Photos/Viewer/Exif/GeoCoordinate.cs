// -------------------------------------------------------------------------------------------------
//
// Library "CompactExifLib" for reading and writing EXIF data in JPEG image files.
//
// © Copyright 2021 Hans-Peter Kalb
//
// Version 1.4, Date 2021-03-31
// •Bug-fix: If a tag ID incorrectly exists more than once an exception is not thrown any more.
//  Instead the first occurrence of the tag ID is used.
// •New exception class "ExifException" added.
// •Load options for "ExifData" constructor added. Now an empty EXIF block can be crated.
// •"ExifData" constructor without parameters removed.
// •When a JPEG file is saved the temporary file name contains the text "~" instead of "~temp".
//
// Version 1.3, Date 2021-03-15
// •"ExifData" constructor: Overloaded constructor added which can load from a stream.
// •"ExifData" constructor: Overloaded constructor added to create an empty EXIF block.
// •Method "Save": Overloaded method added which can save the EXIF data in a stream.
// •Method "GetTagValueCount": Parameter structure of the method changed. Info, if the tag exists, is now given back.
// •Method "SetTagValueCount": Overloaded method added which doesn't have a parameter for the tag type.
// •Method "GetTagType" added for reading the tag type.
//
// Version 1.2, Date 2021-02-13
// •Bug-fix: String comparison of file names changed from linguistic to ordinal comparison.
// •Method "IfdExists" added.
// •Type "ExifRational" extended by methods "ToDecimal" and "FromDecimal".
// •Type "GeoCoordinate" and special methods for reading and writing of GPS tags added: "Get-/SetGpsLongitude" etc.
// •Methods for reading and writing of date and time values with milliseconds added: "Get-/SetDateTaken" etc.
// •Searching for a tag ID speeded up by using the C# class "Dictionary" instead of "ArrayList".
// •Overloaded method "GetTagRawData" added which copies the raw data.
// •Type declarations "uint" and "ushort" removed from the enum types "ExifTag", "ExifIfd", "ExifTagId" and "ExifTagType".
// •Enum type "TimeFormat" renamed to "ExifDateFormat".
// •Method "GetByteCountOfTag" renamed to "GetTagByteCount".
// •Enum type "StrCodingFormat" added.
// •Demo application added.
//
// Version 1.1, Date 2020-05-16
// •Additional string coding constants for tag "UserComment" added.
// •Constant "StrCoding.Utf16Id_Undef" renamed to "StrCoding.IdCode_Utf16".
// •The method "Save" extended by an optional parameter. With this parameter, it is possible to remove JPEG blocks
//  with alternative description formats like IPTC-IIM, MPF and the Adobe Photoshop Information Resource Block.
// •In the method "Save" the order, in which the JPEG blocks are written, revised
//
// Version 1.0, Date 2020-05-01
// •Initial version.
//


// -------------------------------------------------------------------------------------------------


namespace CompactExifLib
{
    public struct GeoCoordinate
  {
    public decimal Degree; // Integer number: 0 ≤ Degree ≤ 90 (for latitudes) or 180 (for longitudes)
    public decimal Minute; // Integer number: 0 ≤ Minute < 60
    public decimal Second; // Fraction number: 0 ≤ Second < 60
    public char CardinalPoint; // For latitudes: 'N' or 'S'; for longitudes: 'E' or 'W'


    static public decimal ToDecimal(GeoCoordinate Value)
    {
      decimal DecimalDegree = Value.Degree + Value.Minute / 60 + Value.Second / 3600;
      if ((Value.CardinalPoint == 'S') || (Value.CardinalPoint == 'W'))
      {
        DecimalDegree = -DecimalDegree;
      }
      return (DecimalDegree);
    }


    static public GeoCoordinate FromDecimal(decimal Value, bool IsLatitude)
    {
      decimal AbsValue;
      GeoCoordinate ret;

      if (Value >= 0)
      {
        ret.CardinalPoint = IsLatitude ? 'N' : 'E';
        AbsValue = Value;
      }
      else
      {
        ret.CardinalPoint = IsLatitude ? 'S' : 'W';
        AbsValue = -Value;
      }
      ret.Degree = decimal.Truncate(AbsValue);
      decimal frac = (AbsValue - ret.Degree) * 60;
      ret.Minute = decimal.Truncate(frac);
      ret.Second = (frac - ret.Minute) * 60;
      return (ret);
    }
  }
}
