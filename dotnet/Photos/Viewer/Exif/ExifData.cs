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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CompactExifLib
{
    public class ExifData
    {
        #region Public area
        public const int IfdShift = 16;


        // Load EXIF data of a JPEG file.
        // An exception occurs in the following situations:
        //  •The file does not exist.
        //  •The access to the file is denied.
        //  •The file is not a valid JPEG file.
        //
        // If the file is a valid JPEG file but without an EXIF block, an empty EXIF block is created.
        public ExifData(string FileNameWithPath, ExifLoadOptions Options = 0)
        {
            if (FileNameWithPath != null)
            {
                _FileNameWithPath = Path.GetFullPath(FileNameWithPath);
            }
            if (Options.HasFlag(ExifLoadOptions.CreateEmptyBlock))
            {
                CreateEmptyExifBlock();
            }
            else
            {
                using (FileStream ImageFile = File.OpenRead(_FileNameWithPath))
                {
                    ReadFromStream(ImageFile);
                }
            }
        }


        public ExifData(Stream ImageStream)
        {
            ReadFromStream(ImageStream);
        }


        // Save the EXIF data in a new file or overwrite the current file.
        //
        // Parameters:
        //  "NewFileNameWithPath": New file name for saving the image and the EXIF data. If this parameter is "null",
        //                         the EXIF data is replaced in the current file.
        public void Save(string DestFileNameWithPath = null, ExifSaveOptions SaveOptions = 0)
        {
            bool DestFileIsTempFile = false;
            string SourceFileNameWithPath = _FileNameWithPath;
            if (DestFileNameWithPath != null)
            {
                DestFileNameWithPath = Path.GetFullPath(DestFileNameWithPath);
            }
            if ((DestFileNameWithPath == null) ||
                (String.Compare(SourceFileNameWithPath, DestFileNameWithPath, StringComparison.OrdinalIgnoreCase) == 0))
            {
                int BackslashPosition = SourceFileNameWithPath.LastIndexOf('\\');
                int DotPosition = SourceFileNameWithPath.LastIndexOf('.');
                if (DotPosition <= BackslashPosition) DotPosition = SourceFileNameWithPath.Length;
                DestFileNameWithPath = SourceFileNameWithPath.Insert(DotPosition, "~");
                DestFileIsTempFile = true;
            }

            bool DestFileCreated = false;
            FileStream SourceFile = null, DestFile = null;
            try
            {
                SourceFile = File.OpenRead(SourceFileNameWithPath);
                DestFile = File.Open(DestFileNameWithPath, FileMode.Create);
                DestFileCreated = true;
                Save(SourceFile, DestFile, SaveOptions);
                SourceFile.Dispose();
                SourceFile = null;
                DestFile.Dispose();
                DestFile = null;
                if (DestFileIsTempFile)
                {
                    File.Delete(SourceFileNameWithPath); // Delete original image file. If the original file cannot be deleted, delete
                                                         // the temporary file within the "catch" block.
                    File.Move(DestFileNameWithPath, SourceFileNameWithPath);
                }
            }
            catch
            {
                if (SourceFile != null)
                {
                    SourceFile.Dispose();
                }
                if (DestFile != null)
                {
                    DestFile.Dispose();
                }
                if (DestFileCreated)
                {
                    File.Delete(DestFileNameWithPath);
                }
                throw;
            }
        }


        public void Save(Stream SourceStream, Stream DestStream, ExifSaveOptions SaveOptions = 0)
        {
            ushort BlockSize;
            int k;
            long NextBlockStartPosition, FirstBlockStartPosition;

            byte[] TempData = new byte[65536];
            ReadStreamUInt16BE(SourceStream, out ushort BlockMarker);
            CheckIfStreamIsSeekable(SourceStream, -2);
            if (BlockMarker == JpegSoiMarker)
            {
                WriteFileUint16BE(DestStream, JpegSoiMarker);
                FirstBlockStartPosition = SourceStream.Position;

                // Copy all APP0 blocks of the source file
                do
                {
                    ReadStreamUInt16BE(SourceStream, out BlockMarker);
                    if (BlockMarker == JpegSosMarker)
                    {
                        break; // Start of JPEG image matrix reached
                    }
                    ReadStreamUInt16BE(SourceStream, out BlockSize);
                    int ContentSize = BlockSize - 2;
                    if (ContentSize >= 0)
                    {
                        NextBlockStartPosition = SourceStream.Position + ContentSize;
                        if (BlockMarker == JpegApp0Marker)
                        {
                            WriteFileUint16BE(DestStream, BlockMarker);
                            WriteFileUint16BE(DestStream, BlockSize);
                            if (SourceStream.Read(TempData, 0, ContentSize) == ContentSize)
                            {
                                DestStream.Write(TempData, 0, ContentSize);
                            }
                            else throw new ExifException(ExifErrCode.UnexpectedEndOfFile);
                        }
                    }
                    else throw new ExifException(ExifErrCode.FileStructureIsWrong);
                    SourceStream.Position = NextBlockStartPosition;
                } while (true);

                // Write new EXIF block if the new EXIF block is not empty
                CreateNewExifBlock(out byte[] NewExifBlock, out int NewExifBlockLen, _ByteOrder);
                if (NewExifBlockLen <= MaxExifBlockLen)
                {
                    if (NewExifBlockLen > 0)
                    {
                        WriteFileUint16BE(DestStream, JpegApp1Marker);
                        WriteFileUint16BE(DestStream, (ushort)(NewExifBlockLen + 2 + 6));
                        DestStream.Write(ExifBlockStr, 0, ExifBlockStr.Length);
                        DestStream.Write(NewExifBlock, 0, NewExifBlockLen);
                    }
                }
                else throw new ExifException(ExifErrCode.ExifDataAreTooLarge);

                // Copy all remaining blocks of the source file
                SourceStream.Position = FirstBlockStartPosition;
                do
                {
                    ReadStreamUInt16BE(SourceStream, out BlockMarker);
                    if (BlockMarker == JpegSosMarker)
                    {
                        // Start of JPEG image matrix reached. Copy all remaining data from source stream to destination stream
                        // without further interpretation.
                        WriteFileUint16BE(DestStream, BlockMarker);
                        do
                        {
                            k = SourceStream.Read(TempData, 0, TempData.Length);
                            DestStream.Write(TempData, 0, k);
                        } while (k == TempData.Length);
                        break; // Destination stream was completely written
                    }

                    ReadStreamUInt16BE(SourceStream, out BlockSize);
                    int ContentSize = BlockSize - 2;
                    if (ContentSize >= 0)
                    {
                        if (SourceStream.Read(TempData, 0, ContentSize) == ContentSize)
                        {
                            bool CopyBlockFromSourceFile = true;
                            if (BlockMarker == JpegApp0Marker)
                            {
                                CopyBlockFromSourceFile = false; // All APP0 blocks have already been copied
                            }
                            else if (BlockMarker == JpegApp1Marker)
                            {
                                if ((ContentSize >= ExifBlockStr.Length) && CompareArrays(TempData, 0, ExifBlockStr))
                                {
                                    CopyBlockFromSourceFile = false; // EXIF has already been written
                                }
                                else if (SaveOptions.HasFlag(ExifSaveOptions.RemoveAdobeInfoBlock) &&
                                         (ContentSize >= AdobeInfoBlockStr.Length) && CompareArrays(TempData, 0, AdobeInfoBlockStr))
                                {
                                    CopyBlockFromSourceFile = false;
                                }
                            }
                            else if (BlockMarker == JpegApp2Marker)
                            {
                                if (SaveOptions.HasFlag(ExifSaveOptions.RemoveMpfBlock) &&
                                    (ContentSize >= MpfBlockStr.Length) && CompareArrays(TempData, 0, MpfBlockStr))
                                {
                                    CopyBlockFromSourceFile = false;
                                }
                            }
                            else if (BlockMarker == JpegApp13Marker)
                            {
                                if (SaveOptions.HasFlag(ExifSaveOptions.RemoveIptcIimBlock) &&
                                    (ContentSize >= IptcIimBlockStr.Length) && CompareArrays(TempData, 0, IptcIimBlockStr))
                                {
                                    CopyBlockFromSourceFile = false;
                                }
                            }
                            else if ((BlockMarker == JpegApp14Marker) && SaveOptions.HasFlag(ExifSaveOptions.RemoveJpegCopyrightBlock))
                            {
                                CopyBlockFromSourceFile = false;
                            }
                            else if ((BlockMarker == JpegCommentMarker) && SaveOptions.HasFlag(ExifSaveOptions.RemoveJpegCommentBlock))
                            {
                                CopyBlockFromSourceFile = false;
                            }

                            if (CopyBlockFromSourceFile)
                            {
                                WriteFileUint16BE(DestStream, BlockMarker);
                                WriteFileUint16BE(DestStream, BlockSize);
                                DestStream.Write(TempData, 0, ContentSize);
                            }
                        }
                        else throw new ExifException(ExifErrCode.UnexpectedEndOfFile);
                    }
                    else throw new ExifException(ExifErrCode.FileStructureIsWrong);
                } while (true);
            }
            else throw new ExifException(ExifErrCode.FileTypeIsNotSupported);
        }


        // Read a string from a tag.
        //
        // Parameters:
        //  "TagSpec":    Tag specification, consisting of the IFD in which the tag is stored and the tag ID.
        //  "Value":      Return: Tag data as a string.
        //                In case of an error "null" is given back.
        //  "Coding":     Tag type and code page in which the string is coded.
        //  Return value: true  = The tag was successfully read.
        //                false = The tag was not found or is of wrong type.
        public bool GetTagValue(ExifTag TagSpec, out string Value, StrCoding Coding)
        {
            TagItem t;
            bool Success = false;
            int i, j;

            StrCodingFormat StringTagFormat = (StrCodingFormat)((uint)Coding & 0xFFFF0000);
            ushort CodePage = (ushort)Coding;
            if (StringTagFormat == StrCodingFormat.TypeUndefinedWithIdCode)
            {
                Success = GetTagValueWithIdCode(TagSpec, out Value, CodePage);
            }
            else
            {
                Value = null;
                if (GetTagItem(TagSpec, out t))
                {
                    i = t.ValueCount;
                    j = t.ValueIndex;

                    if ((CodePage == 1200) || (CodePage == 1201)) // UTF16LE or UTF16BE
                    {
                        // Remove all null terminating characters. Here a null terminating character consists of 2 zero-bytes.
                        while ((i >= 2) && (t.ValueData[j + i - 2] == 0) && (t.ValueData[j + i - 1] == 0))
                        {
                            i -= 2;
                        }
                    }
                    else
                    {
                        // Remove all null terminating characters.
                        while ((i >= 1) && (t.ValueData[j + i - 1] == 0))
                        {
                            i--;
                        }
                    }
                    if (CodePage != 0)
                    {
                        if (((StringTagFormat == StrCodingFormat.TypeAscii) && (t.TagType == ExifTagType.Ascii)) ||
                            ((StringTagFormat == StrCodingFormat.TypeUndefined) && (t.TagType == ExifTagType.Undefined)) ||
                            ((StringTagFormat == StrCodingFormat.TypeByte) && (t.TagType == ExifTagType.Byte)))
                        {
                            Value = Encoding.GetEncoding(CodePage).GetString(t.ValueData, j, i);
                            Success = true;
                        }
                    }
                }
            }
            return (Success);
        }


        // Write a string to a tag.
        //
        // Parameters:
        //  "TagSpec":    Tag specification, consisting of the IFD in which the tag is stored and the tag ID.
        //  "Value":      String to be written.
        //  "Coding":     Tag type and code page in which the string should be coded.
        //  Return value: true  = The tag was successfully written.
        //                false = Error. Tag ID is not allowed to be written or IFD is invalid.
        public bool SetTagValue(ExifTag TagSpec, string Value, StrCoding Coding)
        {
            int TotalByteCount, StrByteLen, NullTermBytes;
            TagItem t;
            bool Success = false;
            byte[] StringAsByteArray;
            ExifTagType TagType;

            StrCodingFormat StringTagFormat = (StrCodingFormat)((uint)Coding & 0xFFFF0000);
            ushort CodePage = (ushort)Coding;
            if (StringTagFormat == StrCodingFormat.TypeUndefinedWithIdCode)
            {
                Success = SetTagValueWithIdCode(TagSpec, Value, CodePage);
            }
            else
            {
                if (StringTagFormat == StrCodingFormat.TypeUndefined)
                {
                    TagType = ExifTagType.Undefined;
                }
                else if (StringTagFormat == StrCodingFormat.TypeByte)
                {
                    TagType = ExifTagType.Byte;
                }
                else
                {
                    TagType = ExifTagType.Ascii;
                }

                NullTermBytes = 0;
                if (TagType != ExifTagType.Undefined)
                {
                    if ((CodePage == 1200) || (CodePage == 1201)) // UTF16LE or UTF16BE
                    {
                        NullTermBytes = 2; // Write null terminating character with 2 zero-bytes
                    }
                    else
                    {
                        NullTermBytes = 1; // Write null terminating character with 1 zero-byte
                    }
                }

                if (CodePage != 0)
                {
                    StringAsByteArray = Encoding.GetEncoding(CodePage).GetBytes(Value);
                    StrByteLen = StringAsByteArray.Length;
                    TotalByteCount = StrByteLen + NullTermBytes;
                    t = PrepareTagForCompleteWriting(TagSpec, TagType, TotalByteCount);
                    if (t != null)
                    {
                        Array.Copy(StringAsByteArray, 0, t.ValueData, t.ValueIndex, StrByteLen);
                        if (NullTermBytes >= 1) t.ValueData[t.ValueIndex + StrByteLen] = 0;
                        if (NullTermBytes >= 2) t.ValueData[t.ValueIndex + StrByteLen + 1] = 0;
                        Success = true;
                    }
                }
            }
            return (Success);
        }


        // Read a 32 bit signed integer number from a tag. The tag type must be one of the following values:
        //  ExifTagType.Byte, ExifTagType.UShort, ExifTagType.ULong, ExifTagType.SLong.
        //
        // Parameters:
        //  "TagSpec":    Tag specification, consisting of the IFD in which the tag is stored and the tag ID.
        //  "Value":      Return: 32 bit integer number with sign. If the tag type is "ExifTagType.ULong" and
        //                the tag contains a number from 0x80000000 to 0xFFFFFFFF, it is treated as an error.
        //                In case of an error, "Value" is 0.
        //  "Index":      Array index of the value to be read.
        //  Return value: true  = The tag was successfully read.
        //                false = The tag was not found, is of wrong type or is out of range.
        public bool GetTagValue(ExifTag TagSpec, out int Value, int Index = 0)
        {
            TagItem t;
            bool Success = false;
            uint TempValue = 0;

            if (GetTagItem(TagSpec, out t) && ReadUintElement(t, Index, out TempValue))
            {
                Success = ((t.TagType != ExifTagType.ULong) || ((int)TempValue >= 0));
                if (!Success) TempValue = 0;
            }
            Value = (int)TempValue;
            return (Success);
        }


        // Write a 32 bit signed integer number to a tag.
        //
        // Parameters:
        //  "TagSpec":    Tag specification, consisting of the IFD in which the tag is stored and the tag ID.
        //  "TagType":    Tag type: ExifTagType.Byte, ExifTagType.UShort, ExifTagType.ULong or
        //                ExifTagType.SLong
        //  "Value":      32 bit integer number with sign.
        //  "Index":      Array index of the value to be written.
        //  Return value: true  = The tag was successfully written.
        //                false = The tag specification is invalid or "Value" is out of range.
        public bool SetTagValue(ExifTag TagSpec, int Value, ExifTagType TagType, int Index = 0)
        {
            TagItem t;
            bool Success = false;

            switch (TagType)
            {
                case ExifTagType.Byte:
                    if ((Value >= 0) && (Value <= 255)) Success = true;
                    break;
                case ExifTagType.UShort:
                    if ((Value >= 0) && (Value <= 65535)) Success = true;
                    break;
                case ExifTagType.ULong:
                    if (Value >= 0) Success = true;
                    break;
                case ExifTagType.SLong:
                    Success = true;
                    break;
            }
            if (Success)
            {
                t = PrepareTagForArrayItemWriting(TagSpec, TagType, Index);
                Success = WriteUintElement(t, Index, (uint)Value);
            }
            return (Success);
        }


        public bool GetTagValue(ExifTag TagSpec, out uint Value, int Index = 0)
        {
            TagItem t;
            bool Success = false;
            uint TempValue = 0;

            if (GetTagItem(TagSpec, out t) && ReadUintElement(t, Index, out TempValue))
            {
                Success = ((t.TagType != ExifTagType.SLong) || ((int)TempValue >= 0));
                if (!Success) TempValue = 0;
            }
            Value = TempValue;
            return (Success);
        }


        public bool SetTagValue(ExifTag TagSpec, uint Value, ExifTagType TagType, int Index = 0)
        {
            TagItem t;
            bool Success = false;

            switch (TagType)
            {
                case ExifTagType.Byte:
                    if ((Value >= 0) && (Value <= 255)) Success = true;
                    break;
                case ExifTagType.UShort:
                    if ((Value >= 0) && (Value <= 65535)) Success = true;
                    break;
                case ExifTagType.ULong:
                    Success = true;
                    break;
                case ExifTagType.SLong:
                    if ((int)Value >= 0) Success = true;
                    break;
            }
            if (Success)
            {
                t = PrepareTagForArrayItemWriting(TagSpec, TagType, Index);
                Success = WriteUintElement(t, Index, (uint)Value);
            }
            return (Success);
        }


        public bool GetTagValue(ExifTag TagSpec, out ExifRational Value, int Index = 0)
        {
            TagItem t;
            bool Success = false;
            uint Numer, Denom;

            if (GetTagItem(TagSpec, out t) && ReadURatElement(t, Index, out Numer, out Denom) == true)
            {
                if (t.TagType == ExifTagType.URational)
                {
                    Value = new ExifRational(Numer, Denom);
                }
                else // ExifTagType.SRational
                {
                    Value = new ExifRational((int)Numer, (int)Denom);
                }
                Success = true;
            }
            else
            {
                Value = new ExifRational(0, 0);
            }
            return (Success);
        }


        public bool SetTagValue(ExifTag TagSpec, ExifRational Value, ExifTagType TagType, int Index = 0)
        {
            TagItem t;
            bool Success = false;

            switch (TagType)
            {
                case ExifTagType.SRational:
                    if ((Value.Numer < 0x80000000) && (Value.Denom < 0x80000000))
                    {
                        if (Value.Sign)
                        {
                            Value.Numer = (uint)(-(int)Value.Numer);
                        }
                        Success = true;
                    }
                    else Success = false;
                    break;
                case ExifTagType.URational:
                    Success = !Value.IsNegative();
                    break;
            }
            if (Success)
            {
                t = PrepareTagForArrayItemWriting(TagSpec, TagType, Index);
                Success = WriteURatElement(t, Index, Value.Numer, Value.Denom);
            }
            return (Success);
        }


        // Read time stamp from a tag. The tag type must be "ExifTagType.Ascii" and the tag
        // must contain a null terminated string with 19 characters in the 
        // format "yyyy:MM:dd HH:mm:ss" or with 10 characters in the format "yyyy:MM:dd".
        //
        // Parameters:
        //  "TagSpec":    Tag specification, consisting of the IFD in which the tag is stored and the tag ID.
        //  "Value":      Return: Date time stamp. In case of an error, the value "DateTime.MinValue" is returned.
        //  "Format"      Selection between time stamp format with 19 or 10 characters.
        //  Return value: true  = The tag was successfully read.
        //                false = The tag was not found, is of wrong type or wrong format.
        public bool GetTagValue(ExifTag TagSpec, out DateTime Value, ExifDateFormat Format = ExifDateFormat.DateAndTime)
        {
            TagItem t;
            bool Success;
            int i, Year, Month, Day, Hour, Minute, Second;

            Success = false;
            Value = DateTime.MinValue;
            if (GetTagItem(TagSpec, out t) && (t.TagType == ExifTagType.Ascii))
            {
                try
                {
                    i = t.ValueIndex;
                    if ((t.ValueCount >= 10) && (t.ValueData[i + 4] == (byte)':') && (t.ValueData[i + 7] == (byte)':'))
                    {
                        Year = CalculateTwoDigitDecNumber(t.ValueData, i) * 100 + CalculateTwoDigitDecNumber(t.ValueData, i + 2);
                        Month = CalculateTwoDigitDecNumber(t.ValueData, i + 5);
                        Day = CalculateTwoDigitDecNumber(t.ValueData, i + 8);
                        if ((Format == ExifDateFormat.DateAndTime) && (t.ValueCount == 19 + 1) && (t.ValueData[i + 10] == (byte)' ') &&
                            (t.ValueData[i + 13] == (byte)':') && (t.ValueData[i + 16] == (byte)':') && (t.ValueData[i + 19] == 0))
                        {
                            Hour = CalculateTwoDigitDecNumber(t.ValueData, i + 11);
                            Minute = CalculateTwoDigitDecNumber(t.ValueData, i + 14);
                            Second = CalculateTwoDigitDecNumber(t.ValueData, i + 17);
                            Value = new DateTime(Year, Month, Day, Hour, Minute, Second);
                            Success = true;
                        }
                        else if ((Format == ExifDateFormat.DateOnly) && (t.ValueCount == 10 + 1) && (t.ValueData[i + 10] == 0))
                        {
                            Value = new DateTime(Year, Month, Day);
                            Success = true;
                        }
                    }
                }
                catch
                {
                    // Invalid time stamp
                }
            }
            return (Success);
        }


        // Write a time stamp to a tag. The tag type is set to "ExifTagType.Ascii" and the time stammp
        // is written in the format "yyyy:MM:dd HH:mm:ss" or "yyyy:MM:dd".
        //
        // Parameters:
        //  "TagSpec":    Tag specification, consisting of the IFD in which the tag is stored and the tag ID.
        //  "Value":      Date time stamp.
        //  "Format"      Selection between time stamp format with 19 or 10 characters.
        //  Return value: true  = The tag was successfully written.
        //                false = Error.
        public bool SetTagValue(ExifTag TagSpec, DateTime Value, ExifDateFormat Format = ExifDateFormat.DateAndTime)
        {
            TagItem t;
            bool Success = false;
            int i, sByteCount = 0, Year;

            if (Format == ExifDateFormat.DateAndTime)
            {
                sByteCount = 19 + 1; // "+ 1" because a null character has to be written
            }
            else if (Format == ExifDateFormat.DateOnly)
            {
                sByteCount = 10 + 1;
            }
            if (sByteCount != 0)
            {
                t = PrepareTagForCompleteWriting(TagSpec, ExifTagType.Ascii, sByteCount);
                if (t != null)
                {
                    i = t.ValueIndex;
                    Year = Value.Year;
                    ConvertTwoDigitNumberToByteArr(t.ValueData, ref i, Year / 100);
                    ConvertTwoDigitNumberToByteArr(t.ValueData, ref i, Year % 100);
                    t.ValueData[i] = (byte)':'; i++;
                    ConvertTwoDigitNumberToByteArr(t.ValueData, ref i, Value.Month);
                    t.ValueData[i] = (byte)':'; i++;
                    ConvertTwoDigitNumberToByteArr(t.ValueData, ref i, Value.Day);
                    if (Format == ExifDateFormat.DateAndTime)
                    {
                        t.ValueData[i] = (byte)' '; i++;
                        ConvertTwoDigitNumberToByteArr(t.ValueData, ref i, Value.Hour);
                        t.ValueData[i] = (byte)':'; i++;
                        ConvertTwoDigitNumberToByteArr(t.ValueData, ref i, Value.Minute);
                        t.ValueData[i] = (byte)':'; i++;
                        ConvertTwoDigitNumberToByteArr(t.ValueData, ref i, Value.Second);
                    }
                    t.ValueData[i] = 0; i++;
                    Success = true;
                }
            }
            return (Success);
        }


        // Read the tag data as raw data without copying the raw data.
        //
        // Parameters:
        //  "TagSpec":          Tag specification, consisting of the IFD in which the tag is stored and the tag ID.
        //  "TagType":          Return: Type of the tag.
        //  "ValueCount":       Return: Number of values which are stored in the tag.
        //  "RawData" :         Return: Array with the tag data, but there may be also other data stored in this array.
        //                      There are the following restrictions:
        //                      *The data returned in the array "RawData" must not be changed by the caller of this method.
        //                       "RawData" should only be used for reading data!
        //                      *After new data have been written into the specified tag, the array "RawData" must not be used any
        //                       more. Therefore the data returned in "RawData" should be copied as soon as possible.
        //                      *If the tag does not exist, "RawData" is null.
        //  "RawDataIndex" :    Return: Array index at which the data starts. The first data byte is stored in 
        //                      "RawData[RawDataIndex]" and the last data byte is stored in 
        //                      "RawData[RawDataIndex + GetTagByteCount(TagType, ValueCount) - 1]".
        //  Return value:       true  = The tag was successfully read.
        //                      false = The tag was not found.
        public bool GetTagRawData(ExifTag TagSpec, out ExifTagType TagType, out int ValueCount, out byte[] RawData,
          out int RawDataIndex)
        {
            bool Success = false;

            if (GetTagItem(TagSpec, out TagItem t))
            {
                TagType = t.TagType;
                ValueCount = t.ValueCount;
                RawData = t.ValueData;
                RawDataIndex = t.ValueIndex;
                Success = true;
            }
            else
            {
                TagType = 0;
                ValueCount = 0;
                RawData = null;
                RawDataIndex = 0;
            }
            return (Success);
        }


        // Read the tag data as raw data with copying the raw data.
        //
        // Parameters:
        //  "TagSpec":          Tag specification, consisting of the IFD in which the tag is stored and the tag ID.
        //  "TagType":          Return: Type of the tag.
        //  "ValueCount":       Return: Number of values which are stored in the tag.
        //  "RawData" :         Return: Array with the tag data. If the tag does not exist, "RawData" is null.
        //  Return value:       true  = The tag was successfully read.
        //                      false = The tag was not found.
        public bool GetTagRawData(ExifTag TagSpec, out ExifTagType TagType, out int ValueCount, out byte[] RawData)
        {
            bool Success = false;

            if (GetTagItem(TagSpec, out TagItem t))
            {
                TagType = t.TagType;
                ValueCount = t.ValueCount;
                int k = t.ByteCount;
                RawData = new byte[k];
                Array.Copy(t.ValueData, t.ValueIndex, RawData, 0, k);
                Success = true;
            }
            else
            {
                TagType = 0;
                ValueCount = 0;
                RawData = null;
            }
            return (Success);
        }


        // Write the tag data as raw data.
        //
        // Parameters:
        //  "TagSpec":      Tag specification, consisting of the IFD in which the tag is stored and the tag ID.
        //  "TagType":      Type of the tag.
        //  "ValueCount":   Number of values which are stored in the tag. This is not the number of raw data bytes!
        //                  The number of raw data bytes is automatically calculated by the parameters "TagType" and
        //                  "ValueCount".
        //  "RawData":      Array with the tag data. The tag data is not copied by this method, therefore
        //                  the array content must not be changed after the call of this method.
        //  "RawDataIndex": Array index at which the data starts. Set this parameter to 0, if the data starts with
        //                  the first element of the array "RawData".
        //  Return value:   true  = The tag was successfully set.
        //                  false = The IFD is invalid.
        public bool SetTagRawData(ExifTag TagSpec, ExifTagType TagType, int ValueCount, byte[] RawData, int RawDataIndex = 0)
        {
            TagItem t;
            bool Success = false;

            ExifIfd Ifd = ExtractIfd(TagSpec);
            if ((uint)Ifd < ExifIfdCount)
            {
                int RawDataByteCount = GetTagByteCount(TagType, ValueCount);
                Dictionary<ExifTagId, TagItem> IfdTagTable = TagTable[(uint)Ifd];
                if (IfdTagTable.TryGetValue(ExtractTagId(TagSpec), out t))
                {
                    t.TagType = TagType;
                    t.ValueCount = ValueCount;
                    t.ValueData = RawData;
                    t.ValueIndex = RawDataIndex;
                    t.AllocatedByteCount = RawDataByteCount;
                }
                else
                {
                    t = new TagItem(ExtractTagId(TagSpec), TagType, ValueCount, RawData, RawDataIndex, RawDataByteCount);
                    IfdTagTable.Add(t.TagId, t);
                }
                Success = true;
            }
            return (Success);
        }


        public bool GetTagValueCount(ExifTag TagSpec, out int ValueCount)
        {
            if (GetTagItem(TagSpec, out TagItem t))
            {
                ValueCount = t.ValueCount;
                return (true);
            }
            else
            {
                ValueCount = 0;
                return (false);
            }
        }


        public bool SetTagValueCount(ExifTag TagSpec, int ValueCount)
        {
            if (GetTagItem(TagSpec, out TagItem t) && ((uint)ValueCount <= 65535))
            {
                t.SetTagTypeAndValueCount(t.TagType, ValueCount, true);
                return (true);
            }
            else return (false);
        }


        public bool SetTagValueCount(ExifTag TagSpec, int ValueCount, ExifTagType TagType)
        {
            TagItem t = PrepareTagForArrayItemWriting(TagSpec, TagType, ValueCount - 1);
            return (t != null);
        }


        public bool GetTagType(ExifTag TagSpec, out ExifTagType TagType)
        {
            if (GetTagItem(TagSpec, out TagItem t))
            {
                TagType = t.TagType;
                return (true);
            }
            else
            {
                TagType = 0;
                return (false);
            }
        }


        public bool TagExists(ExifTag TagSpec)
        {
            ExifIfd Ifd = ExtractIfd(TagSpec);
            if ((uint)Ifd < ExifIfdCount)
            {
                Dictionary<ExifTagId, TagItem> IfdTagTable = TagTable[(uint)Ifd];
                return (IfdTagTable.ContainsKey(ExtractTagId(TagSpec)));
            }
            else return (false);
        }


        public bool IfdExists(ExifIfd Ifd)
        {
            if ((uint)Ifd < ExifIfdCount)
            {
                Dictionary<ExifTagId, TagItem> IfdTagTable = TagTable[(uint)Ifd];
                return (IfdTagTable.Count > 0);
            }
            else return (false);
        }


        // Remove a single tag.
        //
        // Parameters:
        //  "TagSpec":      Tag specification, consisting of the IFD in which the tag is stored and the tag ID.
        //  Return value:   false = Tag was not removed, because it doesn't exist.
        //                  true  = Tag was removed.
        public bool RemoveTag(ExifTag TagSpec)
        {
            ExifIfd Ifd = ExtractIfd(TagSpec);
            if ((uint)Ifd < ExifIfdCount)
            {
                Dictionary<ExifTagId, TagItem> IfdTagTable = TagTable[(uint)Ifd];
                return (IfdTagTable.Remove(ExtractTagId(TagSpec)));
            }
            else return (false);
        }


        // Remove all tags from a specific IFD.
        //
        // Parameters:
        //  "ExifIfd": IFD from which all tags are to be removed. If the IFD "ThumbnailData" is removed, the thumbnail image
        //             is removed, too.
        public bool RemoveAllTagsFromIfd(ExifIfd Ifd)
        {
            bool Removed = false;

            if ((uint)Ifd < ExifIfdCount)
            {
                ClearIfd_Unchecked(Ifd);
                if (Ifd == ExifIfd.ThumbnailData)
                {
                    RemoveThumbnailImage(false);
                }
                Removed = true;
            }
            return (Removed);
        }


        // Remove all tags from the EXIF block and remove the thumbnail image.
        public void RemoveAllTags()
        {
            for (ExifIfd Ifd = 0; Ifd < (ExifIfd)ExifIfdCount; Ifd++)
            {
                ClearIfd_Unchecked(Ifd);
            }
            RemoveThumbnailImage(false);
            MakerNoteOriginalIndex = -1;
        }


        // Replace all EXIF tags and the thumbnail image by the EXIF data of another image file.
        //
        // Parameters:
        //  "NewExifData": EXIF data of another image, that will be copied to the EXIF data of the current object.
        //                 A deep copy is done.
        public void ReplaceAllTagsBy(ExifData NewExifData)
        {
            // Copy byte order value
            this._ByteOrder = NewExifData._ByteOrder;

            // Copy tag table
            for (ExifIfd Ifd = 0; Ifd < (ExifIfd)ExifIfdCount; Ifd++)
            {
                Dictionary<ExifTagId, TagItem> SourceIfdTagList = NewExifData.TagTable[(uint)Ifd];
                Dictionary<ExifTagId, TagItem> DestIfdTagList = this.TagTable[(uint)Ifd];
                DestIfdTagList.Clear();
                foreach (TagItem SourceTag in SourceIfdTagList.Values)
                {
                    TagItem DestTag = new TagItem(SourceTag.TagId, SourceTag.TagType, SourceTag.ValueCount);
                    Array.Copy(SourceTag.ValueData, SourceTag.ValueIndex, DestTag.ValueData, 0, DestTag.ByteCount);
                    DestIfdTagList.Add(DestTag.TagId, DestTag);
                }
            }
            this.ExifBlock = null; // The EXIF block of the image file is no longer needed

            // Copy thumbnail image
            if (NewExifData.ThumbnailImageExists())
            {
                this.ThumbnailStartIndex = 0;
                this.ThumbnailByteCount = NewExifData.ThumbnailByteCount;
                this.ThumbnailImage = new byte[this.ThumbnailByteCount];
                Array.Copy(NewExifData.ThumbnailImage, NewExifData.ThumbnailStartIndex, this.ThumbnailImage, 0, this.ThumbnailByteCount);
            }
            else
            {
                RemoveThumbnailImage_Internal();
            }

            // Copy MakerNote index
            MakerNoteOriginalIndex = NewExifData.MakerNoteOriginalIndex;
        }


        public bool IsExifBlockEmpty()
        {
            ExifIfd Ifd = 0;
            bool IsEmpty = true;
            do
            {
                if (TagTable[(uint)Ifd].Count != 0)
                {
                    IsEmpty = false;
                    break;
                }
                Ifd++;
            } while (Ifd < (ExifIfd)ExifIfdCount);
            if (ThumbnailImageExists())
            {
                IsEmpty = false;
            }
            return (IsEmpty);
        }


        public bool ThumbnailImageExists()
        {
            return (ThumbnailImage != null);
        }


        // Read the thumbnail image data.
        //
        // Parameters:
        //  "ThumbnailData":      Return: Array with the thumbnail image beginning with the JPEG marker 0xFF 0xD8.
        //                        If no thumbnail image is defined, "null" is returned.
        //  "ThumbnailIndex":     Return: Index of the first data byte of the thumbnail image.
        //  "ThumbnailByteCount": Return: Number of bytes of the thumbnail image.
        //  Return value:         false = Thumbnail image does not exist.
        //                        true  = Thumbnail image is given back.
        public bool GetThumbnailImage(out byte[] ThumbnailData, out int ThumbnailIndex, out int ThumbnailByteCount)
        {
            bool Success;

            ThumbnailData = ThumbnailImage;
            ThumbnailIndex = ThumbnailStartIndex;
            ThumbnailByteCount = this.ThumbnailByteCount;
            Success = ThumbnailImageExists();
            return (Success);
        }


        // Write the thumbnail image data.
        //
        // Parameters:
        //  "ThumbnailData": Array which contains the thumbnail image beginning with the JPEG marker 0xFF 0xD8.
        //                   The array is not copied by this method so it should not be changed after calling
        //                   this method.
        public bool SetThumbnailImage(byte[] ThumbnailData, int ThumbnailIndex = 0, int ThumbnailByteCount = -1)
        {
            bool Success = false;

            if (ThumbnailData != null)
            {
                ThumbnailImage = ThumbnailData;
                ThumbnailStartIndex = ThumbnailIndex;
                if (ThumbnailByteCount < 0)
                {
                    this.ThumbnailByteCount = ThumbnailData.Length - ThumbnailIndex;
                }
                else this.ThumbnailByteCount = ThumbnailByteCount;
                SetTagValue(ExifTag.JpegInterchangeFormat, 0, ExifTagType.ULong); // "0" is a dummy value
                SetTagValue(ExifTag.JpegInterchangeFormatLength, this.ThumbnailByteCount, ExifTagType.ULong);
                Success = true;
            }
            return (Success);
        }


        // Remove the thumbnail image data.
        //
        // Parameters:
        //  "RemoveAlsoThumbnailTags": false = Remove the thumbnail image, the thumbnail pointer tag and the thumbnail size tag.
        //                             true  = Remove also all other tags in the IFD "thumbnail data".
        public void RemoveThumbnailImage(bool RemoveAlsoThumbnailTags)
        {
            RemoveThumbnailImage_Internal();
            if (RemoveAlsoThumbnailTags)
            {
                ClearIfd_Unchecked(ExifIfd.ThumbnailData);
            }
            else
            {
                RemoveTag(ExifTag.JpegInterchangeFormat);
                RemoveTag(ExifTag.JpegInterchangeFormatLength);
            }
        }


        // Get the byte order for the EXIF data.
        public ExifByteOrder ByteOrder
        {
            get
            {
                return (_ByteOrder);
            }
        }


        // Extract the IFD of a tag specification.
        public static ExifIfd ExtractIfd(ExifTag TagSpec)
        {
            return (ExifIfd)((uint)TagSpec >> IfdShift);
        }


        // Extract the tag ID of a tag specification.
        public static ExifTagId ExtractTagId(ExifTag TagSpec)
        {
            return (ExifTagId)((ushort)TagSpec);
        }


        // Compose IFD and tag ID to a tag specification.
        public static ExifTag ComposeTagSpec(ExifIfd Ifd, ExifTagId TagId)
        {
            return (ExifTag)(((uint)Ifd << IfdShift) | (uint)TagId);
        }


        // Get the number of bytes that a tag with the specified type and value count has.
        // If the tag type is invalid, the return value is 0.
        public static int GetTagByteCount(ExifTagType TagType, int ValueCount)
        {
            if ((uint)TagType < TypeByteCountLen)
            {
                return (TypeByteCount[(uint)TagType] * ValueCount);
            }
            else return (0);
        }


        // Read 16 bit value from a byte array.
        public static ushort ReadUInt16(byte[] Data, int StartIndex, ExifByteOrder ByteOrder)
        {
            ushort v;

            if (ByteOrder == ExifByteOrder.BigEndian)
            {
                v = (ushort)(((uint)Data[StartIndex] << 8) | Data[StartIndex + 1]);
            }
            else
            {
                v = (ushort)(((uint)Data[StartIndex + 1] << 8) | Data[StartIndex]);
            }
            return (v);
        }


        // Write 16 bit value to a byte array.
        public static void WriteUInt16(byte[] Data, int StartIndex, ushort Value, ExifByteOrder ByteOrder)
        {
            if (ByteOrder == ExifByteOrder.BigEndian)
            {
                Data[StartIndex] = (byte)(Value >> 8);
                Data[StartIndex + 1] = (byte)Value;
            }
            else
            {
                Data[StartIndex + 1] = (byte)(Value >> 8);
                Data[StartIndex] = (byte)Value;
            }
        }


        // Read 32 bit value from a byte array.
        public static uint ReadUInt32(byte[] Data, int StartIndex, ExifByteOrder ByteOrder)
        {
            uint v;

            if (ByteOrder == ExifByteOrder.BigEndian)
            {
                v = ((uint)Data[StartIndex] << 24) |
                    ((uint)Data[StartIndex + 1] << 16) |
                    ((uint)Data[StartIndex + 2] << 8) |
                    (Data[StartIndex + 3]);
            }
            else
            {
                v = ((uint)Data[StartIndex + 3] << 24) |
                   ((uint)Data[StartIndex + 2] << 16) |
                   ((uint)Data[StartIndex + 1] << 8) |
                   (Data[StartIndex]);
            }
            return (v);
        }


        // Write 32 bit value to a byte array.
        public static void WriteUInt32(byte[] Data, int StartIndex, uint Value, ExifByteOrder ByteOrder)
        {
            if (ByteOrder == ExifByteOrder.BigEndian)
            {
                Data[StartIndex] = (byte)(Value >> 24);
                Data[StartIndex + 1] = (byte)(Value >> 16);
                Data[StartIndex + 2] = (byte)(Value >> 8);
                Data[StartIndex + 3] = (byte)(Value);
            }
            else
            {
                Data[StartIndex + 3] = (byte)(Value >> 24);
                Data[StartIndex + 2] = (byte)(Value >> 16);
                Data[StartIndex + 1] = (byte)(Value >> 8);
                Data[StartIndex] = (byte)(Value);
            }
        }


        // Initialize enumeration for all tags of a specific IFD.
        public bool InitTagEnumeration(ExifIfd Ifd)
        {
            if ((uint)Ifd < ExifIfdCount)
            {
                ExifIfdForTagEnumeration = Ifd;
                TagEnumerator = TagTable[(uint)Ifd].Keys.GetEnumerator();
                return (true);
            }
            else
            {
                TagEnumerator = new Dictionary<ExifTagId, TagItem>.KeyCollection.Enumerator();
                return (false);
            }
        }


        // Get the next tag of the IFD, that was defined by a previous call to the method "InitTagEnumeration".
        //
        // Return value: true  = Next tag was successfully read.
        //               false = There is no further tag available.
        public bool EnumerateNextTag(out ExifTag TagSpec)
        {
            bool Success = false;

            if (TagEnumerator.MoveNext())
            {
                ExifTagId TagId = TagEnumerator.Current;
                TagSpec = ComposeTagSpec(ExifIfdForTagEnumeration, TagId);
                Success = true;
            }
            else TagSpec = 0;
            return (Success);
        }

        // -----------------------------------------------------------------------------------------------------------------
        // Methods for accessing EXIF tags on high level
        // -----------------------------------------------------------------------------------------------------------------

        public bool GetDateTaken(out DateTime Value)
        {
            return (GetDateAndTimeWithMillisecHelper(out Value, ExifTag.DateTimeOriginal, ExifTag.SubsecTimeOriginal));
        }


        public bool SetDateTaken(DateTime Value)
        {
            return (SetDateAndTimeWithMillisecHelper(Value, ExifTag.DateTimeOriginal, ExifTag.SubsecTimeOriginal));
        }


        public void RemoveDateTaken()
        {
            RemoveTag(ExifTag.DateTimeOriginal);
            RemoveTag(ExifTag.SubsecTimeOriginal);
        }


        public bool GetDateDigitized(out DateTime Value)
        {
            return (GetDateAndTimeWithMillisecHelper(out Value, ExifTag.DateTimeDigitized, ExifTag.SubsecTimeDigitized));
        }


        public bool SetDateDigitized(DateTime Value)
        {
            return (SetDateAndTimeWithMillisecHelper(Value, ExifTag.DateTimeDigitized, ExifTag.SubsecTimeDigitized));
        }


        public void RemoveDateDigitized()
        {
            RemoveTag(ExifTag.DateTimeDigitized);
            RemoveTag(ExifTag.SubsecTimeDigitized);
        }


        public bool GetDateChanged(out DateTime Value)
        {
            return (GetDateAndTimeWithMillisecHelper(out Value, ExifTag.DateTime, ExifTag.SubsecTime));
        }


        public bool SetDateChanged(DateTime Value)
        {
            return (SetDateAndTimeWithMillisecHelper(Value, ExifTag.DateTime, ExifTag.SubsecTime));
        }


        public void RemoveDateChanged()
        {
            RemoveTag(ExifTag.DateTime);
            RemoveTag(ExifTag.SubsecTime);
        }


        public bool GetGpsLongitude(out GeoCoordinate Value)
        {
            return (GetGpsCoordinateHelper(out Value, ExifTag.GpsLongitude, ExifTag.GpsLongitudeRef, 'W', 'E'));
        }


        public bool SetGpsLongitude(GeoCoordinate Value)
        {
            return (SetGpsCoordinateHelper(Value, ExifTag.GpsLongitude, ExifTag.GpsLongitudeRef, 'W', 'E'));
        }


        public void RemoveGpsLongitude()
        {
            RemoveTag(ExifTag.GpsLongitude);
            RemoveTag(ExifTag.GpsLongitudeRef);
        }


        public bool GetGpsLatitude(out GeoCoordinate Value)
        {
            return (GetGpsCoordinateHelper(out Value, ExifTag.GpsLatitude, ExifTag.GpsLatitudeRef, 'N', 'S'));
        }


        public bool SetGpsLatitude(GeoCoordinate Value)
        {
            return (SetGpsCoordinateHelper(Value, ExifTag.GpsLatitude, ExifTag.GpsLatitudeRef, 'N', 'S'));
        }


        public void RemoveGpsLatitude()
        {
            RemoveTag(ExifTag.GpsLatitude);
            RemoveTag(ExifTag.GpsLatitudeRef);
        }


        public bool GetGpsAltitude(out decimal Value)
        {
            bool Success = false;
            ExifRational AltitudeRat;
            uint BelowSeaLevel;

            if (GetTagValue(ExifTag.GpsAltitude, out AltitudeRat) && AltitudeRat.IsValid())
            {
                Value = ExifRational.ToDecimal(AltitudeRat);
                if (GetTagValue(ExifTag.GpsAltitudeRef, out BelowSeaLevel) && (BelowSeaLevel == 1))
                {
                    Value = -Value;
                }
                Success = true;
            }
            else Value = 0;
            return (Success);
        }


        public bool SetGpsAltitude(decimal Value)
        {
            bool Success = false;
            ExifRational AltitudeRat = ExifRational.FromDecimal(Value);
            uint BelowSeaLevel = 0;
            if (AltitudeRat.IsNegative())
            {
                BelowSeaLevel = 1;
                AltitudeRat.Sign = false; // Remove negative sign from "AltitudeRat"
            }
            if (SetTagValue(ExifTag.GpsAltitude, AltitudeRat, ExifTagType.URational) &&
                SetTagValue(ExifTag.GpsAltitudeRef, BelowSeaLevel, ExifTagType.Byte))
            {
                Success = true;
            }
            return (Success);
        }


        public void RemoveGpsAltitude()
        {
            RemoveTag(ExifTag.GpsAltitude);
            RemoveTag(ExifTag.GpsAltitudeRef);
        }


        public bool GetGpsDateTimeStamp(out DateTime Value)
        {
            bool Success = false;
            ExifRational Hour, Min, Sec;

            if (GetTagValue(ExifTag.GpsDateStamp, out Value, ExifDateFormat.DateOnly))
            {
                if (GetTagValue(ExifTag.GpsTimeStamp, out Hour, 0) && !Hour.IsNegative() && Hour.IsValid() &&
                    GetTagValue(ExifTag.GpsTimeStamp, out Min, 1) && !Min.IsNegative() && Min.IsValid() &&
                    GetTagValue(ExifTag.GpsTimeStamp, out Sec, 2) && !Sec.IsNegative() && Sec.IsValid())
                {
                    Value = Value.AddHours(((double)Hour.Numer) / Hour.Denom);
                    Value = Value.AddMinutes(((double)Min.Numer) / Min.Denom);
                    double ms = Math.Truncate(((double)Sec.Numer * 1000) / Sec.Denom);
                    Value = Value.AddMilliseconds(ms);
                    Success = true;
                }
                else Value = DateTime.MinValue;
            }
            else Value = DateTime.MinValue;
            return (Success);
        }


        public bool SetGpsDateTimeStamp(DateTime Value)
        {
            bool Success = false;
            ExifRational Sec;

            if (SetTagValue(ExifTag.GpsDateStamp, Value.Date, ExifDateFormat.DateOnly))
            {
                TimeSpan ts = Value.TimeOfDay;
                ExifRational Hour = new ExifRational(ts.Hours, 1);
                ExifRational Min = new ExifRational(ts.Minutes, 1);
                int ms = ts.Milliseconds;
                if (ms == 0)
                {
                    Sec = new ExifRational(ts.Seconds, 1);
                }
                else
                {
                    Sec = new ExifRational(ts.Seconds * 1000 + ms, 1000);
                }
                if (SetTagValue(ExifTag.GpsTimeStamp, Hour, ExifTagType.URational, 0) &&
                    SetTagValue(ExifTag.GpsTimeStamp, Min, ExifTagType.URational, 1) &&
                    SetTagValue(ExifTag.GpsTimeStamp, Sec, ExifTagType.URational, 2))
                {
                    Success = true;
                }
            }
            return (Success);
        }


        public void RemoveGpsDateTimeStamp()
        {
            RemoveTag(ExifTag.GpsDateStamp);
            RemoveTag(ExifTag.GpsTimeStamp);
        }

        #endregion

        #region Private area
        // -----------------------------------------------------------------------------------------------------------------
        // --- Private area ------------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------------------------

        private const int MinExifBlockLen = 8 + 2 + 4; // TIFF header (8 bytes) + Number of tags of IFD Primary Data (2 bytes) + 
                                                       // Offset of IFD Thumbnail Data (4 bytes)
        private const int MaxExifBlockLen = 65534 - 2 - 6; // Maximum number of bytes, which an EXIF block in a JPEG file can theoretically have
        private const int MaxValueCount = int.MaxValue / 8;
        private const ushort JpegApp0Marker = 0xFFE0;
        private const ushort JpegApp1Marker = 0xFFE1;
        private const ushort JpegApp2Marker = 0xFFE2;
        private const ushort JpegApp13Marker = 0xFFED;
        private const ushort JpegApp14Marker = 0xFFEE; // Used for JPEG copyright block
        private const ushort JpegCommentMarker = 0xFFFE; // Start of JPEG comment block
        private const ushort JpegSoiMarker = 0xFFD8; // Start of image (SOI) marker
        private const ushort JpegSosMarker = 0xFFDA; // Start of scan (SOS) marker
        private const ushort FixExifValue = 0x002A;
        private static readonly byte[] IdCodeUtf16 = new byte[] { (byte)'U', (byte)'N', (byte)'I', (byte)'C', (byte)'O', (byte)'D', (byte)'E', 0 };
        private static readonly byte[] IdCodeAscii = new byte[] { (byte)'A', (byte)'S', (byte)'C', (byte)'I', (byte)'I', 0, 0, 0 };
        private static readonly byte[] IdCodeDefault = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        private const int IdCodeLength = 8;
        private byte[] ExifBlock; // Storage for the EXIF block, starting with the TIFF header, which starts with the byte order mark
        private int ExifBlockLen; // Number of bytes, which are actually used in the array "ExifBlock"
        private ExifByteOrder _ByteOrder; // Byte order value, extracted from the TIFF header
        private int OffsetIfdPrimaryData; // Offset of the IFD Primary Data relative to the start of the TIFF header. Normally
                                          // this is the number 8.
        private string _FileNameWithPath;
        private static readonly int[] TypeByteCount = {
      0, // Type 0 is invalid
      1, // ExifTagType.Byte
      1, // ExifTagType.Ascii, 8 bit character
      2, // ExifTagType.UShort
      4, // ExifTagType.ULong
      8, // ExifTagType.URational
      0, // Type 6 is invalid
      1, // ExifTagType.Undefined, 8 bit value
      0, // Type 8 is invalid
      4, // ExifTagType.SLong
      8  // ExifTagType.SRational
    };
        private const int TypeByteCountLen = 11; // Number of elements of the array "TypeByteCount"
        private static readonly byte[] ExifBlockStr = { (byte)'E', (byte)'x', (byte)'i', (byte)'f', 0, 0 };
        private static readonly byte[] AdobeInfoBlockStr = { (byte)'h', (byte)'t', (byte)'t', (byte)'p', (byte)':',
      (byte)'/', (byte)'/', (byte)'n', (byte)'s', (byte)'.', (byte)'a', (byte)'d', (byte)'o', (byte)'b',
      (byte)'e', (byte)'.', (byte)'c', (byte)'o', (byte)'m' };
        private static readonly byte[] IptcIimBlockStr = { (byte)'P', (byte)'h', (byte)'o', (byte)'t', (byte)'o',
      (byte)'s', (byte)'h', (byte)'o', (byte)'p' };
        private static readonly byte[] MpfBlockStr = { (byte)'M', (byte)'P', (byte)'F', 0 };

        private Dictionary<ExifTagId, TagItem>[] TagTable;
        private byte[] ThumbnailImage; // Array, which contains among other data the thumbnail image
        private int ThumbnailStartIndex, ThumbnailByteCount; // Range, in which the thumbnail image in the array "ThumbnailData" is stored

        private const int ExifIfdCount = 5; // Number of elements of the enum type "ExifIfd", i. e. the number of IFDs
        private ExifIfd ExifIfdForTagEnumeration;
        private Dictionary<ExifTagId, TagItem>.KeyCollection.Enumerator TagEnumerator;
        protected int MakerNoteOriginalIndex;


        private void ReadFromStream(Stream ImageStream)
        {
            byte[] TempData = new byte[16];
            ReadStreamUInt16BE(ImageStream, out ushort BlockMarker);
            CheckIfStreamIsSeekable(ImageStream, -2);
            if (BlockMarker == JpegSoiMarker)
            {
                do
                {
                    ReadStreamUInt16BE(ImageStream, out BlockMarker);
                    if (BlockMarker == JpegSosMarker)
                    {
                        CreateEmptyExifBlock();
                        break; // Start of the compressed image data reached
                    }
                    ReadStreamUInt16BE(ImageStream, out ushort BlockSize);
                    int ContentSize = BlockSize - 2;
                    long NextBlockStartPosition = ImageStream.Position + ContentSize;
                    if (ContentSize >= 0)
                    {
                        if (BlockMarker == JpegApp1Marker)
                        {
                            if (ContentSize >= 6)
                            {
                                int k = ImageStream.Read(TempData, 0, 6);
                                if ((k == 6) && CompareArrays(TempData, 0, ExifBlockStr))
                                {
                                    ReadExifBlock(ImageStream, ContentSize - 6);
                                    break;
                                }
                            }
                        }
                        ImageStream.Position = NextBlockStartPosition;
                    }
                    else throw new ExifException(ExifErrCode.FileStructureIsWrong);
                } while (true);
            }
            else throw new ExifException(ExifErrCode.FileTypeIsNotSupported);
        }


        private void CheckIfStreamIsSeekable(Stream StreamToBeChecked, long CheckOffset)
        {
            long i = StreamToBeChecked.Position;
            StreamToBeChecked.Position += CheckOffset;
            if (StreamToBeChecked.Position != (i + CheckOffset))
            {
                // The stream for reading the image data must support the property "Position"
                throw new NotSupportedException("Stream is not seekable!");
            }
            StreamToBeChecked.Position = i;
        }


        private void ReadExifBlock(Stream StreamObj, int RemainingContentSize)
        {
            int k;
            ushort FixValue;

            ExifBlockLen = RemainingContentSize;
            ExifBlock = new byte[ExifBlockLen];
            k = StreamObj.Read(ExifBlock, 0, ExifBlockLen);
            if (k != ExifBlockLen) throw new ExifException(ExifErrCode.UnexpectedEndOfFile);
            if (ExifBlockLen >= MinExifBlockLen)
            {
                // Evaluate byte order mark
                if ((ExifBlock[0] == (byte)'I') &&
                    (ExifBlock[1] == (byte)'I'))
                {
                    _ByteOrder = ExifByteOrder.LittleEndian;
                }
                else if ((ExifBlock[0] == (byte)'M') &&
                         (ExifBlock[1] == (byte)'M'))
                {
                    _ByteOrder = ExifByteOrder.BigEndian;
                }
                else throw new ExifException(ExifErrCode.ExifBlockHasIllegalContent);

                // Check fix value and IFD offset
                FixValue = ReadUInt16(ExifBlock, 2, _ByteOrder);
                OffsetIfdPrimaryData = (int)ReadUInt32(ExifBlock, 4, _ByteOrder);
                if ((FixValue == FixExifValue) && (OffsetIfdPrimaryData >= 8))
                {
                    InitTagTable();
                }
                else throw new ExifException(ExifErrCode.ExifBlockHasIllegalContent);
            }
            else throw new ExifException(ExifErrCode.ExifBlockHasIllegalContent);
        }


        private void CreateEmptyExifBlock()
        {
            // The TIFF header within the EXIF block will not be initialized because it is not used any more.
            _ByteOrder = ExifByteOrder.BigEndian;
            ExifBlockLen = MinExifBlockLen;
            ExifBlock = new byte[ExifBlockLen];
            OffsetIfdPrimaryData = 8;
            WriteUInt16(ExifBlock, OffsetIfdPrimaryData, 0, _ByteOrder); // Number of tags of IFD Primary Data
            WriteUInt32(ExifBlock, OffsetIfdPrimaryData + 2, 0, _ByteOrder); // Offset of IFD Thumbnail Data
            InitTagTable();
        }


        private void WriteTagToByteBlock(TagItem TempTagData, byte[] ByteBlock, ref int TagDataIndex, ref int OutsourcedDataIndex, ExifByteOrder ByteOrder)
        {
            int i, ByteCount;
            byte v;

            i = TagDataIndex;
            TagDataIndex += 12;
            WriteUInt16(ByteBlock, i, (ushort)TempTagData.TagId, ByteOrder);
            WriteUInt16(ByteBlock, i + 2, (ushort)TempTagData.TagType, ByteOrder);
            WriteUInt32(ByteBlock, i + 4, (uint)TempTagData.ValueCount, ByteOrder);
            ByteCount = GetTagByteCount(TempTagData.TagType, TempTagData.ValueCount);
            if (ByteCount <= 4)
            {
                // The tag does not have outsourced data. In this case exactly 4 bytes have to be written.
                ByteBlock[i + 8] = TempTagData.ValueData[TempTagData.ValueIndex];
                v = 0;
                if (ByteCount >= 2) v = TempTagData.ValueData[TempTagData.ValueIndex + 1];
                ByteBlock[i + 9] = v;
                v = 0;
                if (ByteCount >= 3) v = TempTagData.ValueData[TempTagData.ValueIndex + 2];
                ByteBlock[i + 10] = v;
                v = 0;
                if (ByteCount >= 4) v = TempTagData.ValueData[TempTagData.ValueIndex + 3];
                ByteBlock[i + 11] = v;
            }
            else
            {
                // The tag has outsourced data.
                WriteUInt32(ByteBlock, i + 8, (uint)OutsourcedDataIndex, ByteOrder);
                Array.Copy(TempTagData.ValueData, TempTagData.ValueIndex, ByteBlock, OutsourcedDataIndex, ByteCount);
                OutsourcedDataIndex += ByteCount;
                // The EXIF block must have a 16 bit alignment.
                if ((ByteCount & 0x1) != 0)
                {
                    ByteBlock[OutsourcedDataIndex] = 0; // "ByteCount" is odd, therefore add a fill byte
                    OutsourcedDataIndex++;
                }
            }
        }


        private void CreateNewExifBlock(out byte[] NewExifBlock, out int NewExifBlockLen, ExifByteOrder ByteOrder)
        {
            byte[] WriteExifBlock;
            int i, OutsourcedDataIndex;
            TagItem PrivateDataPointerTag, GpsInfoDataPointerTag, InteroperabilityPointerTag, ThumbnailImagePointerTag;
            bool ThumbnailImageSizeTagPresent;

            try
            {
                if (IsExifBlockEmpty())
                {
                    NewExifBlock = null;
                    NewExifBlockLen = 0;
                }
                else
                {
                    WriteExifBlock = new byte[MaxExifBlockLen];
                    NewExifBlock = WriteExifBlock;
                    i = 0;
                    if (ByteOrder == ExifByteOrder.BigEndian)
                    {
                        WriteExifBlock[i] = (byte)'M'; i++;
                        WriteExifBlock[i] = (byte)'M'; i++;
                    }
                    else if (ByteOrder == ExifByteOrder.LittleEndian)
                    {
                        WriteExifBlock[i] = (byte)'I'; i++;
                        WriteExifBlock[i] = (byte)'I'; i++;
                    }
                    else throw new ExifException(ExifErrCode.InternalError);

                    WriteUInt16(WriteExifBlock, i, FixExifValue, ByteOrder);
                    i += 2;
                    WriteUInt32(WriteExifBlock, i, 8, ByteOrder);
                    i += 4;

                    Dictionary<ExifTagId, TagItem> PrivateDataIfdTable = TagTable[(uint)ExifIfd.PrivateData];
                    Dictionary<ExifTagId, TagItem> InteroperabilityIfdTable = TagTable[(uint)ExifIfd.Interoperability];
                    PrivateDataIfdTable.TryGetValue(ExifTagId.InteroperabilityIfdPointer, out InteroperabilityPointerTag);
                    int InteroperabilityTagCount = InteroperabilityIfdTable.Count;
                    if (InteroperabilityTagCount > 0)
                    {
                        if (InteroperabilityPointerTag == null)
                        {
                            InteroperabilityPointerTag = new TagItem(ExifTag.InteroperabilityIfdPointer, ExifTagType.ULong, 1);
                            PrivateDataIfdTable.Add(ExifTagId.InteroperabilityIfdPointer, InteroperabilityPointerTag);
                        }
                    }
                    else
                    {
                        if (InteroperabilityPointerTag != null)
                        {
                            PrivateDataIfdTable.Remove(ExifTagId.InteroperabilityIfdPointer);
                        }
                    }

                    Dictionary<ExifTagId, TagItem> PrimaryDataIfdTable = TagTable[(uint)ExifIfd.PrimaryData];
                    Dictionary<ExifTagId, TagItem> GpsInfoDataIfdTable = TagTable[(uint)ExifIfd.GpsInfoData];
                    PrimaryDataIfdTable.TryGetValue(ExifTagId.GpsInfoIfdPointer, out GpsInfoDataPointerTag);
                    int GpsInfoDataTagCount = GpsInfoDataIfdTable.Count;
                    if (GpsInfoDataTagCount > 0)
                    {
                        if (GpsInfoDataPointerTag == null)
                        {
                            GpsInfoDataPointerTag = new TagItem(ExifTag.GpsInfoIfdPointer, ExifTagType.ULong, 1);
                            PrimaryDataIfdTable.Add(ExifTagId.GpsInfoIfdPointer, GpsInfoDataPointerTag);
                        }
                    }
                    else
                    {
                        if (GpsInfoDataPointerTag != null)
                        {
                            PrimaryDataIfdTable.Remove(ExifTagId.GpsInfoIfdPointer);
                        }
                    }


                    PrimaryDataIfdTable.TryGetValue(ExifTagId.ExifIfdPointer, out PrivateDataPointerTag);
                    int PrivateDataTagCount = PrivateDataIfdTable.Count;
                    if (PrivateDataTagCount > 0)
                    {
                        if (PrivateDataPointerTag == null)
                        {
                            PrivateDataPointerTag = new TagItem(ExifTag.ExifIfdPointer, ExifTagType.ULong, 1);
                            PrimaryDataIfdTable.Add(ExifTagId.ExifIfdPointer, PrivateDataPointerTag);
                        }
                    }
                    else
                    {
                        if (PrivateDataPointerTag != null)
                        {
                            PrimaryDataIfdTable.Remove(ExifTagId.ExifIfdPointer);
                        }
                    }

                    // IFD Primary Data
                    int PrimaryDataTagCount = PrimaryDataIfdTable.Count;
                    int PrivateDataIfdPointerIndex = -1;
                    int GpsInfoDataIfdPointerIndex = -1;
                    WriteUInt16(WriteExifBlock, i, (ushort)PrimaryDataTagCount, ByteOrder);
                    i += 2;
                    OutsourcedDataIndex = i + PrimaryDataTagCount * 12 + 4; // "+ 4" for thumbnail data pointer
                    foreach (TagItem t in PrimaryDataIfdTable.Values)
                    {
                        if (t.TagId == ExifTagId.ExifIfdPointer)
                        {
                            t.TagType = ExifTagType.ULong;
                            PrivateDataIfdPointerIndex = i + 8;  // The "Private Data IFD" pointer is unknown at this time and a dummy
                                                                 // value is written for it. Therefore the index is stored, at which the actual pointer can be written later.
                        }
                        else if (t.TagId == ExifTagId.GpsInfoIfdPointer)
                        {
                            t.TagType = ExifTagType.ULong;
                            GpsInfoDataIfdPointerIndex = i + 8; // The "GPS Info Data IFD" pointer is unknown at this time and a dummy
                                                                // value is written for it. Therefore the index is stored, at which the actual pointer can be written later.
                        }
                        WriteTagToByteBlock(t, WriteExifBlock, ref i, ref OutsourcedDataIndex, ByteOrder);
                    }

                    // Now the thumbnail data pointer should be written. Because it is unknown at this time the index is stored.
                    int ThumbnailDataIfdPointerIndex = i;
                    i = OutsourcedDataIndex;

                    // IFD Private Data
                    int InteroperabilityIfdPointerIndex = -1;
                    if (PrivateDataTagCount > 0)
                    {
                        int iPrivateDataStart = i;
                        int MakerNoteDataPointerIndex, OffsetSchemaValueIndex;
                        TagItem OffsetSchemaTag;
                        bool AddOffsetSchemaTag;
                        WriteUInt32(PrivateDataPointerTag.ValueData, PrivateDataPointerTag.ValueIndex, (uint)i, ByteOrder);
                        WriteUInt32(WriteExifBlock, PrivateDataIfdPointerIndex, (uint)i, ByteOrder);
                        do
                        {
                            i = iPrivateDataStart;
                            MakerNoteDataPointerIndex = -1;
                            OffsetSchemaValueIndex = -1;
                            OffsetSchemaTag = null;
                            WriteUInt16(WriteExifBlock, i, (ushort)PrivateDataTagCount, ByteOrder);
                            i += 2;
                            OutsourcedDataIndex = i + PrivateDataTagCount * 12 + 4; // "+ 4" for null pointer at the end
                            foreach (TagItem t in PrivateDataIfdTable.Values)
                            {
                                if (t.TagId == ExifTagId.InteroperabilityIfdPointer)
                                {
                                    t.TagType = ExifTagType.ULong;
                                    InteroperabilityIfdPointerIndex = i + 8; // The "Interoperability IFD" pointer is unknown at this time and a dummy
                                                                             // value is written for it. Therefore the index is stored, at which the actual pointer can be written later.
                                }
                                else if (t.TagId == ExifTagId.MakerNote)
                                {
                                    MakerNoteDataPointerIndex = i + 8;
                                }
                                else if (t.TagId == ExifTagId.OffsetSchema)
                                {
                                    t.TagType = ExifTagType.SLong;
                                    OffsetSchemaValueIndex = i + 8;
                                    OffsetSchemaTag = t;
                                }
                                WriteTagToByteBlock(t, WriteExifBlock, ref i, ref OutsourcedDataIndex, ByteOrder);
                            }
                            WriteUInt32(WriteExifBlock, i, 0, ByteOrder); // Write null pointer at the end of the IFD
                            i = OutsourcedDataIndex;
                            AddOffsetSchemaTag = CheckIfMakerNoteTagHasMoved(WriteExifBlock, MakerNoteDataPointerIndex, OffsetSchemaValueIndex, OffsetSchemaTag, ByteOrder);
                            if (AddOffsetSchemaTag)
                            {
                                TagItem t = new TagItem(ExifTag.OffsetSchema, ExifTagType.SLong, 1);
                                PrivateDataIfdTable.Add(ExifTagId.OffsetSchema, t);
                                PrivateDataTagCount++;
                            }
                        } while (AddOffsetSchemaTag);
                    }

                    // IFD GPS Info Data
                    if (GpsInfoDataTagCount > 0)
                    {
                        WriteUInt32(GpsInfoDataPointerTag.ValueData, GpsInfoDataPointerTag.ValueIndex, (uint)i, ByteOrder);
                        WriteUInt32(WriteExifBlock, GpsInfoDataIfdPointerIndex, (uint)i, ByteOrder);
                        WriteUInt16(WriteExifBlock, i, (ushort)GpsInfoDataTagCount, ByteOrder);
                        i += 2;
                        OutsourcedDataIndex = i + GpsInfoDataTagCount * 12 + 4;
                        foreach (TagItem t in GpsInfoDataIfdTable.Values)
                        {
                            WriteTagToByteBlock(t, WriteExifBlock, ref i, ref OutsourcedDataIndex, ByteOrder);
                        }
                        WriteUInt32(WriteExifBlock, i, 0, ByteOrder); // Write null pointer at the end of the IFD
                        i = OutsourcedDataIndex;
                    }

                    // IFD Interoperability
                    if (InteroperabilityTagCount > 0)
                    {
                        WriteUInt32(InteroperabilityPointerTag.ValueData, InteroperabilityPointerTag.ValueIndex, (uint)i, ByteOrder);
                        WriteUInt32(WriteExifBlock, InteroperabilityIfdPointerIndex, (uint)i, ByteOrder);
                        WriteUInt16(WriteExifBlock, i, (ushort)InteroperabilityTagCount, ByteOrder);
                        i += 2;
                        OutsourcedDataIndex = i + InteroperabilityTagCount * 12 + 4;
                        foreach (TagItem t in InteroperabilityIfdTable.Values)
                        {
                            WriteTagToByteBlock(t, WriteExifBlock, ref i, ref OutsourcedDataIndex, ByteOrder);
                        }
                        WriteUInt32(WriteExifBlock, i, 0, ByteOrder); // Write null pointer at the end of the IFD
                        i = OutsourcedDataIndex;
                    }

                    // IFD Thumbnail Data
                    Dictionary<ExifTagId, TagItem> ThumbnailDataIfdTable = TagTable[(uint)ExifIfd.ThumbnailData];
                    int ThumbnailDataTagCount = ThumbnailDataIfdTable.Count;
                    if ((ThumbnailDataTagCount > 0) || (ThumbnailImageExists()))
                    {
                        WriteUInt32(WriteExifBlock, ThumbnailDataIfdPointerIndex, (uint)i, ByteOrder);
                        WriteUInt16(WriteExifBlock, i, (ushort)ThumbnailDataTagCount, ByteOrder);
                        i += 2;
                        OutsourcedDataIndex = i + ThumbnailDataTagCount * 12 + 4;
                        int ThumbnailImagePointerIndex = -1;
                        ThumbnailImagePointerTag = null;
                        ThumbnailImageSizeTagPresent = false;
                        foreach (TagItem t in ThumbnailDataIfdTable.Values)
                        {
                            if (t.TagId == ExifTagId.JpegInterchangeFormat)
                            {
                                // The tag "TagId_JpegInterchangeFormat" stores a pointer to the thumbnail image.
                                // The pointer is not known at this time, so the index, where the pointer has to be written, is stored.
                                t.TagType = ExifTagType.ULong;
                                ThumbnailImagePointerTag = t;
                                ThumbnailImagePointerIndex = i + 8;
                            }
                            else if (t.TagId == ExifTagId.JpegInterchangeFormatLength)
                            {
                                t.TagType = ExifTagType.ULong;
                                WriteUInt32(t.ValueData, t.ValueIndex, (uint)ThumbnailByteCount, ByteOrder);
                                ThumbnailImageSizeTagPresent = true;
                            }
                            WriteTagToByteBlock(t, WriteExifBlock, ref i, ref OutsourcedDataIndex, ByteOrder);
                        }

                        if (ThumbnailImage != null)
                        {
                            // A thumbnail image is defined
                            if ((ThumbnailImagePointerIndex < 0) || (ThumbnailImageSizeTagPresent == false))
                            {
                                throw new ExifException(ExifErrCode.InternalError);
                            }
                            else
                            {
                                WriteUInt32(ThumbnailImagePointerTag.ValueData, ThumbnailImagePointerTag.ValueIndex, (uint)OutsourcedDataIndex, ByteOrder);
                                WriteUInt32(WriteExifBlock, ThumbnailImagePointerIndex, (uint)OutsourcedDataIndex, ByteOrder);
                                Array.Copy(ThumbnailImage, ThumbnailStartIndex, WriteExifBlock, OutsourcedDataIndex, ThumbnailByteCount);
                                OutsourcedDataIndex += ThumbnailByteCount;
                                if ((OutsourcedDataIndex & 0x1) != 0)
                                {
                                    WriteExifBlock[OutsourcedDataIndex] = 0; // Insert fill byte
                                    OutsourcedDataIndex++;
                                }
                            }
                        }
                        else
                        {
                            // A thumbnail image is not defined
                            if (ThumbnailImagePointerIndex >= 0)
                            {
                                throw new ExifException(ExifErrCode.InternalError);
                            }
                        }
                        WriteUInt32(WriteExifBlock, i, 0, ByteOrder); // Write null pointer at the end of the IFD
                        i = OutsourcedDataIndex;
                    }
                    else
                    {
                        WriteUInt32(WriteExifBlock, ThumbnailDataIfdPointerIndex, 0, ByteOrder);
                    }

                    NewExifBlockLen = i;
                }
            }
            catch (Exception ex)
            {
                if ((ex is IndexOutOfRangeException) || (ex is ArgumentException))
                {
                    // Most likely reason for this exception: array "WriteExifBlock" is overflowed
                    throw new ExifException(ExifErrCode.ExifDataAreTooLarge);
                }
                else if (ex is ExifException)
                {
                    throw;
                }
                else throw new ExifException(ExifErrCode.InternalError);
            }
        }


        private bool CheckIfMakerNoteTagHasMoved(byte[] DataBlock, int MakerNoteDataPointerIndex, int OffsetSchemaValueIndex, TagItem OffsetSchemaTag, ExifByteOrder ByteOrder)
        {
            bool MakerNoteHasMovedAndOffsetSchemaTagRequired = false;
            int MakerNoteCurrentIndex;

            if ((MakerNoteOriginalIndex >= 0) && (MakerNoteDataPointerIndex >= 0))
            {
                // "MakerNote" has existed and is now existing
                MakerNoteCurrentIndex = (int)ReadUInt32(DataBlock, MakerNoteDataPointerIndex, ByteOrder);
                if (MakerNoteOriginalIndex != MakerNoteCurrentIndex)
                {
                    // "MakerNote" has moved
                    if (OffsetSchemaValueIndex >= 0)
                    {
                        int NewOffset = MakerNoteCurrentIndex - MakerNoteOriginalIndex;
                        WriteUInt32(DataBlock, OffsetSchemaValueIndex, (uint)NewOffset, ByteOrder);
                        WriteUInt32(OffsetSchemaTag.ValueData, OffsetSchemaTag.ValueIndex, (uint)NewOffset, ByteOrder);
                    }
                    else
                    {
                        MakerNoteHasMovedAndOffsetSchemaTagRequired = true;
                    }
                }
                else
                {
                    // "MakerNote" has not moved
                    if (OffsetSchemaValueIndex >= 0)
                    {
                        WriteUInt32(DataBlock, OffsetSchemaValueIndex, 0, ByteOrder); // Set move offset to 0
                        WriteUInt32(OffsetSchemaTag.ValueData, OffsetSchemaTag.ValueIndex, 0, ByteOrder);
                    }
                }
            }
            else
            {
                if (OffsetSchemaValueIndex >= 0)
                {
                    WriteUInt32(DataBlock, OffsetSchemaValueIndex, 0, ByteOrder); // Set move offset to 0
                    WriteUInt32(OffsetSchemaTag.ValueData, OffsetSchemaTag.ValueIndex, 0, ByteOrder);
                }
            }
            return (MakerNoteHasMovedAndOffsetSchemaTagRequired);
        }


        // Create a tag and initialize it with the data, which are stored in "ExifBlock[ExifBlockIndex]". 
        // The data values of the tag are not copied, instead a reference to the array "ExifBlock" is assigned.
        private TagItem CreateTagWithReferenceToExifBlock(ExifIfd Ifd, int ExifBlockIndex)
        {
            int ValueIndex, ValueByteCount, AllocatedByteCount;

            ExifTagId TagId = (ExifTagId)ReadUInt16(ExifBlock, ExifBlockIndex, _ByteOrder);
            ExifTagType TagType = (ExifTagType)ReadUInt16(ExifBlock, ExifBlockIndex + 2, _ByteOrder);
            uint ValueCount = ReadUInt32(ExifBlock, ExifBlockIndex + 4, _ByteOrder);
            if (ValueCount > MaxValueCount) ValueCount = MaxValueCount;
            ValueByteCount = GetTagByteCount(TagType, (int)ValueCount);
            if (ValueByteCount <= 4)
            {
                // The tag doesn't have outsourced data. In this case a memory space with a size of 4 bytes is available for the data.
                ValueIndex = ExifBlockIndex + 8;
                AllocatedByteCount = 4;
            }
            else
            {
                // The tag has outsourced data.
                ValueIndex = (int)ReadUInt32(ExifBlock, ExifBlockIndex + 8, _ByteOrder);
                AllocatedByteCount = (ValueByteCount + 1) & ~0x1; // The number of allocated bytes is the number of the required
                                                                  // bytes rounded up to the next even number.
            }
            TagItem TempTagData = new TagItem(TagId, TagType, (int)ValueCount, ExifBlock, ValueIndex, AllocatedByteCount);
            return (TempTagData);
        }


        private void InitTagTable()
        {
            try
            {
                int TagCount, i, j, MakerNoteIndex = -1, OffsetSchemaValue = 0;
                int PrivateDataOffset, GpsInfoDataOffset, InteroperabilityOffset, ThumbnailDataOffset;
                TagItem TempTagData;

                PrivateDataOffset = 0;
                GpsInfoDataOffset = 0;
                InteroperabilityOffset = 0;
                TagTable = new Dictionary<ExifTagId, TagItem>[ExifIfdCount];
                var PrimaryDataIfdTable = new Dictionary<ExifTagId, TagItem>(40);
                TagTable[(uint)ExifIfd.PrimaryData] = PrimaryDataIfdTable;
                var PrivateDataIfdTable = new Dictionary<ExifTagId, TagItem>(60);
                TagTable[(uint)ExifIfd.PrivateData] = PrivateDataIfdTable;
                var GpsInfoDataIfdTable = new Dictionary<ExifTagId, TagItem>(20);
                TagTable[(uint)ExifIfd.GpsInfoData] = GpsInfoDataIfdTable;
                var InteroperabilityIfdTable = new Dictionary<ExifTagId, TagItem>(2);
                TagTable[(uint)ExifIfd.Interoperability] = InteroperabilityIfdTable;
                var ThumbnailDataIfdTable = new Dictionary<ExifTagId, TagItem>(10);
                TagTable[(uint)ExifIfd.ThumbnailData] = ThumbnailDataIfdTable;

                // IFD Primary Data
                i = OffsetIfdPrimaryData;
                TagCount = ReadUInt16(ExifBlock, i, _ByteOrder);
                i += 2;
                j = 0;
                while (j < TagCount)
                {
                    TempTagData = CreateTagWithReferenceToExifBlock(ExifIfd.PrimaryData, i);
                    if (TempTagData.TagId == ExifTagId.ExifIfdPointer)
                    {
                        PrivateDataOffset = (int)ReadUInt32(TempTagData.ValueData, TempTagData.ValueIndex, _ByteOrder);
                    }
                    else if (TempTagData.TagId == ExifTagId.GpsInfoIfdPointer)
                    {
                        GpsInfoDataOffset = (int)ReadUInt32(TempTagData.ValueData, TempTagData.ValueIndex, _ByteOrder);
                    }
                    AddIfNotExists(PrimaryDataIfdTable, TempTagData);
                    i += 12;
                    j++;
                }
                ThumbnailDataOffset = (int)ReadUInt32(ExifBlock, i, _ByteOrder);

                // IFD Private Data
                if (PrivateDataOffset != 0)
                {
                    i = PrivateDataOffset;
                    TagCount = ReadUInt16(ExifBlock, i, _ByteOrder);
                    i += 2;
                    j = 0;
                    while (j < TagCount)
                    {
                        TempTagData = CreateTagWithReferenceToExifBlock(ExifIfd.PrivateData, i);
                        if (TempTagData.TagId == ExifTagId.InteroperabilityIfdPointer)
                        {
                            InteroperabilityOffset = (int)ReadUInt32(TempTagData.ValueData, TempTagData.ValueIndex, _ByteOrder);
                        }
                        else if (TempTagData.TagId == ExifTagId.MakerNote)
                        {
                            MakerNoteIndex = TempTagData.ValueIndex;
                        }
                        else if (TempTagData.TagId == ExifTagId.OffsetSchema)
                        {
                            OffsetSchemaValue = (int)ReadUInt32(TempTagData.ValueData, TempTagData.ValueIndex, _ByteOrder);
                        }
                        AddIfNotExists(PrivateDataIfdTable, TempTagData);
                        i += 12;
                        j++;
                    }
                }

                if (MakerNoteIndex >= 0)
                {
                    MakerNoteOriginalIndex = MakerNoteIndex - OffsetSchemaValue;
                }
                else MakerNoteOriginalIndex = -1;

                // IFD GPS Info Data
                if (GpsInfoDataOffset != 0)
                {
                    i = GpsInfoDataOffset;
                    TagCount = ReadUInt16(ExifBlock, i, _ByteOrder);
                    i += 2;
                    j = 0;
                    while (j < TagCount)
                    {
                        TempTagData = CreateTagWithReferenceToExifBlock(ExifIfd.GpsInfoData, i);
                        AddIfNotExists(GpsInfoDataIfdTable, TempTagData);
                        i += 12;
                        j++;
                    }
                }

                // IFD Interoperability
                if (InteroperabilityOffset != 0)
                {
                    i = InteroperabilityOffset;
                    TagCount = ReadUInt16(ExifBlock, i, _ByteOrder);
                    i += 2;
                    j = 0;
                    while (j < TagCount)
                    {
                        TempTagData = CreateTagWithReferenceToExifBlock(ExifIfd.Interoperability, i);
                        AddIfNotExists(InteroperabilityIfdTable, TempTagData);
                        i += 12;
                        j++;
                    }
                }

                // IFD Thumbnail Data
                if (ThumbnailDataOffset != 0)
                {
                    i = ThumbnailDataOffset;
                    TagCount = ReadUInt16(ExifBlock, i, _ByteOrder);
                    i += 2;
                    j = 0;
                    while (j < TagCount)
                    {
                        TempTagData = CreateTagWithReferenceToExifBlock(ExifIfd.ThumbnailData, i);
                        if (TempTagData.TagId == ExifTagId.JpegInterchangeFormat)
                        {
                            ThumbnailImage = ExifBlock;
                            ThumbnailStartIndex = (int)ReadUInt32(TempTagData.ValueData, TempTagData.ValueIndex, _ByteOrder);
                        }
                        else if (TempTagData.TagId == ExifTagId.JpegInterchangeFormatLength)
                        {
                            ThumbnailByteCount = (int)ReadUInt32(TempTagData.ValueData, TempTagData.ValueIndex, _ByteOrder);
                        }
                        AddIfNotExists(ThumbnailDataIfdTable, TempTagData);
                        i += 12;
                        j++;
                    }
                }
            }
            catch
            {
                throw new ExifException(ExifErrCode.ExifBlockHasIllegalContent);
            }
        }


        private void AddIfNotExists(Dictionary<ExifTagId, TagItem> Dict, TagItem TagData)
        {
            try
            {
                Dict.Add(TagData.TagId, TagData);
            }
            catch { } // An exception occurs, if the tag ID already exists. In this case ignore the new tag item.
        }


        // Compare "Array1" from index "StartIndex1" with "Array2".
        private bool CompareArrays(byte[] Array1, int StartIndex1, byte[] Array2)
        {
            bool IsEqual = true;
            int i = StartIndex1;

            foreach (byte b in Array2)
            {
                if (Array1[i] != b)
                {
                    IsEqual = false;
                    break;
                }
                i++;
            }
            return (IsEqual);
        }


        // Read 16 bit value from a stream in big endian format.
        private static void ReadStreamUInt16BE(Stream StreamObj, out ushort Value)
        {
            int b1, b2;

            b1 = StreamObj.ReadByte();
            b2 = StreamObj.ReadByte();
            if ((b1 >= 0) && (b2 >= 0))
            {
                Value = (ushort)((b1 << 8) | b2);
            }
            else
            {
                throw new ExifException(ExifErrCode.UnexpectedEndOfFile);
            }
        }


        // Write 16 bit value to a stream in big endian format.
        private static void WriteFileUint16BE(Stream StreamObj, ushort Value)
        {
            StreamObj.WriteByte((byte)(Value >> 8));
            StreamObj.WriteByte((byte)Value);
        }


        private bool ReadUintElement(TagItem t, int ElementIndex, out uint Value)
        {
            bool Success = false;

            if ((ElementIndex >= 0) && (ElementIndex < t.ValueCount))
            {
                switch (t.TagType)
                {
                    case ExifTagType.Byte:
                        Value = t.ValueData[t.ValueIndex + ElementIndex];
                        Success = true;
                        break;
                    case ExifTagType.UShort:
                        Value = ReadUInt16(t.ValueData, t.ValueIndex + (ElementIndex << 1), _ByteOrder);
                        Success = true;
                        break;
                    case ExifTagType.ULong:
                    case ExifTagType.SLong:
                        Value = ReadUInt32(t.ValueData, t.ValueIndex + (ElementIndex << 2), _ByteOrder);
                        Success = true;
                        break;
                    default:
                        Value = 0;
                        break;
                }
            }
            else Value = 0;
            return (Success);
        }


        private bool WriteUintElement(TagItem t, int ElementIndex, uint Value)
        {
            bool Success = false;

            if (t != null)
            {
                if (t.TagType == ExifTagType.Byte)
                {
                    t.ValueData[t.ValueIndex + ElementIndex] = (byte)Value;
                }
                else if (t.TagType == ExifTagType.UShort)
                {
                    WriteUInt16(t.ValueData, t.ValueIndex + (ElementIndex << 1), (ushort)Value, _ByteOrder);
                }
                else
                {
                    WriteUInt32(t.ValueData, t.ValueIndex + (ElementIndex << 2), Value, _ByteOrder);
                }
                Success = true;
            }
            return (Success);
        }


        private bool GetTagValueWithIdCode(ExifTag TagSpec, out string Value, ushort CodePage)
        {
            TagItem t;
            bool Success = false, IsUtf16Coded = false, IsAsciiCoded = false;
            int i, j;

            Value = null;
            if (GetTagItem(TagSpec, out t) && (t.TagType == ExifTagType.Undefined) && (t.ValueCount >= IdCodeLength))
            {
                if (CompareArrays(t.ValueData, t.ValueIndex, IdCodeUtf16))
                {
                    IsUtf16Coded = true;
                }
                else if (CompareArrays(t.ValueData, t.ValueIndex, IdCodeAscii) || CompareArrays(t.ValueData, t.ValueIndex, IdCodeDefault))
                {
                    IsAsciiCoded = true;
                }

                i = t.ValueCount - IdCodeLength;
                j = t.ValueIndex + IdCodeLength;
                if (IsUtf16Coded)
                {
                    // The parameter "CodePage" is ignored in this case.
                    // Remove all null terminating characters. Here a null terminating character consists of 2 zero-bytes.
                    while ((i >= 2) && (t.ValueData[j + i - 2] == 0) && (t.ValueData[j + i - 1] == 0))
                    {
                        i -= 2;
                    }
                    if (ByteOrder == ExifByteOrder.BigEndian)
                    {
                        CodePage = 1201; // UTF16BE
                    }
                    else CodePage = 1200; // UTF16LE
                    Value = Encoding.GetEncoding(CodePage).GetString(t.ValueData, j, i);
                    Success = true;
                }
                else if (IsAsciiCoded)
                {
                    // Remove all null terminating characters.
                    while ((i >= 1) && (t.ValueData[j + i - 1] == 0))
                    {
                        i--;
                    }
                    if ((CodePage == 1200) || (CodePage == 1201)) // UTF16LE or UTF16BE
                    {
                        CodePage = 20127; // Ignore parameter "CodePage" and overwrite it with US ASCII
                    }
                    Value = Encoding.GetEncoding(CodePage).GetString(t.ValueData, j, i);
                    Success = true;
                }
            }

            return (Success);
        }


        private bool SetTagValueWithIdCode(ExifTag TagSpec, string Value, ushort CodePage)
        {
            int TotalByteCount, StrByteLen;
            TagItem t;
            bool Success = false;
            byte[] StringAsByteArray, RequiredIdCode;

            if ((CodePage == 1200) && (ByteOrder == ExifByteOrder.BigEndian))
            {
                CodePage = 1201; // Set code page to UTF16BE
            }
            if ((CodePage == 1201) && (ByteOrder == ExifByteOrder.LittleEndian))
            {
                CodePage = 1200; // Set code page to UTF16LE
            }
            StringAsByteArray = Encoding.GetEncoding(CodePage).GetBytes(Value);
            StrByteLen = StringAsByteArray.Length;
            TotalByteCount = IdCodeLength + StrByteLen; // The ID code is a 8 byte header
            t = PrepareTagForCompleteWriting(TagSpec, ExifTagType.Undefined, TotalByteCount);
            if (t != null)
            {
                if ((CodePage == 1200) || (CodePage == 1201))
                {
                    RequiredIdCode = IdCodeUtf16;
                }
                else RequiredIdCode = IdCodeAscii;
                Array.Copy(RequiredIdCode, 0, t.ValueData, t.ValueIndex, IdCodeLength);
                Array.Copy(StringAsByteArray, 0, t.ValueData, t.ValueIndex + IdCodeLength, StrByteLen);
                Success = true;
            }
            return (Success);
        }


        private bool ReadURatElement(TagItem t, int ElementIndex, out uint Numer, out uint Denom)
        {
            bool Success = false;
            int i;

            if ((ElementIndex >= 0) && (ElementIndex < t.ValueCount))
            {
                if ((t.TagType == ExifTagType.SRational) || (t.TagType == ExifTagType.URational))
                {
                    i = t.ValueIndex + (ElementIndex << 3);
                    Numer = ReadUInt32(t.ValueData, i, ByteOrder);
                    Denom = ReadUInt32(t.ValueData, i + 4, ByteOrder);
                    Success = true;
                }
                else
                {
                    Numer = 0;
                    Denom = 0;
                }
            }
            else
            {
                Numer = 0;
                Denom = 0;
            }
            return (Success);
        }


        private bool WriteURatElement(TagItem t, int ElementIndex, uint Numer, uint Denom)
        {
            int i;
            bool Success = false;

            if (t != null)
            {
                i = t.ValueIndex + (ElementIndex << 3);
                WriteUInt32(t.ValueData, i, Numer, _ByteOrder);
                WriteUInt32(t.ValueData, i + 4, Denom, _ByteOrder);
                Success = true;
            }
            return (Success);
        }


        private bool GetTagItem(ExifTag TagSpec, out TagItem t)
        {
            ExifIfd Ifd = ExtractIfd(TagSpec);
            if ((uint)Ifd < ExifIfdCount)
            {
                Dictionary<ExifTagId, TagItem> IfdTagTable = TagTable[(uint)Ifd];
                return (IfdTagTable.TryGetValue(ExtractTagId(TagSpec), out t));
            }
            else
            {
                t = null;
                return (false);
            }
        }


        // Prepare tag for writing a single array item. If the tag does not exist it is created.
        //
        // Parameters:
        //  "TagType":    Tag type.
        //  "ArrayIndex": Zero-based array index of the value to be written. If the array index
        //                is outside the current tag data, the tag data is automatically enlarged.
        //                If it is necessary to reallocate the tag memory, the old content is copied to the new memory.
        private TagItem PrepareTagForArrayItemWriting(ExifTag TagSpec, ExifTagType TagType, int ArrayIndex)
        {
            TagItem t = null;
            ExifIfd Ifd = ExtractIfd(TagSpec);
            if (((uint)Ifd < ExifIfdCount) && ((uint)ArrayIndex < MaxValueCount))
            {
                Dictionary<ExifTagId, TagItem> IfdTagTable = TagTable[(uint)Ifd];
                if (IfdTagTable.TryGetValue(ExtractTagId(TagSpec), out t))
                {
                    int ValueCount = t.ValueCount;
                    if (ArrayIndex >= ValueCount)
                    {
                        ValueCount = ArrayIndex + 1;
                    }
                    t.SetTagTypeAndValueCount(TagType, ValueCount, true);
                }
                else
                {
                    int ValueCount = ArrayIndex + 1;
                    t = new TagItem(TagSpec, TagType, ValueCount);
                    IfdTagTable.Add(t.TagId, t);
                }
            }
            return (t);
        }


        // Prepare tag for a complete writing of the tag content. The tag content is undefined after calling this method.
        //
        // Parameters:
        //  "TagType":    New tag type. The tag type must be valid, because it is not checked in this method!
        //  "ValueCount": New number of array elements that the tag should contain. Must be equal or greater than 1.
        //                If it is necessary to reallocate the tag memory, the old content is not copied.
        private TagItem PrepareTagForCompleteWriting(ExifTag TagSpec, ExifTagType TagType, int ValueCount)
        {
            TagItem t = null;

            ExifIfd Ifd = ExtractIfd(TagSpec);
            if (((uint)Ifd < ExifIfdCount) && ((uint)ValueCount <= MaxValueCount))
            {
                Dictionary<ExifTagId, TagItem> IfdTagTable = TagTable[(uint)Ifd];
                if (IfdTagTable.TryGetValue(ExtractTagId(TagSpec), out t))
                {
                    t.SetTagTypeAndValueCount(TagType, ValueCount, false);
                }
                else
                {
                    t = new TagItem(TagSpec, TagType, ValueCount);
                    IfdTagTable.Add(t.TagId, t);
                }
#if DEBUG
                int k = t.ValueIndex + t.AllocatedByteCount;
                for (int i = t.ValueIndex; i < k; i++)
                {
                    t.ValueData[i] = 0xcc;
                }
#endif
            }
            return (t);
        }


        private static int CalculateTwoDigitDecNumber(byte[] ByteArr, int Index)
        {
            int d1, d2, Value;

            Value = -1;
            d1 = ByteArr[Index];
            d2 = ByteArr[Index + 1];
            if ((d1 >= '0') && (d1 <= '9') && (d2 >= '0') && (d2 <= '9'))
            {
                Value = (d1 - 0x30) * 10 + (d2 - 0x30);
            }
            return (Value);
        }


        // "Value": Number from 0 to 99.
        private static void ConvertTwoDigitNumberToByteArr(byte[] ByteArr, ref int Index, int Value)
        {
            ByteArr[Index] = (byte)((Value / 10) + 0x30);
            Index++;
            ByteArr[Index] = (byte)((Value % 10) + 0x30);
            Index++;
        }


        private void ClearIfd_Unchecked(ExifIfd Ifd)
        {
            TagTable[(uint)Ifd].Clear();
        }


        private void RemoveThumbnailImage_Internal()
        {
            ThumbnailImage = null;
            ThumbnailStartIndex = 0;
            ThumbnailByteCount = 0;
        }


        private bool GetGpsCoordinateHelper(out GeoCoordinate Value, ExifTag ValueTag, ExifTag RefTag, char Cp1, char Cp2)
        {
            bool Success = false;
            ExifRational Deg, Min, Sec;
            string Ref;
            char CardinalPoint;

            if (GetTagValue(ValueTag, out Deg, 0) && Deg.IsValid() &&
                GetTagValue(ValueTag, out Min, 1) && Min.IsValid() &&
                GetTagValue(ValueTag, out Sec, 2) && Sec.IsValid() &&
                GetTagValue(RefTag, out Ref, StrCoding.Utf8) && (Ref.Length == 1))
            {
                CardinalPoint = Ref[0];
                if ((CardinalPoint == Cp1) || (CardinalPoint == Cp2))
                {
                    Value.Degree = ExifRational.ToDecimal(Deg);
                    Value.Minute = ExifRational.ToDecimal(Min);
                    Value.Second = ExifRational.ToDecimal(Sec);
                    Value.CardinalPoint = CardinalPoint;
                    Success = true;
                }
                else Value = new GeoCoordinate();
            }
            else Value = new GeoCoordinate();
            return (Success);
        }


        private bool SetGpsCoordinateHelper(GeoCoordinate Value, ExifTag ValueTag, ExifTag RefTag, char Cp1, char Cp2)
        {
            bool Success = false;

            ExifRational Deg = ExifRational.FromDecimal(Value.Degree);
            ExifRational Min = ExifRational.FromDecimal(Value.Minute);
            ExifRational Sec = ExifRational.FromDecimal(Value.Second);
            if (SetTagValue(ValueTag, Deg, ExifTagType.URational, 0) &&
                SetTagValue(ValueTag, Min, ExifTagType.URational, 1) &&
                SetTagValue(ValueTag, Sec, ExifTagType.URational, 2) &&
                ((Value.CardinalPoint == Cp1) || (Value.CardinalPoint == Cp2)) &&
                SetTagValue(RefTag, Value.CardinalPoint.ToString(), StrCoding.Utf8))
            {
                Success = true;
            }
            return (Success);
        }


        private bool GetDateAndTimeWithMillisecHelper(out DateTime Value, ExifTag DateAndTimeTag, ExifTag MillisecTag)
        {
            bool Success = false;
            if (GetTagValue(DateAndTimeTag, out Value))
            {
                Success = true;
                if (GetTagValue(MillisecTag, out string SubSec, StrCoding.Utf8))
                {
                    string s = SubSec;
                    int len = s.Length;
                    if (len > 3) s = s.Substring(0, 3);
                    if (int.TryParse(s, out int MilliSec) && (MilliSec >= 0))
                    {
                        if (len == 1) MilliSec *= 100;
                        else if (len == 2) MilliSec *= 10;
                        Value = Value.AddMilliseconds(MilliSec);
                    }
                }
            }
            return (Success);
        }


        private bool SetDateAndTimeWithMillisecHelper(DateTime Value, ExifTag DateAndTimeTag, ExifTag MillisecTag)
        {
            bool Success = false;
            if (SetTagValue(DateAndTimeTag, Value))
            {
                Success = true;
                int MilliSec = Value.Millisecond;
                if ((MilliSec != 0) || TagExists(MillisecTag))
                {
                    string s = MilliSec.ToString("000"); // Write exactly 3 decimal digits
                    Success = Success && SetTagValue(MillisecTag, s, StrCoding.Utf8);
                }
            }
            return (Success);
        }


        private class TagItem
        {
            public ExifTagId TagId;
            public ExifTagType TagType;
            public int ValueCount; // Number of values of the tag. In general this is not the number of bytes of the tag!
            public byte[] ValueData; // Array which contains the tag data. There may be other data stored in this array.
            public int ValueIndex; // Array index from which the tag data starts.
            public int AllocatedByteCount; // Number of bytes which are allocated for the tag data. 
                                           // This number can be greater than the required number of bytes.
            public int ByteCount // The number of bytes required to store the tag data, i. e. the number of bytes actually used for the tag data.
            {
                get
                {
                    return (ExifData.GetTagByteCount(TagType, ValueCount));
                }
            }



            public TagItem(ExifTag TagSpec, ExifTagType _TagType, int _ValueCount) :
              this(ExtractTagId(TagSpec), _TagType, _ValueCount)
            {
            }


            public TagItem(ExifTagId _TagId, ExifTagType _TagType, int _ValueCount)
            {
                TagId = _TagId;
                TagType = _TagType;
                ValueCount = _ValueCount;
                int RequiredByteCount = GetTagByteCount(_TagType, _ValueCount);
                ValueData = AllocTagMemory(RequiredByteCount);
                AllocatedByteCount = ValueData.Length;
                ValueIndex = 0;
            }


            public TagItem(ExifTagId _TagId, ExifTagType _TagType, int _ValueCount, byte[] _ValueArray, int _ValueIndex, int _AllocatedByteCount)
            {
                TagId = _TagId;
                TagType = _TagType;
                ValueCount = _ValueCount;
                ValueData = _ValueArray;
                ValueIndex = _ValueIndex;
                AllocatedByteCount = _AllocatedByteCount;
            }


            public static byte[] AllocTagMemory(int RequiredByteCount)
            {
                const int MinByteCount = 32;
                int NewByteCount = RequiredByteCount;
                if (NewByteCount < MinByteCount)
                {
                    NewByteCount = MinByteCount;
                }
                return (new byte[NewByteCount]);
            }


            // Set tag type and value count and reallocate the tag memory if required.
            // The parameter "_ValueCount" must be in a valid range (0 to 65535), because it is not checked here.
            public void SetTagTypeAndValueCount(ExifTagType _TagType, int _ValueCount, bool KeepExistingData)
            {
                int RequiredByteCount = GetTagByteCount(_TagType, _ValueCount);
                if (AllocatedByteCount < RequiredByteCount)
                {
                    int NewByteCount = AllocatedByteCount;
                    do
                    {
                        NewByteCount <<= 1; // Double the allocated byte count
                    }
                    while (NewByteCount < RequiredByteCount);
                    byte[] NewTagData = AllocTagMemory(NewByteCount);
                    if (KeepExistingData)
                    {
                        Array.Copy(ValueData, ValueIndex, NewTagData, 0, AllocatedByteCount);
                    }
                    AllocatedByteCount = NewTagData.Length;
                    ValueData = NewTagData;
                    ValueIndex = 0;
                }
                TagType = _TagType;
                ValueCount = _ValueCount;
            }

            #endregion
        }
    }
}
