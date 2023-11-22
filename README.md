# ASP.NET Core Backend Template

This project is developed for your .NET backend projects.

## Versions

**.NET Standard 2.1**
- Business
- DataAccess
- Core
- Entities

**.NET 7**
- WebAPI

## Information
This project developed by using Classic Architecture (Hexagonal Architecture). Developed according to SOLID principles. Based on **clean code** approach. 
Basicly, it's creating from ***Abstract*** and ***Concrete*** folders.
***Abstract*** folders includes interfaces or abstract classes.
***Concrete*** folders includes concrete classes.

## Entities
This Class Lib. is going to include domain models inside. It's empty becasue this project is an architecture, doesn't include custom unnecessary models.
In **Core Layer** there are soma main entities.  

![entities](https://github.com/Dagbfatih/MyArchitecture/assets/74913012/68f328e3-3a33-43e5-9e20-f93e5b96d8b9)

We see ***Abstract*** and ***Concrete*** folders here. IEntity can include some common props like ID. However I prefered leave this option to you.
In ***Concrete*** we see some models. They are using almost all projects also they aren't mandatory.
We can see DTOs for **Authentication** and **Localization**.

## DataAccess Layer
This layer gets data from DB like its name. You can migrate them with `Update-database` command.
I used EF in this layer and I created these folder names long ago. So I named with `Ef...Dal` (DataAccessLayer - Dal). But now we don't need to add `Ef`.
**For clean code, you must use `AbstractDal`s always.**

## Business
This layer is the most complicated layer of all.

![flowchart](https://github.com/Dagbfatih/MyArchitecture/assets/74913012/6abcb74a-4bd5-4f8b-8e40-fc576e15096f)

