﻿namespace Doc.Management.Documents;

public interface IStoreFile
{
    Task UploadStreamAsync(
        Stream stream,
        string key,
        CancellationToken cancellationToken = default
    );

    Task<Stream> GetStreamAsync(string key, CancellationToken cancellationToken = default);
    Task DeleteFileAsync(string key, CancellationToken cancellationToken);
    Task CreateBucketAsync(string bucketName);
}
