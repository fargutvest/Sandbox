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
    // Tag ID constants.
    public enum ExifTagId
  {
    // IFD Primary Data
    ImageWidth = 0x0100,
    ImageLength = 0x0101,
    BitsPerSample = 0x0102,
    PhotometricInterpretation = 0x0106,
    ImageDescription = 0x010e,
    Make = 0x010f,
    Model = 0x0110,
    StripOffsets = 0x0111,
    SamplesPerPixel = 0x0115,
    RowsPerStrip = 0x0116,
    StripByteCounts = 0x0117,
    PlanarConfiguration = 0x011c,
    TransferFunction = 0x012d,
    Software = 0x0131,
    DateTime = 0x0132,
    Artist = 0x013b,
    WhitePoint = 0x013e,
    PrimaryChromaticities = 0x013f,
    YCbCrCoefficients = 0x0211,
    YCbCrSubSampling = 0x0212,
    YCbCrPositioning = 0x0213,
    ReferenceBlackWhite = 0x0214,
    Copyright = 0x8298,
    ExifIfdPointer = 0x8769,
    GpsInfoIfdPointer = 0x8825,
    XpTitle = 0x9c9b,
    XpComment = 0x9c9c,
    XpAuthor = 0x9c9d,
    XpKeywords = 0x9c9e,
    XpSubject = 0x9c9f,
    Padding = 0xea1c,

    // IFD Primary Data and IFD Thumbnail Data
    Compression = 0x0103,
    XResolution = 0x011a,
    YResolution = 0x011b,
    ResolutionUnit = 0x0128,
    Orientation = 0x0112,

    // IFD Thumbnail Data
    JpegInterchangeFormat = 0x0201,
    JpegInterchangeFormatLength = 0x0202,

    // IFD Private Data
    ExposureTime = 0x829a,
    FNumber = 0x829d,
    ExposureProgram = 0x8822,
    SpectralSensitivity = 0x8824,
    IsoSpeedRatings = 0x8827,
    PhotographicSensitivity = 0x8827, // Tag was renamed from "IsoSpeedRatings" to this name
    Oecf = 0x8828,
    SensitivityType = 0x8830,
    StandardOutputSensitivity = 0x8831,
    RecommendedExposureIndex = 0x8832,
    IsoSpeed = 0x8833,
    IsoSpeedLatitudeyyy = 0x8834,
    IsoSpeedLatitudezzz = 0x8835,
    ExifVersion = 0x9000,
    DateTimeOriginal = 0x9003,
    DateTimeDigitized = 0x9004,
    OffsetTime = 0x9010,
    OffsetTimeOriginal = 0x9011,
    OffsetTimeDigitized = 0x9012,
    ComponentsConfiguration = 0x9101,
    CompressedBitsPerPixel = 0x9102,
    ShutterSpeedValue = 0x9201,
    ApertureValue = 0x9202,
    BrightnessValue = 0x9203,
    ExposureBiasValue = 0x9204,
    MaxApertureValue = 0x9205,
    SubjectDistance = 0x9206,
    MeteringMode = 0x9207,
    LightSource = 0x9208,
    Flash = 0x9209,
    FocalLength = 0x920a,
    SubjectArea = 0x9214,
    MakerNote = 0x927c,
    UserComment = 0x9286,
    SubsecTime = 0x9290,
    SubsecTimeOriginal = 0x9291,
    SubsecTimeDigitized = 0x9292,
    FlashPixVersion = 0xa000,
    ColorSpace = 0xa001,
    PixelXDimension = 0xa002,
    PixelYDimension = 0xa003,
    RelatedSoundFile = 0xa004,
    InteroperabilityIfdPointer = 0xa005,
    FlashEnergy = 0xa20b,
    SpatialFrequencyResponse = 0xa20c,
    FocalPlaneXResolution = 0xa20e,
    FocalPlaneYResolution = 0xa20f,
    FocalPlaneResolutionUnit = 0xa210,
    SubjectLocation = 0xa214,
    ExposureIndex = 0xa215,
    SensingMethod = 0xa217,
    FileSource = 0xa300,
    SceneType = 0xa301,
    CfaPattern = 0xa302,
    CustomRendered = 0xa401,
    ExposureMode = 0xa402,
    WhiteBalance = 0xa403,
    DigitalZoomRatio = 0xa404,
    FocalLengthIn35mmFilm = 0xa405,
    SceneCaptureType = 0xa406,
    GainControl = 0xa407,
    Contrast = 0xa408,
    Saturation = 0xa409,
    Sharpness = 0xa40a,
    DeviceSettingDescription = 0xa40b,
    SubjectDistanceRange = 0xa40c,
    ImageUniqueId = 0xa420,
    CameraOwnerName = 0xa430,
    BodySerialNumber = 0xa431,
    LensSpecification = 0xa432,
    LensMake = 0xa433,
    LensModel = 0xa434,
    LensSerialNumber = 0xa435,
    OffsetSchema = 0xea1d,

    // IFD GPS Data
    GpsVersionId = 0x0000,
    GpsLatitudeRef = 0x0001,
    GpsLatitude = 0x0002,
    GpsLongitudeRef = 0x0003,
    GpsLongitude = 0x0004,
    GpsAltitudeRef = 0x0005,
    GpsAltitude = 0x0006,
    GpsTimestamp = 0x0007,
    GpsSatellites = 0x0008,
    GpsStatus = 0x0009,
    GpsMeasureMode = 0x000a,
    GpsDop = 0x000b,
    GpsSpeedRef = 0x000c,
    GpsSpeed = 0x000d,
    GpsTrackRef = 0x000e,
    GpsTrack = 0x000f,
    GpsImgDirectionRef = 0x0010,
    GpsImgDirection = 0x0011,
    GpsMapDatum = 0x0012,
    GpsDestLatitudeRef = 0x0013,
    GpsDestLatitude = 0x0014,
    GpsDestLongitudeRef = 0x0015,
    GpsDestLongitude = 0x0016,
    GpsDestBearingRef = 0x0017,
    GpsDestBearing = 0x0018,
    GpsDestDistanceRef = 0x0019,
    GpsDestDistance = 0x001a,
    GpsProcessingMethod = 0x001b,
    GpsAreaInformation = 0x001c,
    GpsDateStamp = 0x001d,
    GpsDifferential = 0x001e,
    GpsHPositioningError = 0x001f,

    // IFD Interoperability
    InteroperabilityIndex = 0x0001,
    InteroperabilityVersion = 0x0002
  }
}
