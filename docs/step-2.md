# Paso 2 - Uso de Quick Grid

[Ver documentación](https://aspnet.github.io/quickgridsamples/getstarted) 

1. Instalación de la dependencia Quick Grid:

```
dotnet add package Microsoft.AspNetCore.Components.QuickGrid --prerelease
```

2. Modificar la pagina ubicada en Pages/Users.razor con el siguiente contenido:
```
@page "/users"
@using BlazorApp.Data
@inject FakeDataService FakeService
@using Microsoft.AspNetCore.Components.QuickGrid

<PageTitle>Usuarios</PageTitle>

<h1>Usuarios</h1>

<p>Obtenemos información asíncrona de un servicio.</p>

@if (users == null)
{
    <p><em>Loading...</em></p>
}
else
{
<div class="grid">
    <QuickGrid Items="@FilteredItems" ResizableColumns="true" Pagination="@pagination">
        <PropertyColumn Property="@(c => c.Name)" Sortable="true" Class="name">
            <ColumnOptions>
                <div class="search-box">
                    <input type="search" autofocus @bind="nameFilter" @bind:event="oninput" placeholder="Nombre" />
                </div>
            </ColumnOptions>
        </PropertyColumn>
        <PropertyColumn Property="@(c => c.Name)" Sortable="true" Align="Align.Right" />
        <PropertyColumn Property="@(c => c.Followers)" Sortable="true" Align="Align.Right" />
        <PropertyColumn Property="@(c => c.Area)" Sortable="true" Align="Align.Right" />
        <PropertyColumn Property="@(c => c.Bio)" Sortable="true" Align="Align.Right" />
    </QuickGrid>
</div>

<Paginator Value="@pagination" />

}

@code {
    private User[]? users;
    IQueryable<User>? items;
    PaginationState pagination = new PaginationState { ItemsPerPage = 7 };
    string nameFilter = string.Empty;
    IQueryable<User>? FilteredItems => items?.Where(x => x.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase));

    protected override async Task OnInitializedAsync()
    {
        users = await FakeService.GetFakeDataAsync();
        items = users.AsQueryable();

    }
}

```

3. Observar el uso de Quick Grid en las llamadas, filtros y paginación.

4. Levantar el proyecto

```
dotnet run
```