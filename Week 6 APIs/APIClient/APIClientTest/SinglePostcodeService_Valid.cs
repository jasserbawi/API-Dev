using APIClientApp;

using RestSharp;
using Newtonsoft.Json.Linq;

namespace APIClientTest;

public class WhenTheSinglePostcodeServiceIsCalled_WithValidPostcode
{
    SinglePostcodeService _singlePostcodeService;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _singlePostcodeService = new SinglePostcodeService();
        await _singlePostcodeService.MakeRequestAsync("EC2Y 5AS");
    }
    [Test]
    public void StatusIs200_InJsonResponseBody()
    {
        Assert.That(_singlePostcodeService.ResponseContent["status"].ToString(), Is.EqualTo("200"));
    }
    [Test]
    public void StatusIs200()
    {
        Assert.That((int)_singlePostcodeService.Response.StatusCode, Is.EqualTo(200));
    }
    [Test]
    public void CorrectPostcodeIsReturned()
    {
        Assert.That(_singlePostcodeService.ResponseContent["result"]["postcode"].ToString(), Is.EqualTo("EC2Y 5AS"));
    }
    [Test]
    public void ConnectionIsKeepAlive()
    {
        var result = _singlePostcodeService.Response.Headers.Where(x => x.Name == "Connection").Select(x => x.Value.ToString()).FirstOrDefault();

        Assert.That(result, Is.EqualTo("keep-alive"));
    }

}