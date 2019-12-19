using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.BLL.Dto.Character
{
    public class CharacterLookDto
    {
        public int Id { get; set; }
        public byte? FatherFaceId { get; set; }
        public byte? MotherFaceId { get; set; }
        public byte? SkinColour { get; set; }
        public float? ShapeMix { get; set; }
        public byte? EarsId { get; set; }
        public byte? EarsTexture { get; set; }
        public byte? EyebrowsId { get; set; }
        public byte? SecondEyebrowsColor { get; set; }
        public float EyeBrowsOpacity { get; set; }
        public byte? FirstEyebrowsColor { get; set; }
        public byte? FirstLipstickColor { get; set; }
        public float? LipstickOpacity { get; set; }
        public byte? SecondLipstickColor { get; set; }
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
        public byte? UndershirtId { get; set; }
        public byte? LegsId { get; set; }
        public byte? LegsTexture { get; set; }
        public byte? ShoesId { get; set; }
        public byte? ShoesTexture { get; set; }
    }
}
