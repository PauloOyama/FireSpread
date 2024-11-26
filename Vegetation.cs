
using Raylib_CsLo;

class Vegetation
{
    public Enum vegetationType;
    public float probabilityToBurn;

    public Color vegetationColor;

    public Vegetation(Enum type, float probability, Color color)
    {
        vegetationType = type;
        probabilityToBurn = probability;
        vegetationColor = color;
    }


}