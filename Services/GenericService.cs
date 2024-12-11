//using ClubBlazor.Class;
//using ClubBlazor.Interfaces;
//using System.Net.Http.Json;
//using System.Text.Json;


//    public class GenericService<T> : IGenericService<T> where T : class
//    {
//        private readonly HttpClient client;
//        private static readonly JsonSerializerOptions options = new JsonSerializerOptions()
//        {
//            PropertyNameCaseInsensitive = true,
//            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
//        };
//        private readonly string _endpoint;

//        public GenericService(HttpClient client)
//        {
//            this.client = client;
//            this._endpoint = ApiEndPoints.GetEndPoint(typeof(T).Name);
//        }

//        //Obtener todos los elementos
//        public async Task<List<T>?> GetAllAsync()
//        {
//            var response = await client.GetAsync(_endpoint);
//            response.EnsureSuccessStatusCode();
//            var content = await response.Content.ReadAsStringAsync();
//            return JsonSerializer.Deserialize<List<T>>(content, options);
//        }

//        // Obtener un elemento por id
//        public async Task<T?> GetByIdAsync(int id)
//        {
//            var fullEndpoint = $"{_endpoint}/{id}";
//            var response = await client.GetAsync(fullEndpoint);
//            response.EnsureSuccessStatusCode();
//            var content = await response.Content.ReadAsStringAsync();
//            return JsonSerializer.Deserialize<T>(content, options);
//        }

//        // Añadir un elemento
//        public async Task<T?> AddAsync(T? entity)
//        {
//            var response = await client.PostAsJsonAsync(_endpoint, entity);
//            response.EnsureSuccessStatusCode();
//            var content = await response.Content.ReadAsStringAsync();
//            return JsonSerializer.Deserialize<T>(content, options);
//        }

//        // Actualizar un elemento
//        public async Task UpdateAsync(T? entity)
//        {
//            var idValue = entity?.GetType().GetProperty("Id")?.GetValue(entity);
//            if (idValue == null)
//            {
//                throw new ApplicationException("El objeto no tiene una propiedad Id");
//            }
//            var response = await client.PutAsJsonAsync($"{_endpoint}/{idValue}", entity);
//            response.EnsureSuccessStatusCode();
//        }

//        // Eliminar un elemento
//        public async Task DeleteAsync(int id)
//        {

//            var response = await client.DeleteAsync($"{_endpoint}/{id}");
//            response.EnsureSuccessStatusCode();
//        }
//    }

using ClubBlazor.Class;
using ClubBlazor.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

public class GenericService<T> : IGenericService<T> where T : class
{
    private readonly HttpClient client;
    private static readonly JsonSerializerOptions options = new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true,
        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
    };
    private readonly string _endpoint;

    public GenericService(HttpClient client)
    {
        this.client = client;
        this._endpoint = ApiEndPoints.GetEndPoint(typeof(T).Name);
    }

    // Obtener todos los elementos
    public async Task<List<T>?> GetAllAsync()
    {
        try
        {
            var response = await client.GetAsync(_endpoint);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<T>>(content, options);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error al obtener todos los elementos: {ex.Message}");
            return null; // o manejar el error de manera diferente
        }
    }

    // Obtener un elemento por id
    public async Task<T?> GetByIdAsync(int id)
    {
        try
        {
            var fullEndpoint = $"{_endpoint}/{id}";
            var response = await client.GetAsync(fullEndpoint);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, options);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error al obtener el elemento con ID {id}: {ex.Message}");
            return null;
        }
    }

    // Añadir un elemento
    public async Task<T?> AddAsync(T? entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula.");
        }

        try
        {
            var response = await client.PostAsJsonAsync(_endpoint, entity);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, options);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error al agregar el elemento: {ex.Message}");
            return null;
        }
    }

    // Actualizar un elemento
    public async Task UpdateAsync(T? entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula.");
        }

        var idValue = entity?.GetType().GetProperty("Id")?.GetValue(entity);
        if (idValue == null)
        {
            throw new ApplicationException("El objeto no tiene una propiedad Id");
        }

        try
        {
            var response = await client.PutAsJsonAsync($"{_endpoint}/{idValue}", entity);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error al actualizar el elemento: {ex.Message}");
            throw;
        }
    }

    // Eliminar un elemento
    public async Task DeleteAsync(int id)
    {
        try
        {
            var response = await client.DeleteAsync($"{_endpoint}/{id}");
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error al eliminar el elemento con ID {id}: {ex.Message}");
            throw;
        }
    }
}

