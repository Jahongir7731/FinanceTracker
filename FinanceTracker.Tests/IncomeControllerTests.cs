namespace FinanceTracker.Tests
{
    public class IncomeControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public IncomeControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ShouldReturnSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/income");
            response.EnsureSuccessStatusCode();
        }
    }
}
