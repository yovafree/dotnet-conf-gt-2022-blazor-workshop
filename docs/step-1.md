# Paso 1 - Creación de un servicio de Datos Falsos - Fake Data

1. Instalación de Faket.Net desde la terminal ejecutar el siguiente comando:

```
dotnet add package Faker.Net
```

2. Crear una clase User.cs en el directorio /Data con el siguiente contenido:

```csharp
namespace BlazorApp.Data;

public class User
{
    public string Name { get; set; }
    public int Followers { get; set; }
    public string Area { get; set; }
    public string Bio { get; set; }
}

```

1. Crear una clase FakeDataService.cs en el directorio /Data con el siguiente contenido

```csharp
namespace BlazorApp.Data;
using Faker;
public class FakeDataService
{
    public Task<User[]> GetFakeDataAsync()
    {
        return Task.FromResult(Enumerable.Range(1, 50).Select(index => new User
        {
            Name = Faker.Name.FullName(NameFormats.WithPrefix),
            Followers = Faker.RandomNumber.Next(0, 10000),
            Area = $"{Faker.Address.Country()}, {Faker.Address.City()}",
            Bio = String.Join(" ", Faker.Lorem.Sentences(3))
        }).ToArray());
    }
}
```
4. Inyectar la dependencia del servicio FakeService a la clase Program.cs, despues de la línea 10.
```csharp
builder.Services.AddSingleton<FakeDataService>();
```

5. Crear el archivo Users.razor en el directorio Users y agregar el siguiente contenido:
```
@page "/users"
@using BlazorApp.Data
@inject FakeDataService FakeService

<PageTitle>Usuarios</PageTitle>

<p>Obtenemos información asíncrona de un servicio.</p>

@if (users == null)
{
    <p><em>Loading...</em></p>
}
else
{
    <table class="table">
        <thead>
            <tr>
                <th>Nombre</th>
                <th>Seguidores</th>
                <th>Area</th>
                <th>Biografía</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var user in users)
            {
                <tr>
                    <td>@user.Name</td>
                    <td>@user.Followers</td>
                    <td>@user.Area</td>
                    <td>@user.Bio</td>
                </tr>
            }
        </tbody>
    </table>
}

@code {
    private User[]? users;

    protected override async Task OnInitializedAsync()
    {
        users = await FakeService.GetFakeDataAsync();
    }
}

```
6. Editar el menú ubicado en /Shared/NavMenu.razor, agregar el siguiente contenido despues de la línea 26.
```
<div class="nav-item px-3">
    <NavLink class="nav-link" href="users">
        <span class="oi oi-list-rich" aria-hidden="true"></span> Usuarios
    </NavLink>
</div>
```

7. Levantar el proyecto

```
dotnet run
```