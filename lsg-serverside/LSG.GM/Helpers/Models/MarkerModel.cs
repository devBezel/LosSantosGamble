using AltV.Net;

namespace LSG.GM.Helpers
{
    public class MarkerModel  : IWritable
    {
        public int Type
        {
            get;
            set;
        }

        public int Dimension { get; set; }
        public float PosX
        {
            get;
            set;
        }
        public float PosY
        {
            get;
            set;
        }
        public float PosZ
        {
            get;
            set;
        }
        public float DirX
        {
            get;
            set;
        }
        public float DirY
        {
            get;
            set;
        }
        public float DirZ
        {
            get;
            set;
        }
        public float RotX
        {
            get;
            set;
        }
        public float RotY
        {
            get;
            set;
        }
        public float RotZ
        {
            get;
            set;
        }
        public float ScaleX
        {
            get;
            set;
        }
        public float ScaleY
        {
            get;
            set;
        }
        public float ScaleZ
        {
            get;
            set;
        }
        public int Red
        {
            get;
            set;
        }
        public int Green
        {
            get;
            set;
        }
        public int Blue
        {
            get;
            set;
        }
        public int Alpha
        {
            get;
            set;
        }
        public bool BobUpAndDown
        {
            get;
            set;
        }
        public bool FaceCamera
        {
            get;
            set;
        }
        public int P19
        {
            get;
            set;
        }
        public bool Rotate
        {
            get;
            set;
        }
        public string TextureDict
        {
            get;
            set;
        }
        public string TextureName
        {
            get;
            set;
        }
        public bool DrawOnEnts
        {
            get;
            set;
        }
        public string UniqueID
        {
            get;
            set;
        }




        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("type");
            writer.Value(Type);

            writer.Name("dimension");
            writer.Value(Dimension);

            writer.Name("posX");
            writer.Value(PosX);

            writer.Name("posY");
            writer.Value(PosY);

            writer.Name("posZ");
            writer.Value(PosZ);

            writer.Name("dirX");
            writer.Value(DirX);

            writer.Name("dirY");
            writer.Value(DirY);

            writer.Name("dirZ");
            writer.Value(DirZ);

            writer.Name("rotX");
            writer.Value(RotX);

            writer.Name("rotY");
            writer.Value(RotY);

            writer.Name("rotZ");
            writer.Value(RotZ);

            writer.Name("scaleX");
            writer.Value(ScaleX);

            writer.Name("scaleY");
            writer.Value(ScaleY);

            writer.Name("scaleZ");
            writer.Value(ScaleZ);

            writer.Name("red");
            writer.Value(Red);

            writer.Name("green");
            writer.Value(Green);

            writer.Name("blue");
            writer.Value(Blue);

            writer.Name("alpha");
            writer.Value(Alpha);

            writer.Name("bobUpAndDown");
            writer.Value(BobUpAndDown);

            writer.Name("faceCamera");
            writer.Value(FaceCamera);

            writer.Name("p19");
            writer.Value(P19);

            writer.Name("rotate");
            writer.Value(Rotate);

            writer.Name("textureDict");
            writer.Value(TextureDict);

            writer.Name("textureName");
            writer.Value(TextureName);

            writer.Name("drawOnEnts");
            writer.Value(DrawOnEnts);

            writer.Name("uniqueID");
            writer.Value(UniqueID);

            writer.EndObject();

        }
    }
}