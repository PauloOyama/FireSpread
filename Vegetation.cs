
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
    private int burningStage = 0;
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
    public Color GetColor()
    {
        if (!IsBurning())
        {
            return vegetationType switch
            {
                VegetationType.Riparian => new Color() { r = 0, g = 51, b = 0, a = 150 },
                VegetationType.Evergreen => new Color() { r = 51, g = 102, b = 0, a = 255 },
                VegetationType.Savannah => new Color() { r = 0, g = 153, b = 0, a = 255 },
                VegetationType.Typical => new Color() { r = 0, g = 204, b = 102, a = 255 },
                VegetationType.Rupestrian => new Color() { r = 0, g = 255, b = 0, a = 255 },
                _ => new Color() { r = 0, g = 51, b = 204, a = 255 },
            };
        }
        else
        {
            return burningStage switch
            {
                1 => new() { r = 255, g = 242, b = 0, a = 255 },
                2 => new() { r = 209, g = 198, b = 0, a = 255 },
                3 => new() { r = 255, g = 127, b = 39, a = 255 },
                4 => new() { r = 255, g = 85, b = 19, a = 255 },
                5 => new() { r = 237, g = 28, b = 36, a = 255 },
                6 => new() { r = 136, g = 0, b = 21, a = 255 },
                7 => new() { r = 51, g = 51, b = 51, a = 255 },
                _ => new Color() { r = 0, g = 51, b = 204 },
            };

        }
    }

    //Color 
    //Must be Refactor in other class
    //Fire doens't make sense to be a property of Vegetation   
    public bool IsBurning() => burningStage > 0;
    public void SetFire() => burningStage = 1;
    public void UpdateBurn()
    {
        if (burningStage < 7)
            burningStage++;

    }
}