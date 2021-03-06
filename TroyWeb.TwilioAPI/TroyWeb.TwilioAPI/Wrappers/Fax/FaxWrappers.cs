﻿namespace TroyWeb.TwilioAPI.Wrappers.Fax
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Twilio.Base;
    using Twilio.Clients;
    using Twilio.Rest.Fax.V1;

    public static class FaxWrappers
    {
        public static async Task<FaxResource> SendFaxAsync(ITwilioRestClient client, string from, string to, Uri mediaUrl, FaxResource.QualityEnum quality = null, string sipAuthUsername = null, string sipAuthPassword = null, Uri statusCallback = null, bool? storeMedia = null, int? minutesToSend = null)
        {
            var options = new CreateFaxOptions(to, mediaUrl)
            {
                From = from,
                Quality = quality ?? FaxResource.QualityEnum.Standard,
                SipAuthUsername = sipAuthUsername,
                SipAuthPassword = sipAuthPassword,
                StatusCallback = statusCallback,
                StoreMedia = storeMedia,
                Ttl = minutesToSend
            };
            return await FaxResource.CreateAsync(options, client);
        }

        public static async Task<FaxResource> CancelFaxAsync(ITwilioRestClient client, string faxSid)
        {
            var options = new UpdateFaxOptions(faxSid)
            {
                Status = FaxResource.UpdateStatusEnum.Canceled
            };
            return await FaxResource.UpdateAsync(options, client);
        }

        public static async Task<bool> DeleteFaxAsync(ITwilioRestClient client,
            string faxSid)
        {
            var options = new DeleteFaxOptions(faxSid);
            return await FaxResource.DeleteAsync(options, client);
        }

        public static async Task<FaxResource> GetFaxAsync(ITwilioRestClient client, string faxSid)
        {
            var options = new FetchFaxOptions(faxSid);
            return await FaxResource.FetchAsync(options, client);
        }

        public static async Task<ResourceSet<FaxResource>> GetFaxesAsync(ITwilioRestClient client, string from = null, string to = null, DateTime? dateCreatedAfter = null, DateTime? dateCreatedOnOrBefore = null, long? limit = null)
        {
            var options = new ReadFaxOptions
            {
                DateCreatedAfter = dateCreatedAfter,
                DateCreatedOnOrBefore = dateCreatedOnOrBefore,
                From = from,
                To = to,
                Limit = limit,
                PageSize = null
            };
            return await FaxResource.ReadAsync(options, client);
        }

        public static async Task<FileInfo> DownloadFaxMediaAsync(ITwilioRestClient twilioRestClient, FaxResource fax, string path)
        {
            var twilioPath = fax.MediaUrl;
            var file = File.Create(path);
            using (var client = new HttpClient())
            {
                var bytes = await client.GetByteArrayAsync(twilioPath);
                await file.WriteAsync(bytes, 0, bytes.Length);
                await file.FlushAsync();
            }
            return new FileInfo(path);
        }
    }
}
