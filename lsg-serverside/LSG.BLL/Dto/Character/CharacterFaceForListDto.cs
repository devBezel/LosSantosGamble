using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.BLL.Dto.Character
{
    public class CharacterFaceForListDto
    {
        public int Id { get; set; }
        public int ShapeFirstID { get; set; }
        public int ShapeSecondID { get; set; }
        public int ShapeThirdID { get; set; }
        public int SkinFirstID { get; set; }
        public int SkinSecondID { get; set; }
        public int SkinThirdID { get; set; }
        public float ShapeMix { get; set; }
        public float SkinMix { get; set; }
        public float ThirdMix { get; set; }
        public bool IsParent { get; set; }
    }
}
