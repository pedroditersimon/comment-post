using CommentPost.Infrastructure.Configurations;
using CommentPost.Infrastructure.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace CommentPost.Infrastructure.Services;

// https://auth0.com/
public class Auth0Service
{
	readonly Auth0Settings _settings;
	private readonly IHttpClientFactory _httpClientFactory;

	public Auth0Service(IHttpClientFactory httpClientFactory, Auth0Settings settings)
	{
		_httpClientFactory = httpClientFactory;
		_settings = settings;
	}


	// authentication code -> accessToken
	public async Task<Auth0TokenResponse?> GetAccessToken(string authenticationCode)
	{
		/* HTTP Request example
		POST /oauth/token
		Content-Type: 'application/x-www-form-urlencoded'
		Request body: {
			grant_type=authorization_code
			&client_id={client_id}
			&client_secret={client_secret}
			&code={authorization_code}
			&redirect_uri=http://localhost:3001
		}
		Response: JSON {
			"access_token": "eyJz93a...k4D5w",
			"id_token": "Gei4pJ8TxO2ZNhbF...bD5Qe",
			"scope": "openid profile",
			"expires_in": 86400,
			"token_type": "Bearer",
		}
		*/

		using HttpClient httpClient = _httpClientFactory.CreateClient();

		httpClient.BaseAddress = new Uri(_settings.BaseUrl);

		FormUrlEncodedContent httpContent = new(new Dictionary<string, string>() {
			{"grant_type", "authorization_code" },
			{"code", authenticationCode },
			{"client_id", _settings.ClientId },
			{"client_secret", _settings.ClientSecret },
			{"redirect_uri", _settings.RedirectUri },
		});

		using HttpResponseMessage response = await httpClient.PostAsync("oauth/token", httpContent);
		if (!response.IsSuccessStatusCode)
			return null;

		var jsonResponse = await response.Content.ReadAsStringAsync();
		var tokenResponse = JsonSerializer.Deserialize<Auth0TokenResponse>(jsonResponse);

		return tokenResponse;
	}


	// accessToken -> user id (sub)
	public string? GetUserId(string accessToken)
	{
		return JwtService.DecodeToken(accessToken)?.Payload
			.GetValueOrDefault("sub")?.ToString();
	}

	public async Task<Auth0UserInfo?> GetUserInfo(string accessToken)
	{
		/* HTTP Request example
		GET /userinfo
		Content-Type: 'application/json'
		Authorization: 'Bearer {access_token}'
		Response: JSON {
			"sub": "github|1234",
			"nickname": "nickname",
			"name": "First Last",
			"picture": "https://avatars.githubusercontent.com/u/nickname",
			"updated_at": "2024-12-10T08:37:06.797Z"
		}
		*/

		// configure client
		using HttpClient httpClient = _httpClientFactory.CreateClient();

		httpClient.BaseAddress = new Uri(_settings.BaseUrl);
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

		try
		{
			using HttpResponseMessage response = await httpClient.GetAsync("userinfo");
			if (!response.IsSuccessStatusCode)
				return null;

			return await response.Content.ReadFromJsonAsync<Auth0UserInfo>();
		}
		catch (Exception)
		{
			return null;
		}
	}
}
