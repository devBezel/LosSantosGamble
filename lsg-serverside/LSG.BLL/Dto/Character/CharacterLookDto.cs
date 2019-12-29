using AltV.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.BLL.Dto.Character
{
    public class CharacterLookDto : IWritable
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public int FatherFaceId { get; set; }
        public int MotherFaceId { get; set; }
        public int SkinColour { get; set; }
        public float ShapeMix { get; set; }
        public int EarsColor { get; set; }
        public int BlemishesId { get; set; }
        public float BlemishesOpacity { get; set; }
        public int AgeingId { get; set; }
        public float AgeingOpacity { get; set; }
        public int BlushId { get; set; }
        public float BlushOpacity { get; set; }
        public int BlushColor { get; set; }
        public int BeardId { get; set; }
        public float BeardOpacity { get; set; }
        public float BeardColor { get; set; }

        // Face detail
        public float NoseWidth { get; set; }
        public float NosePeakHight { get; set; }
        public float NosePeakLenght { get; set; }
        public float NoseBoneHigh { get; set; }
        public float NosePeakLowering { get; set; }
        public float NoseBoneTwist { get; set; }
        public float EyeBrownHigh { get; set; }
        public float EyeBrownForward { get; set; }
        public float CheeksBoneWidth { get; set; }
        public float CheeksWidth { get; set; }
        public float EyesOpenning { get; set; }
        public float LipsThickness { get; set; }
        public float JawBoneWidth { get; set; }
        public float JawBoneBackLenght { get; set; }
        public float ChimpBoneLowering { get; set; }
        public float ChimpBoneLenght { get; set; }
        public float ChimpBoneWidth { get; set; }
        public float ChimpHole { get; set; }
        public float NeckThikness { get; set; }



        public int EyebrowsId { get; set; }
        public int SecondEyebrowsColor { get; set; }
        public float EyeBrowsOpacity { get; set; }
        public int FirstEyebrowsColor { get; set; }
        public int LipstickId { get; set; }
        public int FirstLipstickColor { get; set; }
        public float LipstickOpacity { get; set; }
        public int SecondLipstickColor { get; set; }
        public float MakeupId { get; set; }
        public int FirstMakeupColor { get; set; }
        public float MakeupOpacity { get; set; }
        public int SecondMakeupColor { get; set; }
        public int GlassesId { get; set; }
        public int GlassesTexture { get; set; }
        public int HairId { get; set; }
        public int HairTexture { get; set; }
        public int HairColor { get; set; }
        public int HairColorTwo { get; set; }
        public int HatId { get; set; }
        public int HatTexture { get; set; }
        public int TopId { get; set; }
        public int TopTexture { get; set; }
        public int TorsoId { get; set; }
        public int TorsoTexture { get; set; }
        public int UndershirtId { get; set; }
        public int UndershirtTexture { get; set; }
        public int LegsId { get; set; }
        public int LegsTexture { get; set; }
        public int ShoesId { get; set; }
        public int ShoesTexture { get; set; }


        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(Id);

            writer.Name("characterId");
            writer.Value(CharacterId);

            writer.Name("fatherFaceId ");
            writer.Value(FatherFaceId);

            writer.Name("motherFaceId");
            writer.Value(MotherFaceId);

            writer.Name("skinColour");
            writer.Value(SkinColour);

            writer.Name("shapeMix");
            writer.Value(ShapeMix);

            writer.Name("earsColor");
            writer.Value(EarsColor);

            writer.Name("blemishesId");
            writer.Value(BlemishesId);

            writer.Name("blemishesOpacity");
            writer.Value(BlemishesOpacity);

            writer.Name("ageingId");
            writer.Value(AgeingId);

            writer.Name("ageingOpacity");
            writer.Value(AgeingOpacity);

            writer.Name("blushId");
            writer.Value(BlushId);

            writer.Name("blushOpacity");
            writer.Value(BlushOpacity);

            writer.Name("blushColor");
            writer.Value(BlushColor);

            writer.Name("beardId");
            writer.Value(BeardId);

            writer.Name("beardOpacity");
            writer.Value(BeardOpacity);

            writer.Name("beardColor");
            writer.Value(BeardColor);

            writer.Name("noseWidth");
            writer.Value(NoseWidth);

            writer.Name("nosePeakHight");
            writer.Value(NosePeakHight);

            writer.Name("nosePeakLenght");
            writer.Value(NosePeakLenght);

            writer.Name("noseBoneHigh");
            writer.Value(NoseBoneHigh);

            writer.Name("nosePeakLowering");
            writer.Value(NosePeakLowering);

            writer.Name("noseBoneTwist");
            writer.Value(NoseBoneTwist);

            writer.Name("eyeBrownHigh");
            writer.Value(EyeBrownHigh);

            writer.Name("eyeBrownForward");
            writer.Value(EyeBrownForward);

            writer.Name("cheeksBoneWidth");
            writer.Value(CheeksBoneWidth);

            writer.Name("cheeksWidth");
            writer.Value(CheeksWidth);

            writer.Name("eyesOpenning");
            writer.Value(EyesOpenning);

            writer.Name("lipsThickness");
            writer.Value(LipsThickness);

            writer.Name("jawBoneWidth");
            writer.Value(JawBoneWidth);

            writer.Name("jawBoneBackLenght");
            writer.Value(JawBoneBackLenght);

            writer.Name("chimpBoneLowering");
            writer.Value(ChimpBoneLowering);

            writer.Name("chimpBoneLenght");
            writer.Value(ChimpBoneLenght);

            writer.Name("chimpBoneWidth");
            writer.Value(ChimpBoneWidth);

            writer.Name("chimpHole");
            writer.Value(ChimpHole);

            writer.Name("neckThikness");
            writer.Value(NeckThikness);

            writer.Name("eyebrowsId");
            writer.Value(EyebrowsId);

            writer.Name("secondEyebrowsColor");
            writer.Value(SecondEyebrowsColor);

            writer.Name("eyeBrowsOpacity");
            writer.Value(EyeBrowsOpacity);

            writer.Name("firstEyebrowsColor");
            writer.Value(FirstEyebrowsColor);


            writer.Name("lipstickId");
            writer.Value(LipstickId);

            writer.Name("firstLipstickColor");
            writer.Value(FirstLipstickColor);

            writer.Name("lipstickOpacity");
            writer.Value(LipstickOpacity);

            writer.Name("secondLipstickColor");
            writer.Value(SecondLipstickColor);

            writer.Name("makeupId");
            writer.Value(MakeupId);

            writer.Name("firstMakeupColor");
            writer.Value(FirstMakeupColor);

            writer.Name("makeupOpacity");
            writer.Value(MakeupOpacity);

            writer.Name("secondMakeupColor");
            writer.Value(SecondMakeupColor);

            writer.Name("glassesId");
            writer.Value(GlassesId);

            writer.Name("glassesTexture");
            writer.Value(GlassesTexture);

            writer.Name("hairId");
            writer.Value(HairId);

            writer.Name("hairTexture");
            writer.Value(HairTexture);

            writer.Name("hairColor");
            writer.Value(HairColor);

            writer.Name("hairColorTwo");
            writer.Value(HairColorTwo);

            writer.Name("hatId");
            writer.Value(HatId);


            writer.Name("hatTexture");
            writer.Value(HatTexture);


            writer.Name("topId");
            writer.Value(TopId);


            writer.Name("topTexture");
            writer.Value(TopTexture);


            writer.Name("torsoId");
            writer.Value(TorsoId);

            writer.Name("torsoTexture");
            writer.Value(TorsoTexture);

            writer.Name("undershirtId");
            writer.Value(UndershirtId);


            writer.Name("undershirtTexture");
            writer.Value(UndershirtTexture);


            writer.Name("legsId");
            writer.Value(LegsId);


            writer.Name("legsTexture");
            writer.Value(LegsTexture);


            writer.Name("shoesId");
            writer.Value(ShoesId);

            writer.Name("shoesTexture");
            writer.Value(ShoesTexture);

            writer.EndObject();

        }
    }
}
