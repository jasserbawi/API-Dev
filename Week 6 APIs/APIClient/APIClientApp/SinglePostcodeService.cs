namespace APIClientApp;

public class SinglePostcodeSercice
{
    #region Properties
    //Restsharp object which handles communication with the Postcodes.io API
    public RestClient Client { get; set; }
    
    //Capture the response
    public RestResponse Response { get; set; }

    //a Newtonsoft JObject to represent the json response
    public JObject ResponseContent { get; set; }

    #endregion

    public SinglePostcodeSercice()
    {
        Client = new RestClient(AppConfigReader.BaseUrl);
    }
}
