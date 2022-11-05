﻿using System.IO;
using System.Threading.Tasks;

namespace NativeMedia;

partial class MediaFile : IMediaFile
{
    private string extension;

    public string NameWithoutExtension { get; protected internal set; }

    public string Extension
    {
        get => extension;
        protected internal set
            => extension = value?.TrimStart('.')?.ToLower();
    }

    public string ContentType { get; protected internal set; }

    public MediaFileType? Type { get; protected set; }

    public Task<Stream> OpenReadAsync()
    {
#if __MOBILE__
            return PlatformOpenReadAsync();
#endif
        throw new NotImplementedException();
    }

    public void Dispose()
    {
#if __MOBILE__
            PlatformDispose();
#endif
    }

    protected MediaFileType? GetFileType(string contentType)
    {
        if (string.IsNullOrWhiteSpace(contentType))
            return null;
        if (ContentType.StartsWith("image"))
            return MediaFileType.Image;
        if (ContentType.StartsWith("video"))
            return MediaFileType.Video;

        return null;
    }
}