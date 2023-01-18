using Newtonsoft.Json;

namespace APIClientApp.PostcodeService;

public class SinglePostcodeService
{
    #region Properties
    //Restsharp object which handles communication with the Postcodes.io API
    public RestClient Client { get; set; }

    //Capture the response
    public RestResponse Response { get; set; }

    //a Newtonsoft JObject to represent the json response
    public JObject ResponseContent { get; set; }

    public SinglePostcodeResponse ResponseObject { get; set;}

    #endregion

    public SinglePostcodeService()
    {
        Client = new RestClient(AppConfigReader.BaseUrl);
    }

    public async Task MakeRequestAsync(string postcode)
    {
        var restRequest = new RestRequest(); //Makes the HTTP Request

        //Building the request
        restRequest.Method = Method.Get; 
        restRequest.AddHeader("Content-Type", "application/json");
        restRequest.Timeout = -1;

        restRequest.Resource = $"postcodes/{postcode}";

        Response = await Client.ExecuteAsync(restRequest);

        ResponseContent = JObject.Parse(Response.Content);

        ResponseObject = JsonConvert.DeserializeObject<SinglePostcodeResponse>(Response.Content);

    }
}
