

using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Data.Base
{
    public class BaseApi : ControllerBase
    {
        private readonly IHttpClientFactory _httpClient;

        public BaseApi(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        

        public async Task<IActionResult> PostToApi(string controllerName, object model, string token = "")
        {
            try
            {
                var client = _httpClient.CreateClient("useApi");

                if (token != "")
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                }

                var response = await client.PostAsJsonAsync(controllerName, model);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Ok(content);
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        public async Task<IActionResult> PutToApi(string controllerName, object model, string token = "")
        {
            try
            {
                var client = _httpClient.CreateClient("useApi");

                if (token != "")
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                }

                var response = await client.PutAsJsonAsync(controllerName, model);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Ok(content);
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        public async Task<IActionResult> DeleteToApi(string controllerName, string id, string token = "")
        {
            try
            {
                var client = _httpClient.CreateClient("useApi");

                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                var response = await client.DeleteAsync($"{controllerName}");

                if (response.IsSuccessStatusCode)
                {
                    // Successfully deleted
                    return Ok(); // You can return any response you prefer for a successful deletion.
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // Handle the case where the resource to delete was not found.
                    return NotFound();
                }

                return BadRequest(); // Handle other error cases as needed.
            }
            catch (Exception ex)
            {
                // Handle exceptions here, such as network errors or other issues.
                return StatusCode(500, ex.Message); // You can return an appropriate error response.
            }
        }
    }
}