using TechnologyOneTest;
namespace TestProject1;

public class UnitTest1
{
    [Fact]
    public void TestWebServerResponse()
    {
        
    }
[Fact]
    public void TestWebServerRequest() 
    {

    }
[Fact]
    public void TestNumToNameOnes()
    {
        Assert.True(TechnologyOneTest.HttpServer.NumToName(1) =="ONE");
    }
[Fact]
    public void TestNumToNameTeens()
    {
        Assert.True(TechnologyOneTest.HttpServer.NumToName(11) =="ELEVEN");
    }
[Fact]
    public void TestNumToNameHyphen()
    {
        Assert.True(TechnologyOneTest.HttpServer.NumToName(31) == "THIRTY-ONE");
    }
[Fact]
    public void TestNumToNameAnd()
    {
        Assert.True(TechnologyOneTest.HttpServer.NumToName(101) == "ONE HUNDRED AND ONE");
        Assert.True(TechnologyOneTest.HttpServer.NumToName(100001) == "ONE HUNDRED THOUSAND AND ONE");
    }
}

