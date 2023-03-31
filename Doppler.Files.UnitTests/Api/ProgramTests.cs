namespace Doppler.Files.UnitTests.Api
{
    public class ProgramTests
    {
        [Fact]
        public async Task TestSwagger()
        {
            var application = new WebApplicationFactory<Program>();

            var swagger = application.Services
                                     .GetRequiredService<ISwaggerProvider>()
                                     .GetSwagger("v1");

            var client = application.CreateClient(new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = false
            });

            var response = await client.GetAsync("/swagger/index.html");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("text/html; charset=utf-8", response.Content?.Headers?.ContentType?.ToString());
            Assert.Equal(1, swagger.Components.SecuritySchemes.Count);
            Assert.Equal("Bearer",swagger.Components.SecuritySchemes.Keys.Single());
        }
    }
}
