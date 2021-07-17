# Pokedex

There are successful brands who have managed to stay relevant for more than a decade without innovation too much. Some of our colleagues have young children and they would like to offer them a fresh perspective on the world of Pokemon.

## Installation

### Requirements

- .Net Core 3.1 - [Download](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- [Docker](https://docs.docker.com/get-docker/) and [Docker Compose](https://docs.docker.com/compose/install/)

### Run Loally

- Clone this repo
- Navigate to the project folder
- Run `dotnet restore`
- Navigate to `src/Pokedex.WebApi`
- Run `dotnet run`

### Run inside docker

- Clone this repo
- Navigate to the project folder
- Run `docker-compose up -d --build`

## Usage

All responses will have the form

```json
{
  "name": "ditto",
  "description": "It can freely recombine its own cellular structure to transform into other life-forms.",
  "habitat": "urban",
  "isLegendary": false
}
```

### List a Pokemon

**Definition**

`GET /api/{name}`

**Response**

- `404 Not Found` if the Pokemon does not exists
- `200 OK` on success

```json
{
  "name": "ditto",
  "description": "It can freely recombine its own cellular structure to transform into other life-forms.",
  "habitat": "urban",
  "isLegendary": false
}
```

### Translate Pokemon description

**Definition**

`GET /api/translate/{name}`

**Response**

- `404 Not Found` if the Pokemon does not exists
- `200 OK` on success

```json
{
  "name": "pikachu",
  "description": "最近發表了聚集大量皮卡丘 來建造發電廠的計畫。",
  "habitat": "forest",
  "isLegendary": false
}
```
