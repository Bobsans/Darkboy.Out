# Darkboy.Out [![NuGet](https://img.shields.io/nuget/v/Darkboy.Out?logo=nuget)](https://www.nuget.org/packages/Darkboy.Out/) [![GitHub](https://img.shields.io/github/license/Bobsans/Darkboy.Out)](licence)

The package implements the Result pattern. Once again :)

## Getting Started

> `install-package Darkboy.Out`

## Usage

```csharp
    public Out<Entity> CreateEntity(string name, decimal price) {
        if (!IsValidName(name)) {
            return Out<Entity>.Fail().WithMessage("Invalid name");
        }
        if (!IsValidPrice(price)) {
            return Out<Entity>.Fail().WithMessage("Invalid price");
        }
        try {
            var entity = new Entity(name, price);
            _repo.Save(entity);
            return entity;
        } catch (Exception ex) {
            return Out<Entity>.Fail("Fail to create entity", ex);
        }
    }
    
    public void Run() {
        var result = CreateEntity("MyEntity", 100);
        if (result.IsSuccess) {
            Console.WriteLine($"Entity created: {result.Value.Id}");
        } else {
            Console.WriteLine(result.Message);
        }
    }
```
