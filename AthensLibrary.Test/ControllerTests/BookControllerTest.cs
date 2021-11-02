using AthensLibrary.Model.Entities;
using AthensLibrary.Model.RequestFeatures;
using AthensLibrary.Test.Utilities;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AthensLibrary.Test.ControllerTests
{
    public class BookControllerTest : IClassFixture<CustomApplicationFactory<Startup>>
    {
        private readonly CustomApplicationFactory<Startup> _factory;
        private HttpClient _httpClient;
        public BookControllerTest(CustomApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        
        }

        [Fact]
        public async Task GetAllBooksByTitle_ReturnsListOfBooks()
        {
            //Arrange
            string url = Endpoints.Books;

            //Act
            var response = await _httpClient.GetAsync(url);

            string content = await response.Content.ReadAsStringAsync();
            var responseResult = JsonConvert.DeserializeObject<PagedList<Book>>(content);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
