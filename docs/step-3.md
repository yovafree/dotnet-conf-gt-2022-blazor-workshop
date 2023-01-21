# Paso 3 - Uso de Eventos y Componentes

1. Modificar la pagina ubicada en Pages/Users.razor con el siguiente contenido:
```
@page "/users"
@using BlazorApp.Data
@inject FakeDataService FakeService
@using Microsoft.AspNetCore.Components.QuickGrid

<PageTitle>Usuarios</PageTitle>

<h1>Usuarios</h1>

<p>Obtenemos información asíncrona de un servicio.</p>

<p><strong>@message</strong></p>

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
        <TemplateColumn Title="Actions">
            <button @onclick="@(() => Evento1(context))">Evento 1</button>
            <button @onclick="@(() => Evento2(context))">Evento 2</button>
        </TemplateColumn>
    </QuickGrid>
</div>

<Paginator Value="@pagination" />

}

@code {
    private User[]? users;
    IQueryable<User>? items;
    PaginationState pagination = new PaginationState { ItemsPerPage = 7 };
    string nameFilter = string.Empty;
    string message = string.Empty;
    IQueryable<User>? FilteredItems => items?.Where(x => x.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase));

    protected override async Task OnInitializedAsync()
    {
        users = await FakeService.GetFakeDataAsync();
        items = users.AsQueryable();
    }

    void Evento1(User p) => message = $"Evento 1: Usuario: {p.Name} Seguidores: {p.Followers}";

    void Evento2(User p) => message = $"Evento 2: Usuario: {p.Name} Seguidores: {p.Followers}";
}
```

2. Agregar un nuevo estílo de botones al archivo wwwroot/site.css al final del arhivo:
```
/* Stripe effect */
::deep tbody tr { background-color: rgba(0,0,0,0.04); }
::deep tbody tr:nth-child(even) { background: rgba(255,255,255,0.4); }

/* Button styles*/
button {
    background: #4969ee;
    color: white;
    padding: 0.2rem 1rem;
    border-radius: 0.25rem;
    margin: 0.25rem 0.5rem;
}
button:hover { background-color: #6884f9; }
button:active { background-color: #192e86; }
```
3. Levantar el proyecto

```
dotnet run
```