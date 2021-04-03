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
    // Tag specification constants: Composition of IFD and tag ID.
    public enum ExifTag
  {
    // IFD Primary Data
    ImageWidth = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.ImageWidth,
    ImageLength = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.ImageLength,
    BitsPerSample = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.BitsPerSample,
    PhotometricInterpretation = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.PhotometricInterpretation,
    ImageDescription = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.ImageDescription,
    Make = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.Make,
    Model = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.Model,
    StripOffsets = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.StripOffsets,
    Orientation = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.Orientation,
    SamplesPerPixel = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.SamplesPerPixel,
    RowsPerStrip = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.RowsPerStrip,
    StripByteCounts = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.StripByteCounts,
    PlanarConfiguration = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.PlanarConfiguration,
    TransferFunction = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.TransferFunction,
    Software = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.Software,
    DateTime = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.DateTime,
    Artist = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.Artist,
    WhitePoint = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.WhitePoint,
    PrimaryChromaticities = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.PrimaryChromaticities,
    YCbCrCoefficients = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.YCbCrCoefficients,
    YCbCrSubSampling = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.YCbCrSubSampling,
    YCbCrPositioning = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.YCbCrPositioning,
    ReferenceBlackWhite = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.ReferenceBlackWhite,
    Copyright = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.Copyright,
    ExifIfdPointer = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.ExifIfdPointer,
    GpsInfoIfdPointer = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.GpsInfoIfdPointer,
    XpTitle = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.XpTitle,
    XpComment = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.XpComment,
    XpAuthor = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.XpAuthor,
    XpKeywords = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.XpKeywords,
    XpSubject = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.XpSubject,
    Compression = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.Compression,
    XResolution = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.XResolution,
    YResolution = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.YResolution,
    ResolutionUnit = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.ResolutionUnit,
    PrimaryDataPadding = (ExifIfd.PrimaryData << ExifData.IfdShift) | ExifTagId.Padding,

    // IFD Private Data
    ExposureTime = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.ExposureTime,
    FNumber = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.FNumber,
    ExposureProgram = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.ExposureProgram,
    SpectralSensitivity = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.SpectralSensitivity,
    IsoSpeedRatings = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.IsoSpeedRatings,
    PhotographicSensitivity = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.PhotographicSensitivity,
    Oecf = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.Oecf,
    SensitivityType = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.SensitivityType,
    StandardOutputSensitivity = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.StandardOutputSensitivity,
    RecommendedExposureIndex = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.RecommendedExposureIndex,
    IsoSpeed = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.IsoSpeed,
    IsoSpeedLatitudeyyy = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.IsoSpeedLatitudeyyy,
    IsoSpeedLatitudezzz = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.IsoSpeedLatitudezzz,
    ExifVersion = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.ExifVersion,
    DateTimeOriginal = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.DateTimeOriginal,
    DateTimeDigitized = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.DateTimeDigitized,
    OffsetTime = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.OffsetTime,
    OffsetTimeOriginal = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.OffsetTimeOriginal,
    OffsetTimeDigitized = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.OffsetTimeDigitized,
    ComponentsConfiguration = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.ComponentsConfiguration,
    CompressedBitsPerPixel = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.CompressedBitsPerPixel,
    ShutterSpeedValue = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.ShutterSpeedValue,
    ApertureValue = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.ApertureValue,
    BrightnessValue = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.BrightnessValue,
    ExposureBiasValue = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.ExposureBiasValue,
    MaxApertureValue = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.MaxApertureValue,
    SubjectDistance = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.SubjectDistance,
    MeteringMode = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.MeteringMode,
    LightSource = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.LightSource,
    Flash = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.Flash,
    FocalLength = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.FocalLength,
    SubjectArea = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.SubjectArea,
    MakerNote = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.MakerNote,
    UserComment = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.UserComment,
    SubsecTime = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.SubsecTime,
    SubsecTimeOriginal = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.SubsecTimeOriginal,
    SubsecTimeDigitized = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.SubsecTimeDigitized,
    FlashPixVersion = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.FlashPixVersion,
    ColorSpace = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.ColorSpace,
    PixelXDimension = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.PixelXDimension,
    PixelYDimension = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.PixelYDimension,
    RelatedSoundFile = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.RelatedSoundFile,
    InteroperabilityIfdPointer = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.InteroperabilityIfdPointer,
    FlashEnergy = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.FlashEnergy,
    SpatialFrequencyResponse = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.SpatialFrequencyResponse,
    FocalPlaneXResolution = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.FocalPlaneXResolution,
    FocalPlaneYResolution = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.FocalPlaneYResolution,
    FocalPlaneResolutionUnit = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.FocalPlaneResolutionUnit,
    SubjectLocation = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.SubjectLocation,
    ExposureIndex = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.ExposureIndex,
    SensingMethod = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.SensingMethod,
    FileSource = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.FileSource,
    SceneType = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.SceneType,
    CfaPattern = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.CfaPattern,
    CustomRendered = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.CustomRendered,
    ExposureMode = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.ExposureMode,
    WhiteBalance = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.WhiteBalance,
    DigitalZoomRatio = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.DigitalZoomRatio,
    FocalLengthIn35mmFilm = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.FocalLengthIn35mmFilm,
    SceneCaptureType = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.SceneCaptureType,
    GainControl = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.GainControl,
    Contrast = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.Contrast,
    Saturation = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.Saturation,
    Sharpness = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.Sharpness,
    DeviceSettingDescription = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.DeviceSettingDescription,
    SubjectDistanceRange = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.SubjectDistanceRange,
    ImageUniqueId = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.ImageUniqueId,
    CameraOwnerName = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.CameraOwnerName,
    BodySerialNumber = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.BodySerialNumber,
    LensSpecification = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.LensSpecification,
    LensMake = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.LensMake,
    LensModel = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.LensModel,
    LensSerialNumber = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.LensSerialNumber,
    PrivateDataPadding = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.Padding,
    OffsetSchema = (ExifIfd.PrivateData << ExifData.IfdShift) | ExifTagId.OffsetSchema,

    // IFD GPS Data
    GpsVersionId = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsVersionId,
    GpsLatitudeRef = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsLatitudeRef,
    GpsLatitude = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsLatitude,
    GpsLongitudeRef = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsLongitudeRef,
    GpsLongitude = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsLongitude,
    GpsAltitudeRef = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsAltitudeRef,
    GpsAltitude = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsAltitude,
    GpsTimeStamp = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsTimestamp,
    GpsSatellites = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsSatellites,
    GpsStatus = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsStatus,
    GpsMeasureMode = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsMeasureMode,
    GpsDop = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsDop,
    GpsSpeedRef = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsSpeedRef,
    GpsSpeed = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsSpeed,
    GpsTrackRef = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsTrackRef,
    GpsTrack = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsTrack,
    GpsImgDirectionRef = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsImgDirectionRef,
    GpsImgDirection = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsImgDirection,
    GpsMapDatum = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsMapDatum,
    GpsDestLatitudeRef = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsDestLatitudeRef,
    GpsDestLatitude = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsDestLatitude,
    GpsDestLongitudeRef = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsDestLongitudeRef,
    GpsDestLongitude = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsDestLongitude,
    GpsDestBearingRef = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsDestBearingRef,
    GpsDestBearing = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsDestBearing,
    GpsDestDistanceRef = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsDestDistanceRef,
    GpsDestDistance = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsDestDistance,
    GpsProcessingMethod = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsProcessingMethod,
    GpsAreaInformation = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsAreaInformation,
    GpsDateStamp = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsDateStamp,
    GpsDifferential = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsDifferential,
    GpsHPositioningError = (ExifIfd.GpsInfoData << ExifData.IfdShift) | ExifTagId.GpsHPositioningError,

    // IFD Interoperability
    InteroperabilityIndex = (ExifIfd.Interoperability << ExifData.IfdShift) | ExifTagId.InteroperabilityIndex,
    InteroperabilityVersion = (ExifIfd.Interoperability << ExifData.IfdShift) | ExifTagId.InteroperabilityVersion,

    // IFD Thumbnail Data
    ThumbnailImageWidth = (ExifIfd.ThumbnailData << ExifData.IfdShift) | ExifTagId.ImageWidth,
    ThumbnailImageLength = (ExifIfd.ThumbnailData << ExifData.IfdShift) | ExifTagId.ImageLength,
    ThumbnailCompression = (ExifIfd.ThumbnailData << ExifData.IfdShift) | ExifTagId.Compression,
    ThumbnailXResolution = (ExifIfd.ThumbnailData << ExifData.IfdShift) | ExifTagId.XResolution,
    ThumbnailYResolution = (ExifIfd.ThumbnailData << ExifData.IfdShift) | ExifTagId.YResolution,
    ThumbnailResolutionUnit = (ExifIfd.ThumbnailData << ExifData.IfdShift) | ExifTagId.ResolutionUnit,
    ThumbnailOrientation = (ExifIfd.ThumbnailData << ExifData.IfdShift) | ExifTagId.Orientation,
    JpegInterchangeFormat = (ExifIfd.ThumbnailData << ExifData.IfdShift) | ExifTagId.JpegInterchangeFormat,
    JpegInterchangeFormatLength = (ExifIfd.ThumbnailData << ExifData.IfdShift) | ExifTagId.JpegInterchangeFormatLength,
  }
}
