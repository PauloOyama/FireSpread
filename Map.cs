class VegetationMap
{
    private readonly List<List<string>> vegetationMapList;
    private List<List<Vegetation>> vegetationMap = new();

    public VegetationMap()
    //improve to read json
    {

        vegetationMapList = new List<List<string>>
        {
            new () { "E", "E", "E", "S", "S", "S", "T", "T", "T", "RU", "RU", "RU", "RI", "RI", "RI", "E", "E", "E", "S", "S" },
            new () { "S", "S", "S", "S", "S", "S", "T", "T", "T", "RU", "RU", "RU", "RI", "RI", "RI", "E", "E", "E", "S", "S" },
            new() { "S", "S", "S", "S", "S", "S", "T", "T", "T", "RU", "RU", "RU", "RI", "RI", "RI", "E", "E", "E", "S", "S" },
            new() { "S", "S", "S", "T", "T", "T", "RU", "RU", "RU", "RI", "RI", "RI", "E", "E", "E", "S", "S", "S", "T", "T" },
            new() { "T", "T", "T", "T", "T", "T", "RU", "RU", "RU", "RI", "RI", "RI", "E", "E", "E", "S", "S", "S", "T", "T" },
            new() { "T", "T", "T", "T", "T", "T", "RU", "RU", "RU", "RI", "RI", "RI", "E", "E", "E", "S", "S", "S", "T", "T" },
            new() { "T", "T", "T", "RU", "RU", "RU", "RI", "RI", "RI", "E", "E", "E", "S", "S", "S", "T", "T", "T", "RU", "RU" },
            new() { "RU", "RU", "RU", "RU", "RU", "RU", "RI", "RI", "RI", "E", "E", "E", "S", "S", "S", "T", "T", "T", "RU", "RU" },
            new() { "RU", "RU", "RU", "RU", "RU", "RU", "RI", "RI", "RI", "E", "E", "E", "S", "S", "S", "T", "T", "T", "RU", "RU" },
            new() { "RU", "RU", "RU", "RI", "RI", "RI", "E", "E", "E", "S", "S", "S", "T", "T", "T", "RU", "RU", "RU", "RI", "RI" }
        };
        // vegetationMapList = new List<List<string>>
        // {
        //     new() { "E", "E", "E", "S", "S", "S", "T", "T", "T" },
        //     new() { "S", "S", "S", "T", "T", "T", "RU", "RU", "RU" },
        //     new() { "RI", "RI", "RI", "E", "E", "E", "S", "S", "S" },
        //     new() { "T", "T", "T", "RU", "RU", "RU", "RI", "RI", "RI" },
        //     new() { "E", "E", "E", "S", "S", "S", "T", "T", "T" },
        //     new() { "RU", "RU", "RU", "RI", "RI", "RI", "E", "E", "E" },
        //     new() { "S", "S", "S", "T", "T", "T", "RU", "RU", "RU" },
        //     new() { "RI", "RI", "RI", "E", "E", "E", "S", "S", "S" },
        //     new() { "T", "T", "T", "RU", "RU", "RU", "RI", "RI", "RI" }
        // };
        // vegetationMapList = new List<List<string>>
        // {
        //     new() { "E", "E", "E","E", "E", "E","E", "E", "E" },
        //     new() { "E", "E", "E","E", "E", "E","E", "E", "E" },
        //     new() { "E", "E", "E","E", "E", "E","E", "E", "E" },
        //     new() { "E", "E", "E","E", "E", "E","E", "E", "E" },
        //     new() { "E", "E", "E","E", "E", "E","E", "E", "E" },
        //     new() { "E", "E", "E","E", "E", "E","E", "E", "E" },
        //     new() { "E", "E", "E","E", "E", "E","E", "E", "E" },
        //     new() { "E", "E", "E","E", "E", "E","E", "E", "E" },
        //     new() { "E", "E", "E","E", "E", "E","E", "E", "E" },
        // };
        GetMap();
    }

    private void GetMap()
    {
        List<List<Vegetation>> map = new();
        for (int i = 0; i < vegetationMapList.Count; i++)
        {
            List<Vegetation> line = new();
            for (int ii = 0; ii < vegetationMapList[0].Count; ii++)
            {
                line.Add(new Vegetation(vegetationMapList[i][ii]));
            }
            map.Add(line);
        }
        vegetationMap = map;
    }

    public List<List<Vegetation>> MakeGrid()
    {


        vegetationMap[4][4].SetFire();

        List<List<Vegetation>> padding = new();
        List<Vegetation> aa = new();

        for (int y = 0; y < vegetationMap.Count + 2; y++) aa.Add(new Vegetation(""));

        padding.Add(aa);

        for (int i = 0; i < vegetationMap.Count; i++)
        {
            List<Vegetation> paddingLine = new();
            for (int ii = 0; ii < vegetationMap[i].Count + 2; ii++)
            {
                if (ii == 0) paddingLine.Add(vegetationMap[i][ii]);
                else if (ii == (vegetationMap[i].Count + 1)) paddingLine.Add(vegetationMap[i][ii - 2]);
                else paddingLine.Add(vegetationMap[i][ii - 1]);

            }
            padding.Add(paddingLine);
        }
        padding.Add(aa);

        return padding;
    }


}
