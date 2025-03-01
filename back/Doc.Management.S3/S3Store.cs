﻿using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Doc.Management.Documents;
using Microsoft.Extensions.Options;

namespace Doc.Management.S3;

public class S3Store(IAmazonS3 s3Client, IOptionsSnapshot<S3Options> s3OptionsSnapshot) : IStoreFile
{
    private readonly IAmazonS3 _s3Client = s3Client;
    private readonly S3Options _s3Options = s3OptionsSnapshot.Value;

    public async Task CreateBucketAsync(string bucketName)
    {
        var request = new PutBucketRequest { BucketName = bucketName, UseClientRegion = true, };

        var exists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);

        if (exists)
        {
            return;
        }

        await _s3Client.PutBucketAsync(request);
    }

    public async Task UploadStreamAsync(
        Stream stream,
        string key,
        CancellationToken cancellationToken = default
    )
    {
        var request = new PutObjectRequest
        {
            InputStream = stream,
            Key = key,
            BucketName = _s3Options.BucketName
        };

        await _s3Client.PutObjectAsync(request, cancellationToken);
    }

    public Task<Stream> GetStreamAsync(string key, CancellationToken cancellationToken = default) =>
        _s3Client.GetObjectStreamAsync(
            _s3Options.BucketName,
            key,
            new Dictionary<string, object>(),
            cancellationToken
        );

    public Task DeleteFileAsync(string key, CancellationToken cancellationToken) =>
        _s3Client.DeleteAsync(
            _s3Options.BucketName,
            key,
            new Dictionary<string, object>(),
            cancellationToken
        );
}
