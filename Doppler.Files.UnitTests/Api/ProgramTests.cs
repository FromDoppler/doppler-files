namespace Doppler.Files.UnitTests.Api
{
    public class ProgramTests
    {
        private readonly WebApplicationFactory<Program> application;

        public ProgramTests()
        {
            application = new WebApplicationFactory<Program>();
        }

        [Theory]
        [InlineData("/swagger/index.html", HttpStatusCode.OK, "text/html; charset=utf-8")]
        [InlineData("/swagger/v1/swagger.json", HttpStatusCode.OK, "application/json; charset=utf-8")]
        public async Task GetEndpoints_ShouldReturnCorrectStatusAndContentType(string url, HttpStatusCode expectedStatusCode, string expectedContentType)
        {
            var client = application.CreateClient(new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = false
            });

            var response = await client.GetAsync(url);

            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Equal(expectedContentType, response.Content?.Headers?.ContentType?.ToString());
        }
        [Fact]
        public void Swagger_ShouldHaveCorrectSecuritySchema()
        {
            var swagger = application.Services
                                     .GetRequiredService<ISwaggerProvider>()
                                     .GetSwagger("v1");

            Assert.Equal(1, swagger.Components.SecuritySchemes.Count);
            Assert.Equal("Bearer", swagger.Components.SecuritySchemes.Keys.Single());
        }
    }
}
