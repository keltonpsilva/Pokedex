using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pokedex.Infraclassure.Integrations.PokeApi.Contracts.Response.v2
{
    public class PokemonSpeciesResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }

        [JsonProperty("gender_rate")]
        public int GenderRate { get; set; }

        [JsonProperty("capture_rate")]
        public int CaptureRate { get; set; }

        [JsonProperty("base_happiness")]
        public int BaseHappiness { get; set; }

        [JsonProperty("is_baby")]
        public bool IsBaby { get; set; }

        [JsonProperty("is_legendary")]
        public bool IsLegendary { get; set; }

        [JsonProperty("is_mythical")]
        public bool IsMythical { get; set; }

        [JsonProperty("hatch_counter")]
        public int HatchCounter { get; set; }

        [JsonProperty("has_gender_differences")]
        public bool HasGenderDifferences { get; set; }

        [JsonProperty("forms_switchable")]
        public bool FormsSwitchable { get; set; }

        [JsonProperty("growth_rate")]
        public GrowthRate GrowthRate { get; set; }

        [JsonProperty("pokedex_numbers")]
        public List<PokedexNumber> PokedexNumbers { get; set; }

        [JsonProperty("egg_groups")]
        public List<EggGroup> EggGroups { get; set; }

        [JsonProperty("color")]
        public Color Color { get; set; }

        [JsonProperty("shape")]
        public Shape Shape { get; set; }

        [JsonProperty("evolves_from_species")]
        public EvolvesFromSpecies EvolvesFromSpecies { get; set; }

        [JsonProperty("evolution_chain")]
        public EvolutionChain EvolutionChain { get; set; }

        [JsonProperty("habitat")]
        public PokemonHabitat Habitat { get; set; }

        [JsonProperty("generation")]
        public Generation Generation { get; set; }

        [JsonProperty("names")]
        public List<PokemonName> Names { get; set; }

        [JsonProperty("flavor_text_entries")]
        public List<FlavorTextEntry> FlavorTextEntries { get; set; }

        [JsonProperty("form_descriptions")]
        public List<FormDescription> FormDescriptions { get; set; }

        [JsonProperty("genera")]
        public List<Genera> Genera { get; set; }

        [JsonProperty("varieties")]
        public List<Variety> Varieties { get; set; }
    }

    public class GrowthRate
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Pokedex
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class PokedexNumber
    {
        [JsonProperty("entry_number")]
        public int EntryNumber { get; set; }

        [JsonProperty("pokedex")]
        public Pokedex Pokedex { get; set; }
    }

    public class EggGroup
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Color
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Shape
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class EvolvesFromSpecies
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class EvolutionChain
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Generation
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Language
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class PokemonName
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("language")]
        public Language Language { get; set; }
    }

    public class Version
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class FlavorTextEntry
    {
        [JsonProperty("flavor_text")]
        public string FlavorText { get; set; }

        [JsonProperty("language")]
        public Language Language { get; set; }

        [JsonProperty("version")]
        public Version Version { get; set; }
    }

    public class FormDescription
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("language")]
        public Language Language { get; set; }
    }

    public class Genera
    {
        [JsonProperty("genus")]
        public string Genus { get; set; }

        [JsonProperty("language")]
        public Language Language { get; set; }
    }

    public class Pokemon
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Variety
    {
        [JsonProperty("is_default")]
        public bool IsDefault { get; set; }

        [JsonProperty("pokemon")]
        public Pokemon Pokemon { get; set; }
    }

    public class PokemonHabitat
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

