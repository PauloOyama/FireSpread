
using Raylib_CsLo;

enum VegetationType
{
    Riparian,
    Evergreen,
    Savannah,
    Typical,
    Rupestrian,
    Error

}

class Vegetation
{
    private VegetationType vegetationType;
    private bool isBurning;
    public Vegetation(string type) => vegetationType = GetVegetationType(type);


    public static VegetationType GetVegetationType(string type)
    {
        return type switch
        {
            "RI" => VegetationType.Riparian,
            "E" => VegetationType.Evergreen,
            "S" => VegetationType.Savannah,
            "T" => VegetationType.Typical,
            "RU" => VegetationType.Rupestrian,
            _ => VegetationType.Error,
        };
    }

    public int GetProbabilityToBurn()
    {
        return vegetationType switch
        {
            VegetationType.Riparian => 10,
            VegetationType.Evergreen => 25,
            VegetationType.Savannah => 45,
            VegetationType.Typical => 65,
            VegetationType.Rupestrian => 80,
            _ => 0,
        };
    }
    public Color GetVegetationColor()
    {
        return vegetationType switch
        {
            VegetationType.Riparian => new Color() { r = 0, g = 51, b = 0 },
            VegetationType.Evergreen => new Color() { r = 51, g = 102, b = 0 },
            VegetationType.Savannah => new Color() { r = 0, g = 153, b = 0 },
            VegetationType.Typical => new Color() { r = 0, g = 204, b = 102 },
            VegetationType.Rupestrian => new Color() { r = 0, g = 255, b = 0 },
            _ => new Color() { r = 0, g = 51, b = 204 },
        };
    }

    public bool SetBurn() => isBurning = true;

    public bool IsBurning() => isBurning;

}