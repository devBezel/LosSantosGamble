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
        public int? FatherFaceId { get; set; }
        public int? MotherFaceId { get; set; }
        public int? SkinColour { get; set; }
        public float? ShapeMix { get; set; }
        public int? EarsColor { get; set; }
        public int? BlemishesId { get; set; }
        public float? BlemishesOpacity { get; set; }
        public int? AgeingId { get; set; }
        public float? AgeingOpacity { get; set; }
        public int? BlushId { get; set; }
        public float? BlushOpacity { get; set; }
        public int? BlushColor { get; set; }
        public int? BeardId { get; set; }
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



        public int? EyebrowsId { get; set; }
        public int? SecondEyebrowsColor { get; set; }
        public float EyeBrowsOpacity { get; set; }
        public int? FirstEyebrowsColor { get; set; }
        public int? LipstickId { get; set; }
        public int? FirstLipstickColor { get; set; }
        public float? LipstickOpacity { get; set; }
        public int? SecondLipstickColor { get; set; }
        public float? MakeupId { get; set; }
        public int? FirstMakeupColor { get; set; }
        public float? MakeupOpacity { get; set; }
        public int? SecondMakeupColor { get; set; }
        public int? GlassesId { get; set; }
        public int? GlassesTexture { get; set; }
        public int? HairId { get; set; }
        public int? HairTexture { get; set; }
        public int? HairColor { get; set; }
        public int? HairColorTwo { get; set; }
        public int? HatId { get; set; }
        public int? HatTexture { get; set; }
        public int? TopId { get; set; }
        public int? TopTexture { get; set; }
        public int? TorsoId { get; set; }
        public int? TorsoTexture { get; set; }
        public int? UndershirtId { get; set; }
        public int? UndershirtTexture { get; set; }
        public int? LegsId { get; set; }
        public int? LegsTexture { get; set; }
        public int? ShoesId { get; set; }
        public int? ShoesTexture { get; set; }

    }
}
