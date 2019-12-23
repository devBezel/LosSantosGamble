using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LSG.DAL.Database.Models.CharacterModels
{
    public class CharacterLook
    {
        public int Id { get; set; }
        [ForeignKey("CharacterId")]
        public int CharacterId { get; set; }
        public byte? FatherFaceId { get; set; }
        public byte? MotherFaceId { get; set; }
        public byte? SkinColour { get; set; }
        public float? ShapeMix { get; set; }
        public byte? EarsColor { get; set; }
        public byte? BlemishesId { get; set; }
        public float? BlemishesOpacity { get; set; }
        public byte? AgeingId { get; set; }
        public float? AgeingOpacity { get; set; }
        public byte? BlushId { get; set; }
        public float? BlushOpacity { get; set; }
        public byte? BlushColor { get; set; }
        public byte? BeardId { get; set; }
        public float? BeardOpacity { get; set; }
        public float? BeardColor { get; set; }

        // Face detail
        public float? NoseWidth { get; set; }
        public float? NosePeakHight { get; set; }
        public float? NosePeakLenght { get; set; }
        public float? NoseBoneHigh { get; set; }
        public float? NosePeakLowering { get; set; }
        public float? NoseBoneTwist { get; set; }
        public float? EyeBrownHigh { get; set; }
        public float? EyeBrownForward { get; set; }
        public float? CheeksBoneWidth { get; set; }
        public float? CheeksWidth { get; set; }
        public float? EyesOpenning { get; set; }
        public float? LipsThickness { get; set; }
        public float? JawBoneWidth { get; set; }
        public float? JawBoneBackLenght { get; set; }
        public float? ChimpBoneLowering { get; set; }
        public float? ChimpBoneLenght { get; set; }
        public float? ChimpBoneWidth { get; set; }
        public float? ChimpHole { get; set; }
        public float? NeckThikness { get; set; }



        public byte? EyebrowsId { get; set; }
        public byte? SecondEyebrowsColor { get; set; }
        public float EyeBrowsOpacity { get; set; }
        public byte? FirstEyebrowsColor { get; set; }
        public byte? LipstickId { get; set; }
        public byte? FirstLipstickColor { get; set; }
        public float? LipstickOpacity { get; set; }
        public byte? SecondLipstickColor { get; set; }
        public float? MakeupId { get; set; }
        public byte? FirstMakeupColor { get; set; }
        public float? MakeupOpacity { get; set; }
        public byte? SecondMakeupColor { get; set; }
        public byte? GlassesId { get; set; }
        public byte? GlassesTexture { get; set; }
        public byte? HairId { get; set; }
        public byte? HairTexture { get; set; }
        public byte? HairColor { get; set; }
        public byte? HairColorTwo { get; set; }
        public byte? HatId { get; set; }
        public byte? HatTexture { get; set; }
        public byte? TopId { get; set; }
        public byte? TopTexture { get; set; }
        public byte? TorsoId { get; set; }
        public byte? TorsoTexture { get; set; }
        public byte? UndershirtId { get; set; }
        public byte? UndershirtTexture { get; set; }
        public byte? LegsId { get; set; }
        public byte? LegsTexture { get; set; }
        public byte? ShoesId { get; set; }
        public byte? ShoesTexture { get; set; }

    }
}
