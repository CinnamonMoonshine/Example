public class BlockRender : AbstractBlockRender
{
    private readonly Vector3[] RightVertices;
    private readonly Vector3[] LeftVertices;
    private readonly Vector3[] UpVertices;
    private readonly Vector3[] DownVertices;
    private readonly Vector3[] ForwardVertices;
    private readonly Vector3[] BackwardVertices;
    private readonly Vector2[] UVs;

    public BlockRender(uint id, Vector3[] rightVertices, Vector3[] leftVertices, Vector3[] upVertices,
        Vector3[] downVertices, Vector3[] forwardVertices, Vector3[] backwardVertices, Vector2[] uvs) : base(id)
    {
        RightVertices = rightVertices;
        LeftVertices = leftVertices;
        UpVertices = upVertices;
        DownVertices = downVertices;
        ForwardVertices = forwardVertices;
        BackwardVertices = backwardVertices;
        UVs = uvs;
    }
    
    public override (Vector3[] vertices, Vector2[] uvs) GetBlockSidesData(BlockSide blockSide, Rectangle uvPosition, Vector3 blockPosition)
    {
        Vector3[] sideData = blockSide switch
        {
            BlockSide.RIGHT => RightVertices,
            BlockSide.LEFT => LeftVertices,
            BlockSide.UP => UpVertices,
            BlockSide.DOWN => DownVertices,
            BlockSide.FORWARD => ForwardVertices,
            BlockSide.BACKWARD => BackwardVertices
        };
        Vector3[] sideDataValue = new Vector3[sideData.Length];
        for (int i = 0; i < sideData.Length; i++)
        {
            sideDataValue[i] = sideData[i] + blockPosition;
        }
        Vector2[] uvDataValue = new Vector2[UVs.Length];
        for (int i = 0; i < UVs.Length; i++)
        {
            Vector2 uv = UVs[i];

            uv.X = uvPosition.X + uv.X * uvPosition.Width;
            uv.Y = uvPosition.Y + uv.Y * uvPosition.Height;
            uv.X /= Constants.AtlasSizeX;
            uv.Y /= Constants.AtlasSizeY;

            uvDataValue[i] = uv;
        }
        return (sideDataValue, uvDataValue);
    }
    public override (Vector3[] vertices, Vector2[] uvs)[] GetBlockSidesData((BlockSide blockSide, Rectangle uvPosition)[] sides, Vector3 blockPosition)
    {
        (Vector3[], Vector2[])[] data = new (Vector3[], Vector2[])[sides.Length];
        
        for (int i = 0; i < sides.Length; i++)
        {
            (BlockSide blockSide, Rectangle uvPosition) = sides[i];
            
            Vector3[] sideData = blockSide switch
            {
                BlockSide.RIGHT => RightVertices,
                BlockSide.LEFT => LeftVertices,
                BlockSide.UP => UpVertices,
                BlockSide.DOWN => DownVertices,
                BlockSide.FORWARD => ForwardVertices,
                BlockSide.BACKWARD => BackwardVertices
            };
            Vector3[] sideDataValue = new Vector3[sideData.Length];
            for (int j = 0; j < sideData.Length; j++)
            {
                sideDataValue[j] = sideData[j] + blockPosition;
            }
            Vector2[] uvDataValue = new Vector2[UVs.Length];
            for (int j = 0; j < UVs.Length; j++)
            {
                Vector2 uv = UVs[j];

                uv.X = uvPosition.X + uv.X * uvPosition.Width;
                uv.Y = uvPosition.Y + uv.Y * uvPosition.Height;
                uv.X /= Constants.AtlasSizeX;
                uv.Y /= Constants.AtlasSizeY;

                uvDataValue[j] = uv;
            }
            data[i] = (sideDataValue, uvDataValue);
        }
        return data;
    }
}
