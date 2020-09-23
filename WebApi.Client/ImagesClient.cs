using LaXiS.ImageHash.Models.Resources;
using RestSharp;
using System;

namespace LaXiS.ImageHash.WebApi.Client
{
    public class ImagesClient
    {
        private readonly RestClient client;

        public ImagesClient(string baseUrl)
        {
            client = new RestClient(baseUrl)
            {
                ThrowOnAnyError = true
            };
        }

        public void Post(ImageWriteResource image)
        {
            var request = new RestRequest("images");
            request.AddJsonBody(image);

            var response = client.Post(request);
            if (!response.IsSuccessful)
                throw new Exception(response.Content);
        }
    }
}
